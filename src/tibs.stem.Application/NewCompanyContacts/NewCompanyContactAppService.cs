using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.NewCompanyContacts.Dto;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Countrys;
using tibs.stem.EnquiryContacts;
using System.Text.RegularExpressions;
using tibs.stem.Emaildomains;
using tibs.stem.Discounts;
using tibs.stem.Team;
using tibs.stem.TeamDetails;
using Abp.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using tibs.stem.Authorization.Roles;

namespace tibs.stem.NewCompanyContacts
{
    public class NewCompanyContactAppService : stemAppServiceBase, INewCompanyContactAppService
    {
        private readonly IRepository<NewCustomerType> _NewCustomerTypeRepository;
        private readonly IRepository<NewCompany> _NewCompanyRepository;
        private readonly IRepository<NewContact> _NewContactRepository;
        private readonly IRepository<NewInfoType> _NewInfoTypeRepository;
        private readonly IRepository<NewAddressInfo> _NewAddressInfoRepository;
        private readonly IRepository<NewContactInfo> _NewContactInfoRepository;
        private readonly IRepository<EnquiryContact> _enquiryContactRepository;
        private readonly IRepository<Emaildomain> _EmaildomainRepository;
        private readonly IRepository<Discount> _DiscountRepository;
        private readonly IRepository<Teams> _TeamRepository;
        private readonly IRepository<TeamDetail> _TeamDetailRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly RoleManager _roleManager;
        public NewCompanyContactAppService(
            IRepository<NewContactInfo> NewContactInfoRepository, 
            IRepository<NewAddressInfo> NewAddressInfoRepository, 
            IRepository<NewInfoType> NewInfoTypeRepository, 
            IRepository<NewContact> NewContactRepository, 
            IRepository<NewCustomerType> NewCustomerTypeRepository, 
            IRepository<NewCompany> NewCompanyRepository,
            IRepository<Emaildomain> EmaildomainRepository,
            IRepository<Discount> DiscountRepository,
            IRepository<Teams> TeamRepository, 
            IRepository<TeamDetail> TeamDetailRepository,
            IRepository<UserRole, long> userRoleRepository,
            RoleManager roleManager,
            IRepository<EnquiryContact> enquiryContactRepository)
        {
            _NewCustomerTypeRepository = NewCustomerTypeRepository;
            _NewCompanyRepository = NewCompanyRepository;
            _NewContactRepository = NewContactRepository;
            _NewInfoTypeRepository = NewInfoTypeRepository;
            _NewAddressInfoRepository = NewAddressInfoRepository;
            _NewContactInfoRepository = NewContactInfoRepository;
            _enquiryContactRepository = enquiryContactRepository;
            _EmaildomainRepository = EmaildomainRepository;
            _DiscountRepository = DiscountRepository;
            _TeamRepository = TeamRepository;
            _TeamDetailRepository = TeamDetailRepository;
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManager;
        }
        public async Task<Array> GetNewCompanyForEdit(NullableIdDto input)
        {
            var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.NewCompanyId == input.Id);

            var contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == input.Id);

            var SubListout = new List<GetNewCompany>();


            var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

             if(comp != null)            
             {
                SubListout.Add(new GetNewCompany {
                    Id = comp.Id,
                    Name = comp.Name,
                    CustomerId = comp.CustomerId,
                    NewCustomerTypeId = comp.NewCustomerTypeId,
                    AccountManagerId = comp.AccountManagerId,
                    ApprovedById = comp.ApprovedById ?? 0,
                    CreatorUserId = comp.CreatorUserId ?? 0,
                    IsApproved = comp.IsApproved,
                    TradeLicense = comp.TradeLicense,
                    TRNnumber = comp.TRNnumber,
                    IndustryId = comp.IndustryId,
                    Discountable = comp.Discountable,
                    UnDiscountable = comp.UnDiscountable,
                    AddressInfo = (from r in Addinfo select new CreateAddressInfo {
                        Id = r.Id,
                        NewCompanyId = r.NewCompanyId ?? 0,
                        NewContacId = r.NewContacId ?? 0,
                        NewInfoTypeId = r.NewInfoTypeId,
                        Address1 = r.Address1,
                        Address2 = r.Address2,
                        CityId = r.CityId,
                        CountryName = r.Citys.Country.CountryName
                    }).ToArray(),

                    Contactinfo = (from r in contactinfo select new CreateContactInfo {
                        Id = r.Id,
                        NewCompanyId = r.NewCompanyId ?? 0,
                        NewContacId = r.NewContacId ?? 0,
                        NewInfoTypeId = r.NewInfoTypeId,
                        InfoData = r.InfoData                     
                  }).ToArray()
                });

            }
            var CompanyArray = SubListout.ToArray();
             foreach (var data in CompanyArray)
            {
                if(data.ApprovedById > 0)
                {
                    var app = await UserManager.GetUserByIdAsync(data.ApprovedById);
                    data.ApprovedName = app.UserName;
                }
                if (data.CreatorUserId > 0)
                {
                    var user = await UserManager.GetUserByIdAsync(data.CreatorUserId);
                    data.UserName = user.UserName;
                }
            }

            return CompanyArray;
       }
        public async Task<Array> GetNewContactForEdit(NullableIdDto input)
        {
            var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.NewContacId == input.Id);

            var contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == input.Id);

            var SubListout = new List<GetNewContacts>();

            var comp = _NewContactRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            if (comp != null)
            {
                var CompanyName = "";
                if(comp.NewCompanyId > 0)
                {
                    var company = _NewContactRepository.GetAll().Where(p => p.Id == comp.Id).Select(p => p.NewCompanys).FirstOrDefault();
                    CompanyName = company.Name;
                }


                SubListout.Add(new GetNewContacts
                {
                    Id = comp.Id,
                    Name = comp.Name,
                    NewCustomerTypeId = comp.NewCustomerTypeId,
                    CompanyId = comp.NewCompanyId ?? 0,
                    CompanyName = CompanyName,
                    TitleId = comp.TitleId,
                    LastName = comp.LastName,
                    DesignationId = comp.DesignationId,
                    AddressInfo = (from r in Addinfo
                                   select new CreateAddressInfo
                                   {
                                       Id = r.Id,
                                       NewCompanyId = r.NewCompanyId != null ? r.NewCompanyId : 0,
                                       NewContacId = r.NewContacId != null ? r.NewContacId : 0,
                                       NewInfoTypeId = r.NewInfoTypeId,
                                       Address1 = r.Address1,
                                       Address2 = r.Address2,
                                       CityId = r.CityId,
                                       CountryName = r.Citys.Country.CountryName

                                   }).ToArray(),

                    Contactinfo = (from r in contactinfo
                                   select new CreateContactInfo
                                   {
                                       Id = r.Id,
                                       NewCompanyId = r.NewCompanyId != null ? r.NewCompanyId : 0,
                                       NewContacId = r.NewContacId != null ? r.NewContacId : 0,
                                       NewInfoTypeId = r.NewInfoTypeId,
                                       InfoData = r.InfoData

                                   }).ToArray()
                });

            }

            return SubListout.ToArray();
        }

        //public async Task<PagedResultDto<NewCompanyListDto>> GetCompanys(GetCompanyInput input)
        //{
        //    var query = _NewCompanyRepository.GetAll()
        //      .WhereIf(
        //      !input.Filter.IsNullOrEmpty(),
        //      p => p.Name.Contains(input.Filter) ||
        //          p.NewCustomerTypes.Title.Contains(input.Filter)
        //      );
        //    var company = (from a in query select new NewCompanyListDto {
        //        Id = a.Id,
        //        CompanyName = a.Name,
        //        CustomerId = a.CustomerId,
        //        CustomerTypeName = a.NewCustomerTypes.Title,
        //        NewCustomerTypeId = a.NewCustomerTypeId,
        //        AccountManagerId = a.AccountManagerId,
        //        ManagedBy = a.AbpAccountManager.UserName,
        //        ApprovedById = a.ApprovedById,
        //        ApprovedBy = a.AbpApprovedBy.UserName,
        //        IsApproved = a.IsApproved,
        //        CreatedBy = "",
        //        CreationTime = a.LastModificationTime ?? a.CreationTime,
        //        CreatedUserId = a.CreatorUserId ?? 0 ,
        //        TRNnumber = a.TRNnumber,
        //        TradeLicense = a.TradeLicense,
        //        IndustryName = a.IndustryId != null ? a.Industries.IndustryName : "",
        //        Discountable = a.Discountable,
        //        UnDiscountable = a.UnDiscountable
        //    });
        //    var companyCount = await company.CountAsync();
        //    var companylist = await company.ToListAsync();



        //    if (input.Sorting == "CompanyName,CustomerTypeName,ManagedBy,ApprovedBy,CustomerId,IsApproved,CreatedBy")
        //    {
        //        companylist = await company
        //        .OrderBy(a => a.CreationTime)
        //       .PageBy(input)
        //       .ToListAsync();
        //    }
        //    else
        //    {
        //       companylist = await company
        //      .OrderBy(input.Sorting)
        //      .PageBy(input)
        //      .ToListAsync();
        //    }

        //    foreach (var r in companylist)
        //    {
        //        if (r.CreatedUserId > 0)
        //        {
        //            var user = await UserManager.GetUserByIdAsync(r.CreatedUserId);
        //            r.CreatedBy = user.UserName;
        //        }

        //    }
        //    var companylistoutput = companylist.MapTo<List<NewCompanyListDto>>();

        //    return new PagedResultDto<NewCompanyListDto>(
        //        companyCount, companylistoutput);
        //}
        public async Task<PagedResultDto<NewCompanyListDto>> GetCompanys(GetCompanyInput input)
        {

            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _NewCompanyRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from comp in _NewCompanyRepository.GetAll() where comp.AccountManagerId == userid select comp);
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {

                query = (from com in _NewCompanyRepository.GetAll()
                         join q in _TeamDetailRepository.GetAll() on com.AccountManagerId equals q.SalesmanId
                         join r in _TeamRepository.GetAll() on q.TeamId equals r.Id
                         where r.SalesManagerId == userid
                         select com);

            }
            else
            {
                query = _NewCompanyRepository.GetAll();

            }

            query = query
              .WhereIf(
              !input.Filter.IsNullOrEmpty(),
              p => p.Name.Contains(input.Filter) ||
                  p.NewCustomerTypes.Title.Contains(input.Filter)
              );
            var company = (from a in query
                           select new NewCompanyListDto
                           {
                               Id = a.Id,
                               CompanyName = a.Name,
                               CustomerId = a.CustomerId,
                               CustomerTypeName = a.NewCustomerTypes.Title,
                               NewCustomerTypeId = a.NewCustomerTypeId,
                               AccountManagerId = a.AccountManagerId,
                               ManagedBy = a.AbpAccountManager.UserName,
                               ApprovedById = a.ApprovedById,
                               ApprovedBy = a.AbpApprovedBy.UserName,
                               IsApproved = a.IsApproved,
                               CreatedBy = "",
                               CreationTime = a.LastModificationTime ?? a.CreationTime,
                               CreatedUserId = a.CreatorUserId ?? 0,
                               TRNnumber = a.TRNnumber,
                               TradeLicense = a.TradeLicense,
                               IndustryName = a.IndustryId != null ? a.Industries.IndustryName : "",
                               Discountable = a.Discountable,
                               UnDiscountable = a.UnDiscountable
                           });
            var companyCount = await company.CountAsync();
            var companylist = await company.ToListAsync();



            if (input.Sorting == "CompanyName,CustomerTypeName,ManagedBy,ApprovedBy,CustomerId,IsApproved,CreatedBy")
            {
                companylist = await company
                .OrderBy(a => a.CreationTime)
               .PageBy(input)
               .ToListAsync();
            }
            else
            {
                companylist = await company
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }

            foreach (var r in companylist)
            {
                if (r.CreatedUserId > 0)
                {
                    var user = await UserManager.GetUserByIdAsync(r.CreatedUserId);
                    r.CreatedBy = user.UserName;
                }

            }
            var companylistoutput = companylist.MapTo<List<NewCompanyListDto>>();

            return new PagedResultDto<NewCompanyListDto>(
                companyCount, companylistoutput);
        }


        public async Task<PagedResultDto<NewContactListDto>> GetContacts(GetContactInput input)
        {
            var query = _NewContactRepository.GetAll()
                          .WhereIf(
                          !input.Filter.IsNullOrEmpty(),
                          p => p.Name.Contains(input.Filter) ||
                              p.NewCustomerTypes.Title.Contains(input.Filter)
                          );
            var contact = (from a in query
                           select new NewContactListDto
                           {
                               Id = a.Id,
                               ContactName = a.Name,
                               LastName = a.LastName,
                               Title = a.TitleOfCourtesies.Name,
                               CompanyName = a.NewCompanys.Name,
                               CustomerTypeName = a.NewCompanys.NewCustomerTypes.Title,
                               NewCustomerTypeId = a.NewCustomerTypeId,
                               ContactTypeName = a.NewCustomerTypes.Title,
                               DesignationId = a.DesignationId,
                               DesignationName = a.DesignationId != null ? a.Designations.DesiginationName : ""

                           });
            var contactCount = await contact.CountAsync();
            var contactlist = await contact
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var contactlistoutput = contactlist.MapTo<List<NewContactListDto>>();
            return new PagedResultDto<NewContactListDto>(
                contactCount, contactlistoutput);
        }

        public ListResultDto<NewContactListDto> GetCompanyContacts(NullableIdDto input)
        {
            var contact = _NewContactRepository
               .GetAll().Where(p => p.NewCompanyId ==input.Id);

            var data = (from a in contact select new NewContactListDto {
                Id = a.Id,
                ContactName = a.Name,
                LastName = a.LastName,
                Title = a.TitleOfCourtesies.Name,
                CompanyName = a.NewCompanys.Name,
                CustomerTypeName = a.NewCompanys.NewCustomerTypes.Title,
                NewCustomerTypeId = a.NewCustomerTypeId,
                ContactTypeName = a.NewCustomerTypes.Title,
                DesignationName = a.Designations.DesiginationName
            });

            return new ListResultDto<NewContactListDto>(data.MapTo<List<NewContactListDto>>());
        }

        public async Task<int>CreateOrUpdateCompanyOrContact(CreateCompanyOrContact input)
        {
            var id = 0;

            if (input.Id == 0 && input.NewCompanyId != null)
            {
                id = await CreateContact(input);
            }
            else if (input.Id == 0 && input.NewCompanyId == null)
            {
                id = await  CreateCompany(input);
            }
            else if (input.Id != 0 && input.NewCompanyId != null)
            {
               id =  await UpdateContact(input);

            }
            else if (input.Id != 0 && input.NewCompanyId == null && (input.NewCustomerTypeId == 3 || input.NewCustomerTypeId == 4))
            {
                id =  await UpdateContact(input);
            }
            else
            {
                id = await UpdateCompany(input);
            }

            return id;
        }
        public virtual async Task<int> CreateCompany(CreateCompanyOrContact input)
        {
            var id = 0;
            var discount = _DiscountRepository.GetAll().FirstOrDefault();
            var data = _NewCompanyRepository.GetAll().Where(p => p.Name == input.Name && p.NewCustomerTypeId == input.NewCustomerTypeId).ToList();
            if(data.Count == 0)
            {
                CreateCompany company = new CreateCompany()
                {
                    Name = input.Name,
                    NewCustomerTypeId = input.NewCustomerTypeId ?? null,
                    AccountManagerId = input.AccountManagerId ?? null,
                    CustomerId = input.CustomerId,
                    ApprovedById = AbpSession.UserId,
                    IsApproved = true,
                    TradeLicense = input.TradeLicense,
                    TRNnumber = input.TRNnumber,
                    IndustryId = input.IndustryId ?? null,
                    Discountable = discount != null ? (int)discount.Discountable : 0,
                    UnDiscountable = discount != null ? (int)discount.UnDiscountable : 0,
                };
                var companys = company.MapTo<NewCompany>();
                id = _NewCompanyRepository.InsertAndGetId(companys);


                var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();

                comp.CustomerId = "FN-" + input.Name.Substring(0, 3).ToUpper() + id;
                await _NewCompanyRepository.UpdateAsync(comp);
            }
            else
            {
                id = data[0].Id;
            }
            return id;
        }
        public virtual async Task<int> UpdateCompany(CreateCompanyOrContact input)
        {
            var data = _NewCompanyRepository.GetAll().Where(p => p.Name == input.Name && p.NewCustomerTypeId == input.NewCustomerTypeId && p.Id != input.Id).ToList();
            if (data.Count == 0)
            {
                if (input.ApprovedById == 0)
                    input.ApprovedById = null;

                CreateCompany company = new CreateCompany()
                {
                    Id = input.Id,
                    Name = input.Name,
                    NewCustomerTypeId = input.NewCustomerTypeId ?? null,
                    AccountManagerId = input.AccountManagerId ?? null,
                    CustomerId = input.CustomerId,
                    ApprovedById = input.ApprovedById ?? null,
                    IsApproved = input.IsApproved,
                    TradeLicense = input.TradeLicense,
                    TRNnumber = input.TRNnumber,
                    IndustryId = input.IndustryId ?? null,
                    Discountable = input.Discountable,
                    UnDiscountable = input.UnDiscountable
                };
                var companys = await _NewCompanyRepository.GetAsync(input.Id);
                ObjectMapper.Map(company, companys);
                await _NewCompanyRepository.UpdateAsync(companys);
            }               
            return input.Id;        
        }
        public virtual async Task<int> CreateContact(CreateCompanyOrContact input)
        {
            var id = 0;
            CreateContact contact = new CreateContact()
            {
                Name = input.Name,
                NewCustomerTypeId = input.NewCustomerTypeId ?? null,
                NewCompanyId = input.NewCompanyId ?? null,
                LastName = input.LastName,
                TitleId = input.TitleId ?? null,
                DesignationId = input.DesignationId ?? null
            };
            var contacts = contact.MapTo<NewContact>();
            id = _NewContactRepository.InsertAndGetId(contacts);
            return id;
           
        }
        public virtual async Task<int> UpdateContact(CreateCompanyOrContact input)
        {
            if (input.DesignationId == 0)
                input.DesignationId = null;

            CreateContact contact = new CreateContact()
            {
                Id = input.Id,
                Name = input.Name,
                NewCustomerTypeId = input.NewCustomerTypeId ?? null,
                NewCompanyId = input.NewCompanyId ?? null,
                LastName = input.LastName,
                TitleId = input.TitleId ?? null,
                DesignationId = input.DesignationId ?? null
            };
            var contacts = await _NewContactRepository.GetAsync(input.Id);
            ObjectMapper.Map(contact, contacts);
            await _NewContactRepository.UpdateAsync(contacts);
            return input.Id;   
          
        }
        public virtual async Task<int> ContactUpdate(CreateCompanyOrContact input)
        {
            CreateContact contact = new CreateContact()
            {
                Id = input.Id,
                Name = input.Name,
                NewCustomerTypeId = input.NewCustomerTypeId ?? null,
                NewCompanyId = input.NewCompanyId ?? null,
                LastName = input.LastName,
                TitleId = input.TitleId ?? null,
                DesignationId = input.DesignationId ?? null
            };
            var contacts = await _NewContactRepository.GetAsync(input.Id);
            ObjectMapper.Map(contact, contacts);
            await _NewContactRepository.UpdateAsync(contacts);
            return input.Id;

        }

        public async Task CreateOrUpdateAddressInfo(CreateAddressInfo input)
        {
            if (input.Id == 0)
            {
                await CreateAddressInfo(input);
            }
            else
            {
                await UpdateAddressInfo(input);

            }
        }

        public virtual async Task CreateAddressInfo(CreateAddressInfo input)
        {
            var AddressInfo = input.MapTo<NewAddressInfo>();
            await _NewAddressInfoRepository.InsertAsync(AddressInfo);
        }
        public virtual async Task UpdateAddressInfo(CreateAddressInfo input)
        {
            var AddressInfo = await _NewAddressInfoRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, AddressInfo);
            await _NewAddressInfoRepository.UpdateAsync(AddressInfo);
        }

        public async Task CreateOrUpdateContactInfo(CreateContactInfo input)
        {
            if (input.Id == 0)
            {
                await CreateContactInfo(input);
            }
            else
            {
                await UpdateContactInfo(input);

            }
        }

        public virtual async Task CreateContactInfo(CreateContactInfo input)
        {
            try
            {
            var ContactInfo = input.MapTo<NewContactInfo>();
            if (string.IsNullOrEmpty(input.InfoData) == false && input.NewInfoTypeId > 0)
            {
                if (input.NewInfoTypeId == 4)
                {
                    var currentDomain = Regex.Match(input.InfoData, "@.*").Value;
                    if (input.NewCompanyId > 0)
                    {
                        var query = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.NewCompanyId > 0 && p.NewCompanyId != input.NewCompanyId && Regex.Match(p.InfoData, "@.*").Value == currentDomain).ToList();
                        if (query.Count() > 0)
                        {
                            var Ignoredomain = _EmaildomainRepository.GetAll().Where(p => p.EmaildomainName == currentDomain.Substring(1, currentDomain.Length - 1)).FirstOrDefault();
                            if (Ignoredomain != null)
                            {
                                var query1 = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.InfoData == input.InfoData).FirstOrDefault();
                                if (query1 == null)
                                {
                                    await _NewContactInfoRepository.InsertAsync(ContactInfo);
                                }
                                else
                                {
                                    //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                                }
                            }
                            else
                            {
                                //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Email Domain '" + currentDomain + "'...");
                            }
                        }
                        else
                        {
                            var dup = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.NewCompanyId == input.NewCompanyId && p.InfoData == input.InfoData).FirstOrDefault();
                            if (dup == null)
                            {
                                await _NewContactInfoRepository.InsertAsync(ContactInfo);
                            }
                            else
                            { }
                               // throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                        }
                    }
                    if (input.NewContacId > 0)
                    {
                        var cmp = _NewContactRepository.GetAll().Where(p => p.Id == input.NewContacId).FirstOrDefault();
                        var companyId = cmp.NewCompanyId;
                        var query = (from ci in _NewContactInfoRepository.GetAll()
                                     join c in _NewContactRepository.GetAll() on ci.NewContacId equals c.Id
                                     where ci.NewInfoTypeId == 4 && ci.NewContacId > 0 && c.NewCompanyId != companyId && Regex.Match(ci.InfoData, "@.*").Value == currentDomain
                                     select ci).ToList();
                        // var query = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.NewContacId > 0 && p.NewContacId != input.NewContacId && Regex.Match(p.InfoData, "@.*").Value == currentDomain).ToList();
                        if (query.Count() > 0)
                        {
                            var Ignoredomain = _EmaildomainRepository.GetAll().Where(p => p.EmaildomainName == currentDomain.Substring(1, currentDomain.Length - 1)).FirstOrDefault();
                            if (Ignoredomain != null)
                            {
                                var query1 = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.InfoData == input.InfoData).FirstOrDefault();
                                if (query1 == null)
                                {
                                    await _NewContactInfoRepository.InsertAsync(ContactInfo);
                                }
                                else
                                {
                                   // throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                                }
                            }
                            else
                            {
                                //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Email Domain '" + currentDomain + "'...");
                            }
                        }
                        else
                        {
                            var dup = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.InfoData == input.InfoData).FirstOrDefault();
                            if (dup == null)
                            {
                                await _NewContactInfoRepository.InsertAsync(ContactInfo);
                            }
                            else
                            { }
                               // throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                        }
                    }
                }
                else if (input.NewInfoTypeId == 7)
                {
                    var val = _NewContactInfoRepository.GetAll().Where(p => p.InfoData == input.InfoData && p.NewInfoTypeId == input.NewInfoTypeId).FirstOrDefault();

                    if (val == null)
                    {
                        await _NewContactInfoRepository.InsertAsync(ContactInfo);
                    }
                    else
                    {
                        //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                    }
                }
                else
                {
                    await _NewContactInfoRepository.InsertAsync(ContactInfo);
                }
            }

            }
            catch (Exception ex)
            {

            }
        }

        public virtual async Task UpdateContactInfo(CreateContactInfo input)
        {
            var ContactInfo1 = input.MapTo<NewContactInfo>();
            var ContactInfo = await _NewContactInfoRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, ContactInfo);

            if (input.NewInfoTypeId == 4)
            {
                var currentDomain = Regex.Match(input.InfoData, "@.*").Value;
                if (input.NewCompanyId > 0)
                {
                    var query = _NewContactInfoRepository.GetAll().Where(p => (p.NewInfoTypeId == 4 && p.NewCompanyId > 0 && p.NewCompanyId != ContactInfo.NewCompanyId) && p.Id != input.Id && Regex.Match(p.InfoData, "@.*").Value == currentDomain).ToList();
                    if (query.Count() > 0)
                    {
                        var Ignoredomain = _EmaildomainRepository.GetAll().Where(p => p.EmaildomainName == currentDomain.Substring(1, currentDomain.Length - 1)).FirstOrDefault();
                        if (Ignoredomain != null)
                        {
                            var query1 = _NewContactInfoRepository.GetAll().Where(p => (p.NewInfoTypeId == 4 && p.InfoData == input.InfoData) && p.Id != input.Id).FirstOrDefault();
                            if (query1 == null)
                            {
                                try
                                {
                                    await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                                }catch(Exception ex)
                                {

                                }
                            }
                            else
                            {
                               // throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                            }
                        }
                        else
                        {
                            //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Email Domain '" + currentDomain + "'...");
                        }
                    }
                    else
                    {
                        var dup = _NewContactInfoRepository.GetAll().Where(p => (p.NewInfoTypeId == 4 && p.NewCompanyId == ContactInfo.NewCompanyId && p.InfoData == input.InfoData) && p.Id != input.Id).FirstOrDefault();
                        if (dup == null)
                        {
                            try
                            {
                                await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        { }
                            //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                    }
                }
                if (input.NewContacId > 0)
                {
                    var cmp = _NewContactRepository.GetAll().Where(p => p.Id == input.NewContacId).FirstOrDefault();
                    var companyId = cmp.NewCompanyId;
                    var query = (from ci in _NewContactInfoRepository.GetAll()
                                 join c in _NewContactRepository.GetAll() on ci.NewContacId equals c.Id
                                 where ci.NewInfoTypeId == 4 && ci.NewContacId > 0 && c.NewCompanyId != companyId && Regex.Match(ci.InfoData, "@.*").Value == currentDomain
                                 select ci).ToList();
                    if (query.Count() > 0)
                    {
                        var Ignoredomain = _EmaildomainRepository.GetAll().Where(p => p.EmaildomainName == currentDomain.Substring(1, currentDomain.Length - 1)).FirstOrDefault();
                        if (Ignoredomain != null)
                        {
                            var query1 = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.InfoData == input.InfoData && p.Id != input.Id).FirstOrDefault();
                            if (query1 == null)
                            {
                                try
                                {
                                    await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                               //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                            }
                        }
                        else
                        {
                            //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Email Domain '" + currentDomain + "'...");
                        }
                    }
                    else
                    {
                        var dup = _NewContactInfoRepository.GetAll().Where(p => p.NewInfoTypeId == 4 && p.InfoData == input.InfoData).FirstOrDefault();
                        if (dup == null)
                        {
                            try
                            {
                                await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {

                        }
                           // throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                    }
                }
            }
            else if (input.NewInfoTypeId == 7)
            {
                var val = _NewContactInfoRepository.GetAll().Where(p => p.InfoData == input.InfoData && p.NewInfoTypeId == input.NewInfoTypeId && p.Id != input.Id).FirstOrDefault();
                if (val == null)
                {
                    try
                    {
                        await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    //throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in data info '" + input.InfoData + "'...");
                }
            }
            else
            {
                try
                {
                    await _NewContactInfoRepository.UpdateAsync(ContactInfo);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<GetNewAddressInfo> GetNewAddressInfoForEdit(NullableIdDto input)
        {
            var output = new GetNewAddressInfo { };
            var AddressInfo = _NewAddressInfoRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.CreateAddressInfos = AddressInfo.MapTo<CreateAddressInfo>();
            return output;
        }
        public async Task<GetNewContactInfo> GetNewContactInfoForEdit(NullableIdDto input)
        {
            var output = new GetNewContactInfo { };
            var ContactInfo = _NewContactInfoRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.CreateContactInfos = ContactInfo.MapTo<CreateContactInfo>();
            return output;
        }

        public async Task GetDeleteAddressInfo(EntityDto input)
        {
            await _NewAddressInfoRepository.DeleteAsync(input.Id);
        }

        public async Task GetDeleteContactInfo(EntityDto input)
        {
            await _NewContactInfoRepository.DeleteAsync(input.Id);
        }


        public async Task GetDeleteEnquiryContact(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 30);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {

                    da.Fill(ds);
                }
                if (input.Id > 0)
                {
                    var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                    if (results.Count() > 0)
                    {
                        throw new UserFriendlyException("Ooops!", "EnquiryContact cannot be deleted '");
                    }
                    else
                    {
                        await _enquiryContactRepository.DeleteAsync(input.Id);
                    }
                }
            }
        }
        public async Task GetDeleteCompany(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 24);
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
                    throw new UserFriendlyException("Ooops!", "Data cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 6);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public async Task GetDeleteContact(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 13);
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
                    throw new UserFriendlyException("Ooops!", "Data cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 7);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        public bool CheckDuplicateCompany(CompanyInputDto input)
        {
            var data = false;
            var query = _NewCompanyRepository.GetAll().Where(p => p.Name == input.CompanyName).ToList();

            if (query.Count() > 0)
            {
                data = true;
            }

            return data;
        }
        public bool CheckDuplicateContact(ContactInputDto input)
        {
            var data = false;
            var contact = _NewContactRepository.GetAll().Where(p => p.NewCompanyId == input.NewCompanyId);

            var query = (from c in contact where c.Name == input.FirstName && c.LastName == input.LastName select c).ToList();

            if (query.Count() > 0)
            {
                data = true;
            }

            return data;
        }

        public async Task ApprovedCompany(EntityDto input)
        {
            var companys = await _NewCompanyRepository.GetAsync(input.Id);
            companys.ApprovedById = AbpSession.UserId;
            companys.IsApproved = true;
            await _NewCompanyRepository.UpdateAsync(companys);
        }

        public async Task<GetNewContacts> SearchContactInfo(EnquiryContactInput input)
        {
            var query = _NewContactInfoRepository.GetAll().Where(p => (p.InfoData == input.Email && p.NewInfoTypeId == 4) || (p.InfoData == input.MobileNo && p.NewInfoTypeId == 7)).FirstOrDefault();

            var SubListout = new GetNewContacts();

            if (query != null)
            {
                var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.NewContacId == query.NewContacId);

                var Contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == query.NewContacId);

                var Contact = _NewContactRepository.GetAll().Where(p => p.Id == query.NewContacId);

                if (Contact != null)
                {
                    SubListout = (from comp in Contact
                                  select new GetNewContacts
                                  {
                                      Id = comp.Id,
                                      Name = comp.Name,
                                      NewCustomerTypeId = comp.NewCustomerTypeId,
                                      CompanyId = comp.NewCompanyId ?? 0,
                                      CompanyName = "",
                                      TitleId = comp.TitleId,
                                      LastName = comp.LastName,

                                      AddressInfo = (from r in Addinfo
                                                     select new CreateAddressInfo
                                                     {
                                                         Id = r.Id,
                                                         NewCompanyId = r.NewCompanyId ?? 0,
                                                         NewContacId = r.NewContacId ?? 0,
                                                         NewInfoTypeId = r.NewInfoTypeId,
                                                         Address1 = r.Address1,
                                                         Address2 = r.Address2,
                                                         CityId = r.CityId,
                                                         CountryName = r.Citys.Country.CountryName

                                                     }).ToArray(),

                                      Contactinfo = (from r in Contactinfo
                                                     select new CreateContactInfo
                                                     {
                                                         Id = r.Id,
                                                         NewCompanyId = r.NewCompanyId ?? 0,
                                                         NewContacId = r.NewContacId ?? 0,
                                                         NewInfoTypeId = r.NewInfoTypeId,
                                                         InfoData = r.InfoData

                                                     }).ToArray()
                                  }).FirstOrDefault();

                }
                if (SubListout.CompanyId > 0)
                {
                    var Company = (from c in _NewCompanyRepository.GetAll().Where(q => q.Id == SubListout.CompanyId) select c.Name).FirstOrDefault();
                    SubListout.CompanyName = Company;
                }

            }
            return SubListout;
        }

        public async Task<int> SearchContactInfoId(EnquiryContactInput input)
        {
            int Id = 0;
            var query = _NewContactInfoRepository.GetAll().Where(p => (p.InfoData == input.Email && p.NewInfoTypeId == 4) || (p.InfoData == input.Telephone && p.NewInfoTypeId == 9) || (p.InfoData == input.MobileNo && p.NewInfoTypeId == 7)).FirstOrDefault();

            if (query != null)
            {
                    Id = (int)query.NewContacId;
            }
            return Id;
        }
    }
}
