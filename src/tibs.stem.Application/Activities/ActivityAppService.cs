using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem;
using tibs.stem.Activities;
using tibs.stem.Activities.Dto;
using tibs.stem.Activities.Exporting;
using tibs.stem.Authorization;
using tibs.stem.Countrys;
using tibs.stem.Countrys.Dto;
using tibs.stem.Dto;
using tibs.stem.Tenants.Dashboard;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Countrys
{
    public class ActivityAppService : stemAppServiceBase, IActivityAppService
    {
        private readonly IRepository<Activity> _activityRepository;
        private readonly IActivityTypeListExcelExporter _activityTypeListExcelExporter;
        public ActivityAppService(IRepository<Activity> activityRepository, IActivityTypeListExcelExporter activityTypeListExcelExporter)
        {
            _activityRepository = activityRepository;
            _activityTypeListExcelExporter = activityTypeListExcelExporter;
        }

        public ListResultDto<ActivityListDto> GetActivity(ActivityInput input)
        {
            var act = _activityRepository
                .GetAll();
            var query = act
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ActivityCode.Contains(input.Filter) ||
                        u.ActivityName.Contains(input.Filter) ||
                        u.ColorCode.Contains(input.Filter))
                .ToList();

            return new ListResultDto<ActivityListDto>(query.MapTo<List<ActivityListDto>>());
        }

        public async Task<GetActivity> GetActivityForEdit(EntityDto input)
        {
            var output = new GetActivity
            {
            };

            var act = _activityRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Activity = act.MapTo<ActivityListDto>();
            return output;
        }


        public async Task CreateOrUpdateActivity(ActivityInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateActivity(input);
            }
            else
            {
                await CreateActivity(input);
            }
        }

        public async Task CreateActivity(ActivityInputDto input)
        {
            var act = input.MapTo<Activity>();
            var val = _activityRepository
             .GetAll().Where(p => p.ActivityCode == input.ActivityCode || p.ActivityName == input.ActivityName).FirstOrDefault();

            if (val == null)
            {
                await _activityRepository.InsertAsync(act);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity Name '" + input.ActivityName + "' or Activity Code '" + input.ActivityCode + "'...");
            }
        }

        public async Task UpdateActivity(ActivityInputDto input)
        {
            var act = await _activityRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, act);

            var val = _activityRepository
              .GetAll().Where(p => (p.ActivityCode == input.ActivityCode || p.ActivityName == input.ActivityName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _activityRepository.UpdateAsync(act);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Activity Name '" + input.ActivityName + "' or Activity Code '" + input.ActivityCode + "'...");
            }

        }
        //public bool GetMappedActivityType(EntityDto input)
        //{
        //    bool ok = false;
        //    ConnectionAppService db = new ConnectionAppService();
        //    DataTable ds = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
        //    {
        //        SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
        //        sqlComm.Parameters.AddWithValue("@TableId", 4);
        //        sqlComm.CommandType = CommandType.StoredProcedure;

        //        using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
        //        {

        //            da.Fill(ds);
        //        }

        //    }

        //    if (input.Id > 0)
        //    {
        //        var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
        //        if (results.Count() > 0)
        //        {
        //            ok = true;
        //        }
        //    }
        //    return ok;
        //}

        public async Task GetDeleteActivityType(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 4);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {

                    da.Fill(ds);
                }

            }

            if (input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    throw new UserFriendlyException("Ooops!", "Activity cannot be deleted '");
                }
                else
                {
                    await _activityRepository.DeleteAsync(input.Id);
                }
            }
        }

       public async Task<FileDto> GetActivityTypeToExcel()
        {
            var act = _activityRepository.GetAll();

            var activityListDtos = (from a in act
                              select new ActivityListDto
                              {
                                  Id = a.Id,
                                  ActivityCode = a.ActivityCode,
                                  ActivityName = a.ActivityName,
                                  ColorCode = a.ColorCode
                                  
                              });
            var order = await activityListDtos.OrderBy("ActivityName").ToListAsync();

            var activityListDtoss = order.MapTo<List<ActivityListDto>>();

            return _activityTypeListExcelExporter.ExportToFile(activityListDtoss);
        }

    }

}