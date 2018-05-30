using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.NewInfoTypes.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using tibs.stem.Tenants.Dashboard;
using System.Data.SqlClient;
using tibs.stem.Countrys;
using Abp.UI;
using System.Data;

namespace tibs.stem.NewInfoTypes
{
    public class NewInfoTypeAppService : stemAppServiceBase, INewInfoTypeAppService
    {
        private readonly IRepository<NewInfoType> _newInfotypeRepository;
        public NewInfoTypeAppService(IRepository<NewInfoType> newInfotypeRepository)
        {
            _newInfotypeRepository = newInfotypeRepository;


        }

        public async Task<PagedResultDto<NewInfoTypeListDto>> GetNewInfoType(GetNewInfoTypeInput input)
        {
            var query = _newInfotypeRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.ContactName.Contains(input.Filter) ||
                     p.Info.ToString().Contains(input.Filter)
                );
            var inquiry = (from a in query
                           select new NewInfoTypeListDto
                           {
                               Id = a.Id,
                               ContactName = a.ContactName,
                               Info = (bool)a.Info

                           });

            var inquiryCount = inquiry.Count();

            var inquirylist = await inquiry
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var inquirylistoutput = inquirylist.MapTo<List<NewInfoTypeListDto>>();
            return new PagedResultDto<NewInfoTypeListDto>(
                inquiryCount, inquirylistoutput);
        }

        public async Task<GetNewInfoType> GetNewInfoTypeForEdit(NullableIdDto input)
        {
            var output = new GetNewInfoType();
            var query = _newInfotypeRepository
               .GetAll().Where(p => p.Id == input.Id);
            var inquiry = (from a in query
                           select new NewInfoTypeListDto
                           {
                               Id = a.Id,
                               ContactName = a.ContactName,
                               Info = (bool)a.Info
                           }).FirstOrDefault();


            output = new GetNewInfoType
            {
                NewInfoTypes = inquiry
            };


            return output;
        }


        public async Task CreateOrUpdateNewInfoType(NewInfoTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateNewInfoTypeAsync(input);
            }
            else
            {
                await CreateNewInfoTypeAsync(input);
            }
        }

        public virtual async Task CreateNewInfoTypeAsync(NewInfoTypeInputDto input)
        {
            var query = input.MapTo<NewInfoType>();

            try
            {
                await _newInfotypeRepository.InsertAsync(query);
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

        }

        public virtual async Task UpdateNewInfoTypeAsync(NewInfoTypeInputDto input)
        {
            var query = await _newInfotypeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, query);
            await _newInfotypeRepository.UpdateAsync(query);
        }

        public async Task GetDeleteNewInfoType(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 8);
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

                    throw new UserFriendlyException("Ooops!", "newinfo cannot be deleted '");
                }
                else
                {
                    await _newInfotypeRepository.DeleteAsync(input.Id);
                }
            }
        }

    }
    }
