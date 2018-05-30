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
using tibs.stem.NewCustomerTypes.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using tibs.stem.Tenants.Dashboard;
using System.Data.SqlClient;
using tibs.stem.Countrys;
using Abp.UI;
using System.Data;
using tibs.stem.NewCompanyContacts.Dto;
using System.Diagnostics;

namespace tibs.stem.NewCustomerTypes
{
    public class NewCustomerTypeAppService : stemAppServiceBase, INewCustomerTypeAppService
    {
        private readonly IRepository<NewCustomerType> _newCustomerTypeRepository;
        private readonly IRepository<NewCompany> _NewCompanyRepository;
        private readonly IRepository<NewContact> _NewContactRepository;
        public NewCustomerTypeAppService(
            IRepository<NewCustomerType> newCustomerTypeRepository,
            IRepository<NewCompany> NewCompanyRepository,
            IRepository<NewContact> NewContactRepository
            )
        {
            _newCustomerTypeRepository = newCustomerTypeRepository;
            _NewCompanyRepository = NewCompanyRepository;
            _NewContactRepository = NewContactRepository;
        }

        public async Task<PagedResultDto<NewCustomerTypeListDto>> GetNewCustomerType(GetNewCustomerTypeInput input)
        {
            var query = _newCustomerTypeRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.Title.Contains(input.Filter) ||
                     p.Company.ToString().Contains(input.Filter)
                );
            var inquiry = (from a in query
                           select new NewCustomerTypeListDto
                           {
                               Id = a.Id,
                               Title = a.Title,
                               Company = (bool)a.Company

                           });

            var inquiryCount = inquiry.Count();

            var inquirylist = await inquiry
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var inquirylistoutput = inquirylist.MapTo<List<NewCustomerTypeListDto>>();
            return new PagedResultDto<NewCustomerTypeListDto>(
                inquiryCount, inquirylistoutput);
        }

        public async Task<GetNewCustomerType> GetNewCustomerTypeForEdit(NullableIdDto input)
        {
            var output = new GetNewCustomerType();
            var query = _newCustomerTypeRepository
               .GetAll().Where(p => p.Id == input.Id);
            var inquiry = (from a in query
                           select new NewCustomerTypeListDto
                           {
                               Id = a.Id,
                               Title = a.Title,
                               Company = (bool)a.Company
                           }).FirstOrDefault();


            output = new GetNewCustomerType
            {
                NewCustomerTypes = inquiry
            };


            return output;
        }


        public async Task CreateOrUpdateNewCustomerType(NewCustomerTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateInquiryAsync(input);
            }
            else
            {
                await CreateInquiryAsync(input);
            }
        }

        public virtual async Task CreateInquiryAsync(NewCustomerTypeInputDto input)
        {
            var query = input.MapTo<NewCustomerType>();

            try
            {
                await _newCustomerTypeRepository.InsertAsync(query);
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

        }

        public virtual async Task UpdateInquiryAsync(NewCustomerTypeInputDto input)
        {
            var query = await _newCustomerTypeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, query);
            await _newCustomerTypeRepository.UpdateAsync(query);
        }

        public async Task GetDeleteNewCustomerType(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 9);
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
                    throw new UserFriendlyException("Ooops!", "Customer cannot be deleted '");
                }
                else
                {
                    await _newCustomerTypeRepository.DeleteAsync(input.Id);
                }

            }
        }
    }
}
