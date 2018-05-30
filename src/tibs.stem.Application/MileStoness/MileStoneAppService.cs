using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Milestones;
using tibs.stem.MileStoness.Dto;
using tibs.stem.SourceTypes;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Countrys;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Dto;
using tibs.stem.MileStoness.Exporting;

namespace tibs.stem.MileStoness
{
    public class MileStoneAppService: stemAppServiceBase, IMileStoneAppService
    {
        private readonly IRepository<MileStone> _milestoneRepository;
        private readonly IRepository<SourceType> _sourceTypeRepository;
        private readonly IRepository<StageDetails> _stageDetailRepository;
        private readonly IMileStoneListExcelExporter _mileStoneListExcelExporter;
        public MileStoneAppService(
            IRepository<MileStone> milestoneRepository, 
            IRepository<SourceType> sourceTypeRepository, 
            IMileStoneListExcelExporter mileStoneListExcelExporter,
            IRepository<StageDetails> stageDetailRepository)
        {
            _milestoneRepository = milestoneRepository;
            _sourceTypeRepository = sourceTypeRepository;
            _mileStoneListExcelExporter = mileStoneListExcelExporter;
            _stageDetailRepository = stageDetailRepository;
        }

        public async Task<PagedResultDto<MileStoneList>> GetMileStone(GetMileStoneListInput input)
        {
            var query = _milestoneRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.MileStoneCode.Contains(input.Filter) ||
                    p.MileStoneName.Contains(input.Filter)
                );
            var MileStone = (from a in query select new MileStoneList {
                Id = a.Id,
                MileStoneCode = a.MileStoneCode,
                MileStoneName = a.MileStoneName,
                SourceTypeName = a.SourceTypes.SourceTypeName,
                RottingPeriod = a.RottingPeriod,
                IsQuotation = a.IsQuotation
            });
            var MileStoneCount = await MileStone.CountAsync();
            var MileStonelist = await MileStone
              .OrderBy(p => p.CreationTime)
              .PageBy(input)
              .ToListAsync();

            if (input.Sorting != "MileStoneCode,MileStoneName")
            {
                MileStonelist = await MileStone
                                .OrderBy(input.Sorting)
                                .PageBy(input)
                                .ToListAsync();
            }
          
            var MileStonelistoutput = MileStonelist.MapTo<List<MileStoneList>>();
            return new PagedResultDto<MileStoneList>(
                MileStoneCount, MileStonelistoutput);
        }

        public async Task<GetMileStone> GetMileStoneForEdit(NullableIdDto input)
        {
            var output = new GetMileStone { };
            var query = _milestoneRepository
               .GetAll().Where(p => p.Id == input.Id);

            var sources = await _sourceTypeRepository.GetAll().Select(p => new SourceTypees { SourceTypeId = p.Id, SourceTypeName = p.SourceTypeName }).ToArrayAsync();

            var Stages = await _stageDetailRepository.GetAll().Where(p => p.MileStoneId == input.Id).Select(p => new StageDetailListDto
            {
                Id = p.Id,
                StageId = p.StageId,
                StageName = p.EnquiryStatuss.EnqStatusName,
                Value = p.EnquiryStatuss.Percentage
            }).ToArrayAsync();

            if (query.Count() > 0)
            {
                var MileStone = (from a in query select new MileStoneList {
                    Id = a.Id,
                    MileStoneCode = a.MileStoneCode,
                    SourceTypeId = a.SourceTypes.Id,
                    SourceTypeName = a.SourceTypes.SourceTypeName,
                    MileStoneName = a.MileStoneName,
                    RottingPeriod = a.RottingPeriod,
                    IsQuotation = a.IsQuotation,
                    ResetActivity = a.ResetActivity
                }).FirstOrDefault();

                output = new GetMileStone
                {
                    MileStones = MileStone,
                    SourceTyped = sources,
                    Stages = Stages
                };
            }
            else
            {
                output = new GetMileStone
                {
                    SourceTyped = sources
                };
            }
            return output;
        }

        public virtual async Task CreateMileStoneStage(MileStoneDetailInput input)
        {

            var stageDetails = input.MapTo<StageDetails>();
            var val = _stageDetailRepository
              .GetAll().Where(p =>  p.StageId == input.StageId).FirstOrDefault();

            if (val == null)
            {
                await _stageDetailRepository.InsertAsync(stageDetails);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data already added To the MileStone Name ...");
            }

        }

        public async Task GetDeleteMileStoneDetail(EntityDto input)
        {
          await _stageDetailRepository.DeleteAsync(input.Id);              
        }

        public async Task CreateOrUpdateMileStone(CreateMileStoneInput input)
        {
            if (input.Id != 0)
            {
                await UpdateMileStoneAsync(input);
            }
            else
            {
                await CreateMileStoneAsync(input);
            }
        }

        public virtual async Task CreateMileStoneAsync(CreateMileStoneInput input)
        {
            var mileStone = input.MapTo<MileStone>();
            var val = _milestoneRepository
              .GetAll().Where(p => p.MileStoneName == input.MileStoneName || p.MileStoneCode == input.MileStoneCode).FirstOrDefault();

            if (val == null)
            {
                await _milestoneRepository.InsertAsync(mileStone);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileStone Name '" + input.MileStoneName + "' or MileStone Code '" + input.MileStoneCode + "'...");
            }
        }

        public virtual async Task UpdateMileStoneAsync(CreateMileStoneInput input)
        {
            var mileStone = await _milestoneRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, mileStone);

            var val = _milestoneRepository
             .GetAll().Where(p => (p.MileStoneName == input.MileStoneName || p.MileStoneCode == input.MileStoneCode) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _milestoneRepository.UpdateAsync(mileStone);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileStone Name '" + input.MileStoneName + "' or MileStone Code '" + input.MileStoneCode + "'...");
            }
        }

        public async Task GetDeleteMileStone(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 6);
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
                    throw new UserFriendlyException("Ooops!", "Milestone cannot be deleted '");
                }
                else
                {
                    await _milestoneRepository.DeleteAsync(input.Id);
                }
            }
            
        }


        //public bool GetMappedMileStone(EntityDto input)
        //{
        //    bool ok = false;
        //    ConnectionAppService db = new ConnectionAppService();
        //    DataTable ds = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
        //    {
        //        SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
        //        sqlComm.Parameters.AddWithValue("@TableId", 6);
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
        public async Task<FileDto> GetMileStoneToExcel()
        {

            var milestone = _milestoneRepository.GetAll();
           
            var milestones = (from a in milestone  select new MileStoneList
            {
                Id = a.Id,
                MileStoneCode = a.MileStoneCode,
                MileStoneName = a.MileStoneName,
                SourceTypeName = a.SourceTypes.SourceTypeName,
                RottingPeriod = a.RottingPeriod,
                IsQuotation = a.IsQuotation
            });
            var order = await milestones.OrderBy("MileStoneName").ToListAsync();

            var MileStonelistoutput = order.MapTo<List<MileStoneList>>();

            return _mileStoneListExcelExporter.ExportToFile(MileStonelistoutput);
        }
    }
}
