
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.UI;
using System.Data.SqlClient;
using tibs.stem;
using tibs.stem.Citys;
using tibs.stem.CustomerTypes;
using tibs.stem.CompanyContacts;
using tibs.stem.TitleOfCourtes;
using tibs.stem.Designations;
using tibs.stem.Companies.Dto;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Countrys;
using tibs.stem.Authorization;
using tibs.stem.Locations;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Companies 
{
    public class CompanyAppService : stemAppServiceBase, ICompanyAppService
    {
        private readonly IRepository<Company> _CompanyRepository;
        private readonly IRepository<City> _CityRepository;
        public readonly IRepository<CustomerType> _CustomerTypeRepository;
        private readonly IRepository<CompanyContact> _CompanyContactRespository;
        private readonly IRepository<TitleOfCourtesy> _TitleRepository;
        private readonly IRepository<Designation> _DesiginationRepository;

        public CompanyAppService(IRepository<Company> CompanyRepository, IRepository<City> CityRepository, IRepository<CustomerType> CustomerTypeRepository,
            IRepository<CompanyContact> CompanyContactRespository, IRepository<TitleOfCourtesy> TitleRepository, IRepository<Designation> DesiginationRepository)
        {
            _CompanyRepository = CompanyRepository;
            _CityRepository = CityRepository;
            _CustomerTypeRepository = CustomerTypeRepository;
            _CompanyContactRespository = CompanyContactRespository;
            _DesiginationRepository = DesiginationRepository;
            _TitleRepository = TitleRepository;
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company)]
        public async Task<PagedResultDto<CompanyViewDt>> GetCompanies(GetCompanyInput input)
        {
            var query = _CompanyRepository.GetAll()
                       .WhereIf(
                      !input.Filter.IsNullOrWhiteSpace(),
                      p => p.CompanyCode.Contains(input.Filter) ||
                          p.CompanyName.Contains(input.Filter) ||
                          p.Address.Contains(input.Filter) ||
                          p.Email.Contains(input.Filter) ||
                          p.Fax.Contains(input.Filter) ||
                          p.Cities.CityName.Contains(input.Filter) ||
                          p.CustomerTypes.CustomerTypeName.Contains(input.Filter));
            var companycount = await query.CountAsync();
            var companydto = from c in query select new CompanyViewDt { Id = c.Id, CompanyCode = c.CompanyCode, CompanyName = c.CompanyName, City = c.Cities.CityName, Address = c.Address, Country = c.Cities.Country.CountryName, Fax = c.Fax, TelNo = c.TelNo, CustomerType = c.CustomerTypes.CustomerTypeName, Phone = c.PhoneNo, Email = c.Email, AccountManager = c.AbpAccountManager.UserName };
            var companydtos = await companydto.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var companydtoss = companydtos.MapTo<List<CompanyViewDt>>();
            return new PagedResultDto<CompanyViewDt>(companycount, companydtoss);
        }

        public async Task<GetCompany> GetCompanyForEdit(NullableIdDto input)
        {
            var output = new GetCompany { };
            var company = _CompanyRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var customertype = await _CustomerTypeRepository.GetAll().Select(p => new CustomerTypeDto { CustomerTypeId = p.Id, CustomerTypeName = p.CustomerTypeName }).ToArrayAsync();
            var city = await _CityRepository.GetAll().Where(p => p.Id == 0).Select(p => new CityDto { CityId = p.Id, CityName = p.CityName }).ToArrayAsync();
            if (company != null)
            {
                city = await _CityRepository.GetAll().Where(p => p.Id == company.CityId).Select(p => new CityDto { CityId = p.Id, CityName = p.CityName }).ToArrayAsync();
                var account = await (from r in UserManager.Users.Where(p => p.Id == company.AccountManagerId) select new AccountManagerDto { AccountManagerId = r.Id, AccountManagerName = r.UserName }).ToArrayAsync();
                output = new GetCompany { City = city, CustomerType = customertype, AccountManager = account };
            }
            else
            {
                output = new GetCompany { City = city, CustomerType = customertype };
            }

            output.Company = company.MapTo<CreateCompanyInput>();
            return output;
        }

        public async Task CreateOrUpdateCompany(CreateCompanyInput input)
        {
            if (input.Id == 0)
            {
                await CreateCompany(input);
            }
            else
            {
                await UpdateCompany(input);

            }
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Create)]
        public virtual async Task CreateCompany(CreateCompanyInput input)
        {
            if (input.CustomerTypeId == 0)
            {
                input.CustomerTypeId = 1;
            }
            var company = input.MapTo<Company>();
            var maxId = (_CompanyRepository.GetAll().Select(x => (int?)x.Id).Max() ?? 0) + 1;
            var id1 = "MEA" + maxId;
            company.CompanyCode = id1;
            await _CompanyRepository.InsertAsync(company);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Edit)]
        public virtual async Task UpdateCompany(CreateCompanyInput input)
        {
            if (input.CustomerTypeId == 0)
            {
                input.CustomerTypeId = 1;
            }
            var company = await _CompanyRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, company);

            await _CompanyRepository.UpdateAsync(company);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Company_Delete)]
        public async Task DeleteCompany(EntityDto input)
        {

                    await _CompanyRepository.DeleteAsync(input.Id);

        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact)]
        public ListResultDto<ContactViewDto> GetContacts(NullableIdDto input)
        {
            var query = _CompanyContactRespository.GetAll().Where(p => p.CompanyId == input.Id);

            var contactdto = from c in query
                             select new ContactViewDto
                             {
                                 CompanyName = c.Companies.CompanyName,
                                 ContactPersonName = c.TitleOfCourtesies.Name + "." + c.ContactPersonName,
                                 Address = c.Address,
                                 Desigination = c.DesiginationId != null ? c.Desiginations.DesiginationName : "",
                                 Mobile_No = c.Mobile_No,
                                 Work_No = c.Work_No,
                                 Description = c.Description,
                                 Email = c.Email,
                                 Id = c.Id
                             };

            return new ListResultDto<ContactViewDto>(contactdto.MapTo<List<ContactViewDto>>());

        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact)]
        public async Task<PagedResultDto<ContactViewDto>> GetAllContacts(GetContactInput input)
        {
            var query1 = _CompanyContactRespository.GetAll();

            var query = _CompanyContactRespository.GetAll()
                       .WhereIf(
                       !input.Filter.IsNullOrWhiteSpace(),
                       p => p.Companies.CompanyName.Contains(input.Filter) ||
                       p.Desiginations.DesiginationName.Contains(input.Filter) ||
                       p.Email.Contains(input.Filter) ||
                       p.Mobile_No.Contains(input.Filter) ||
                       p.TitleOfCourtesies.Name.Contains(input.Filter) ||
                       p.Work_No.Contains(input.Filter) ||
                       p.ContactPersonName.Contains(input.Filter));

            var contactdto = from c in query
                             select new ContactViewDto
                             {
                                 CompanyName = c.Companies.CompanyName,
                                 ContactPersonName = c.TitleOfCourtesies.Name + "." + c.ContactPersonName,
                                 Address = c.Address,
                                 Desigination = c.DesiginationId != null ? c.Desiginations.DesiginationName : "",
                                 Mobile_No = c.Mobile_No,
                                 Work_No = c.Work_No,
                                 Description = c.Description,
                                 Email = c.Email,
                                 Id = c.Id,
                                 CompanyId = c.CompanyId,
                                 CreatorUserId = (int)(c.CreatorUserId != null ? c.CreatorUserId : 0)

                             };
            var contactdtos = contactdto.OrderBy(input.Sorting).PageBy(input);


            var contactcount = await contactdto.CountAsync();
            return new PagedResultDto<ContactViewDto>(contactcount, contactdtos.MapTo<List<ContactViewDto>>());
        }

        public async Task<GetCompanyContact> GetContactForInput(NullableIdDto input)
        {
            var Title = await _TitleRepository.GetAll().Select(p => new TitleDto { Title = p.Name, TitleId = p.Id }).ToArrayAsync();
            var desination = await _DesiginationRepository.GetAll().Select(p => new DesiginationDto { DesiginationId = p.Id, Desigination = p.DesiginationName }).ToArrayAsync();
            var company = await _CompanyRepository.GetAll().Select(p => new CompanyDto { CompanyId = p.Id, CompanyName = p.CompanyName }).ToArrayAsync();
            var output = new GetCompanyContact { Title = Title, Desigination = desination, Company = company };
            var contact = _CompanyContactRespository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.Contact = contact.MapTo<CreateContactInput>();
            return output;
        }
        public async Task CreateOrUpdateContact(CreateContactInput input)
        {
            if (input.Id == 0)
            {
                await CraeteContact(input);

            }
            else
            {
                await UpdateContact(input);

            }
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact_Create)]
        public virtual async Task CraeteContact(CreateContactInput input)
        {
            var contact = input.MapTo<CompanyContact>();
            await _CompanyContactRespository.InsertAsync(contact);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact_Edit)]
        public virtual async Task UpdateContact(CreateContactInput input)
        {
            var contact = await _CompanyContactRespository.GetAsync(input.Id);
            ObjectMapper.Map(input, contact);
            await _CompanyContactRespository.UpdateAsync(contact);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_AddressBook_Contact_Delete)]
        public async Task DeleteContact(EntityDto input)
        {
            //using (var context = new SpDbContext())
            //{

            //    //try
            //    //{
            //    var idd = new SqlParameter
            //    {
            //        ParameterName = "TableId",
            //        Value = 5
            //    };
            //    var list = context.Database.SqlQuery<DeleteData>("exec Sp_DeleteData @TableId", idd).ToList();

            //    var count = list.Where(f => f.id == input.Id).ToList();
            //    if (count.Count() <= 0)
            //    {
            //        var id = new SqlParameter
            //        {
            //            ParameterName = "Id",
            //            Value = input.Id
            //        };
            //        context.Database.SqlQuery<Resultt>("exec Sp_CompanyContact @Id", id).FirstOrDefault();
            //        //await _CompanyContactRespository.DeleteAsync(input.Id);
            //    }
            //    else
            //    {
            //        throw new UserFriendlyException("Unable to Delete", "Data is being used by another Field");

            //    }
            //}  
            await _CompanyContactRespository.DeleteAsync(input.Id);
        }
        public int CompanyCreate(CompanyCreateInput input)
        {
            int id = 0;
            //var city = _CityRepository.GetAll().Where(p => p.Id == input.CountryId).FirstOrDefault();
            //var maxId = (_CompanyRepository.GetAll().Select(x => (int?)x.Id).Max() ?? 0) + 1;
            //var id1 = "MEA" + maxId;
            //try
            //{
            //    using (var context = new SpDbContext())
            //    {

            //        var countryid = new SqlParameter
            //        {
            //            ParameterName = "CountryId",
            //            Value = city.Id
            //        };
            //        var CompanyName = new SqlParameter
            //        {
            //            ParameterName = "CompanyName",
            //            Value = input.CompanyName
            //        };
            //        var CompanyCode = new SqlParameter
            //        {
            //            ParameterName = "CompanyCode",
            //            Value = id1
            //        };
            //        var list = context.Database.SqlQuery<Result>("exec Sp_CreateCompany @CountryId,@CompanyName,@CompanyCode", countryid, CompanyName, CompanyCode).FirstOrDefault();
            //        id = list.id;
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
            return id;

        }
        public int NewCompanyCreate(CompanyCreateInput input)
        {
            int id = 0;
            //CreateCompanyInput data = new CreateCompanyInput();
            //var city = _CityRepository.GetAll().Where(p => p.CountryId == input.CountryId).FirstOrDefault();
            //id = (_CompanyRepository.GetAll().Select(x => (int?)x.Id).Max() ?? 0) + 1;
            //var id1 = "MEA" + id;
            //data.CompanyName = input.CompanyName;
            //data.CompanyCode = id1;
            //data.CityId = city.Id;
            //data.CustomerTypeId = 1;
            //data.PhoneNo = "0000";
            //data.Email = "abc@gmail.com";
            //data.Fax = "0000";
            //data.Mob_No = "0000";
            //data.Address = " Auto Generated From Enquiry";
            //data.AccountManagerId = 2;
            //var company = data.MapTo<Company>();
            //_CompanyRepository.InsertAsync(company);
            return id;
        }
      
      
    }
    public class Result
    {
        public int id { get; set; }
    }
}
