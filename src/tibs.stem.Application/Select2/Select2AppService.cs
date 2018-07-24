using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.AutoMapper;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.Activities;
using tibs.stem.Citys;
using tibs.stem.Companies;
using tibs.stem.Countrys;
using tibs.stem.Departments;
using tibs.stem.Designations;
using tibs.stem.EnquiryStatuss;
using tibs.stem.Industrys;
using tibs.stem.LeadReasons;
using tibs.stem.LeadSources;
using tibs.stem.LeadTypes;
using tibs.stem.LineTypes;
using tibs.stem.Locations;
using tibs.stem.Milestones;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSubGroups;
using tibs.stem.SourceTypes;
using tibs.stem.TitleOfCourtes;
using tibs.stem.PriceLevels;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductFamilys;
using tibs.stem.ProductSpecifications;
using tibs.stem.Collections;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.Products;
using tibs.stem.Sections;
using Abp.Authorization.Users;
using tibs.stem.QuotationStatuss;
using tibs.stem.Inquirys;
using tibs.stem.ProductImageUrls;
using tibs.stem.TeamDetails;
using tibs.stem.Team;
using tibs.stem.TemporaryProducts;
using tibs.stem.EnquiryDetails;
using tibs.stem.ProductCategorys;
using tibs.stem.Authorization.Roles;
using tibs.stem.Ybafcos;
using tibs.stem.OpportunitySources;
using tibs.stem.Stagestates;
using tibs.stem.Select2.Dtos;
using tibs.stem.LeadStatuss;
using tibs.stem.Discounts;
using tibs.stem.LeadDetails;
using tibs.stem.UserDesignations;
using tibs.stem.Views;
using tibs.stem.Storage;
using tibs.stem.Authorization.Users.Profile.Dto;
using tibs.stem.Url;
using tibs.stem.Finish;

namespace tibs.stem.Select2
{
    public class Select2AppService : stemAppServiceBase, ISelect2AppService
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<ProductGroup> _productGroupRepositary;
        private readonly IRepository<ProductSubGroup> _productSubGroupRepositary;
        private readonly IRepository<SourceType> _sourceTypeRepositary;
        private readonly IRepository<LeadReason> _leadReasonRepositary;
        private readonly IRepository<Designation> _designationRepositary;
        private readonly IRepository<Company> _companyRepositary;
        private readonly IRepository<Location> _locationRepositary;
        private readonly IRepository<LineType> _lineTypeRepositary;
        private readonly IRepository<MileStone> _milestoneRepositary;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Activity> _activityRepository;
        private readonly IRepository<NewInfoType> _newInfoTypeRepositary;
        private readonly IRepository<NewCustomerType> _newCustomerTypeRepositary;
        private readonly IRepository<NewCompany> _newCompanyRepositary;
        private readonly IRepository<NewContact> _newContactRepositary;
        private readonly IRepository<TitleOfCourtesy> _TitleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<EnquiryStatus> _EnquiryStatusRepository;
        private readonly IRepository<Industry> _IndustryRepository;
        private readonly IRepository<LeadType> _LeadTypeRepository;
        private readonly IRepository<LeadSource> _LeadSourceRepository;
        private readonly IRepository<PriceLevel> _PriceLevelRepository;
        private readonly IRepository<ProductAttribute> _ProductAttributeRepository;
        private readonly IRepository<ProductFamily> _productFamilyRepository;
        private readonly IRepository<ProductSpecification> _ProductSpecificationRepository;
        private readonly IRepository<Collection> _CollectionRepository;
        private readonly IRepository<ProductAttributeGroup> _ProductAttributeGroupRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Section> _sectionRepository;
        private readonly IRepository<NewContactInfo> _NewContactInfoRepository;
        private readonly IRepository<QuotationStatus> _quotationStatusRepository;
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<ProductImageUrl> _productImageUrlRepository;
        private readonly IRepository<TeamDetail> _TeamDetailRepository;
        private readonly IRepository<Teams> _TeamRepository;
        private readonly IRepository<TemporaryProduct> _temporaryProductRepository;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<StageDetails> _StageDetailRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Ybafco> _ybafcoRepository;
        private readonly IRepository<OpportunitySource> _opportunitySourceRepository;
        private readonly IRepository<Stagestate> _StagestateRepository;
        private readonly IRepository<LeadStatus> _leadStatusRepository;
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<LeadDetail> _LeadDetailRepository;
        private readonly IRepository<TemporaryProductImage> _TempProductImageRepository;
        private readonly IRepository<UserDesignation> _userDesignationRepository;
        private readonly IRepository<ProductStates> _ProductStatesRepository;
        private readonly IRepository<View> _viewRepository;
        private readonly IRepository<ReportColumn> _ReportColumnRepository;
        private readonly IRepository<DateFilter> _DateFilterRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IWebUrlService _webUrlService;
        private readonly IRepository<Finished> _FinishedRepository;

        public Select2AppService(IRepository<Department> departmentRepository, IRepository<Collection> CollectionRepository, IRepository<TitleOfCourtesy> TitleRepository, IRepository<NewContact> newContactRepositary, IRepository<NewCompany> newCompanyRepositary, IRepository<City> cityRepository, IRepository<MileStone> milestoneRepositary,
           IRepository<LineType> lineTypeRepositary, IRepository<ReportColumn> ReportColumnRepository, IRepository<DateFilter> DateFilterRepository, IRepository<UserDesignation> userDesignationRepository, IRepository<Company> companyRepositary, IRepository<Location> locationRepositary, IRepository<LeadReason> leadReasonRepositary,
           IRepository<Designation> designationRepositary, IBinaryObjectManager binaryObjectManager, IRepository<LeadDetail> LeadDetailRepository,IRepository<ProductCategory> productCategoryRepository,IRepository<SourceType> sourceTypeRepositary, IRepository<Country> countryRepository, IRepository<ProductGroup> productGroupRepositary, IRepository<EnquiryStatus> enquiryStatusRepository, IRepository<Industry> industryRepository,
           IRepository<Activity> activityRepository, IRepository<Finished> FinishedRepository, IWebUrlService webUrlService, IRepository<View> viewRepository, IRepository<ProductStates> ProductStatesRepository, IRepository<TemporaryProductImage> TempProductImageRepository, IRepository<Discount> discountRepository,IRepository<LeadStatus> leadStatusRepository, IRepository<Stagestate> StagestateRepository,IRepository<Ybafco> ybafcoRepository, IRepository<OpportunitySource> opportunitySourceRepository,RoleManager roleManager, IRepository<EnquiryDetail> enquiryDetailRepository, IRepository<StageDetails> StageDetailRepository,IRepository<TemporaryProduct> temporaryProductRepository,IRepository<TeamDetail> TeamDetailRepository, IRepository<Teams> TeamRepository, IRepository<ProductImageUrl> productImageUrlRepository, IRepository<Inquiry> inquiryRepository,IRepository<QuotationStatus> quotationStatusRepository,IRepository<NewContactInfo> NewContactInfoRepository, IRepository<Product> productRepository, IRepository<Section> sectionRepository,IRepository<ProductAttributeGroup> ProductAttributeGroupRepository,IRepository<ProductSpecification> ProductSpecificationRepository,IRepository<ProductFamily> productFamilyRepository, IRepository<ProductAttribute> ProductAttributeRepository, IRepository<PriceLevel> PriceLevelRepository, IRepository<ProductSubGroup> productSubGroupRepositary, IRepository<LeadType> LeadTypeRepository, IRepository<LeadSource> LeadSourceRepository,IRepository<NewInfoType> newInfoTypeRepositary, IRepository<NewCustomerType> newCustomerTypeRepositary, IRepository<UserRole, long> userRoleRepository)
        {
            _cityRepository = cityRepository;
            _discountRepository = discountRepository;
            _TempProductImageRepository = TempProductImageRepository;
            _countryRepository = countryRepository;
            _productGroupRepositary = productGroupRepositary;
            _sourceTypeRepositary = sourceTypeRepositary;
            _leadReasonRepositary = leadReasonRepositary;
            _designationRepositary = designationRepositary;
            _companyRepositary = companyRepositary;
            _locationRepositary = locationRepositary;
            _lineTypeRepositary = lineTypeRepositary;
            _milestoneRepositary = milestoneRepositary;
            _departmentRepository = departmentRepository;
            _activityRepository = activityRepository;
            _newInfoTypeRepositary = newInfoTypeRepositary;
            _newCustomerTypeRepositary = newCustomerTypeRepositary;
            _newCompanyRepositary = newCompanyRepositary;
            _newContactRepositary = newContactRepositary;
            _userRoleRepository = userRoleRepository;
            _TitleRepository = TitleRepository;
            _EnquiryStatusRepository = enquiryStatusRepository;
            _IndustryRepository = industryRepository;
            _LeadTypeRepository = LeadTypeRepository;
            _LeadSourceRepository = LeadSourceRepository;
            _productSubGroupRepositary = productSubGroupRepositary;
            _PriceLevelRepository = PriceLevelRepository;
            _ProductAttributeRepository = ProductAttributeRepository;
            _productFamilyRepository = productFamilyRepository;
            _ProductSpecificationRepository = ProductSpecificationRepository;
            _CollectionRepository = CollectionRepository;
            _TeamDetailRepository = TeamDetailRepository;
            _TeamRepository = TeamRepository;
            _ReportColumnRepository = ReportColumnRepository;
            _DateFilterRepository = DateFilterRepository;
            _ProductAttributeGroupRepository = ProductAttributeGroupRepository;
            _productRepository = productRepository;
            _sectionRepository = sectionRepository;
            _NewContactInfoRepository = NewContactInfoRepository;
            _quotationStatusRepository = quotationStatusRepository;
            _inquiryRepository = inquiryRepository;
            _productImageUrlRepository = productImageUrlRepository;
            _temporaryProductRepository = temporaryProductRepository;
            _enquiryDetailRepository = enquiryDetailRepository;
            _productCategoryRepository = productCategoryRepository;
            _StageDetailRepository = StageDetailRepository;
            _roleManager = roleManager;
            _ybafcoRepository = ybafcoRepository;
            _opportunitySourceRepository = opportunitySourceRepository;
            _StagestateRepository = StagestateRepository;
            _leadStatusRepository = leadStatusRepository;
            _LeadDetailRepository = LeadDetailRepository;
            _userDesignationRepository = userDesignationRepository;
            _ProductStatesRepository = ProductStatesRepository;
            _viewRepository = viewRepository;
            _binaryObjectManager = binaryObjectManager;
            _webUrlService = webUrlService;
            _FinishedRepository = FinishedRepository;
        }

        public async Task<Select2City> GetCity()
        {
            Select2City sr = new Select2City();
            var CityDto = (from c in _cityRepository.GetAll() select c);

            if (CityDto.Count() > 0)
            {
                var CityDtos = (from c in CityDto select new citydto { Id = c.Id, Name = c.CityName, Country = c.Country.CountryName });
                sr.select2data = CityDtos.ToArray();
            }

            return sr;
        }

        public async Task<Select2Result> GetFinishes()
            {
            Select2Result sr = new Select2Result();
            var query = (from c in _FinishedRepository.GetAll() select c).ToArray();

            if (query.Length > 0)
            {
                var finishes = (from c in query select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = finishes;
            }
            return sr;
        }
        public async Task<Select2product> GetSpecProduct(NullableIdDto input)
        {
            Select2product sr = new Select2product();

            var product = (from c in _productRepository.GetAll() where c.ProductSpecificationId == input.Id select c).ToArray();

            if (product.Length > 0)
            {
                var products = (from c in product
                                select new productdto
                                {
                                    Id = c.Id,
                                    Name = c.ProductCode,
                                    Prize = (int)(c.Price ?? 0)
                                }).ToArray();
                sr.select2data = products;
            }

            return sr;
        }
        public async Task<Select2Result> GetPriceLevel()
        {
            Select2Result sr = new Select2Result();
            var query = (from c in _PriceLevelRepository.GetAll() select c).ToArray();

            if (query.Length > 0)
            {
                var PriceLevelList = (from c in query select new datadto { Id = c.Id, Name = c.PriceLevelName }).ToArray();
                sr.select2data = PriceLevelList;
            }
            return sr;
        }

        public async Task<Select2Result> GetSourceType()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _sourceTypeRepositary.GetAll() select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.SourceTypeName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }

        public async Task<Select2Result> GetCountry()
        {
            Select2Result sr = new Select2Result();
            var country = (from c in _countryRepository.GetAll() select c).ToArray();

            if (country.Length > 0)
            {
                var countryarray = (from c in country select new datadto { Id = c.Id, Name = c.CountryName }).ToArray();
                sr.select2data = countryarray;
            }

            return sr; 

        }
        public async Task<Select2Result> GetProductSubGroup(NullableIdDto input)
        {
            Select2Result sr = new Select2Result();
            var prouctSubGroup = (from c in _productSubGroupRepositary.GetAll() where c.GroupId == input.Id select c).ToArray();

            if (prouctSubGroup.Length > 0)
            {
                var prouctGrouparray = (from c in prouctSubGroup select new datadto { Id = c.Id, Name = c.ProductSubGroupName }).ToArray();
                sr.select2data = prouctGrouparray;
            }

            return sr;
        }
        public async Task<Select3Result> GetAccountHandler()
        {
            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users where c.Id > 2 select c).ToArray();

                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }
        public async Task<Select3Result> GetSalesman()
        {
            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 4 || role.RoleId == 10 || role.RoleId == 17
                               select c).ToArray();

                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }
        public async Task<Select3Result> GetSalesCoordinator()
        {
            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 6 || role.RoleId == 17
                               select c).ToArray();

                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }

        public async Task<Select3Result> GetDesigner()
        {
            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 7
                               select c).ToArray();

                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }

        public async Task<Select2Result> GetLeadReason()
        {
            Select2Result sr = new Select2Result();
            var reason = (from c in _leadReasonRepositary.GetAll() select c).ToArray();

            if (reason.Length > 0)
            {
                var reasonArray = (from c in reason select new datadto { Id = c.Id, Name = c.LeadReasonName }).ToArray();
                sr.select2data = reasonArray;
            }
            return sr;
        }

        public async Task<Select2Result> GetLeadType()
        {
            Select2Result sr = new Select2Result();
            var leadtype = (from c in _LeadTypeRepository.GetAll() select c).ToArray();

            if (leadtype.Length > 0)
            {
                var leadtypeArray = (from c in leadtype select new datadto { Id = c.Id, Name = c.LeadTypeName }).ToArray();
                sr.select2data = leadtypeArray;
            }
            return sr;
        }
        public async Task<Select2Result> GetLeadSource()
        {
            Select2Result sr = new Select2Result();
            var leadsource = (from c in _LeadSourceRepository.GetAll() select c).ToArray();

            if (leadsource.Length > 0)
            {
                var leadsourceArray = (from c in leadsource select new datadto { Id = c.Id, Name = c.LeadSourceName }).ToArray();
                sr.select2data = leadsourceArray;
            }
            return sr;
        }

        public async Task<Select2Result> GetDesignation()
        {
            Select2Result sr = new Select2Result();
            var designtion = (from c in _designationRepositary.GetAll() select c).ToArray();

            if (designtion.Length > 0)
            {
                var designtionArray = (from c in designtion select new datadto { Id = c.Id, Name = c.DesiginationName }).ToArray();
                sr.select2data = designtionArray;
            }
            return sr;

        }
        public async Task<Select2Result> GetCompany(LocationInput input)
        {
            Select2Result sr = new Select2Result();
            if (input.Id != null)
            {
                var company = (from c in _companyRepositary.GetAll() where c.Cities.CountryId == input.Id select c).ToArray();

                if (company.Length > 0)
                {
                    var companyArray = (from c in company select new datadto { Id = c.Id, Name = c.CompanyName }).ToArray();
                    sr.select2data = companyArray;
                }
            }
            return sr;
        }
        //public async Task<Select2Result> GetAllCompany()
        //{
        //    Select2Result sr = new Select2Result();

        //        var company = (from c in _newCompanyRepositary.GetAll()  select c).ToArray();

        //        if (company.Length > 0)
        //        {
        //            var companyArray = (from c in company select new datadto { Id = c.Id, Name = c.Name }).ToArray();
        //            sr.select2data = companyArray;
        //        }
        //    return sr;
        //}

        public async Task<Select2Result> GetAllCompany(Select2Input input)
        {
            Select2Result sr = new Select2Result();

            long userid = (int)AbpSession.UserId;

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var company = (from c in _newCompanyRepositary.GetAll() where c.Name.Contains(input.Name) select c).ToArray();

            if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Manager / Sales Executive" || userrole.DisplayName == "Sales Coordinator / Sales Executive")
            {
                company = (from c in _newCompanyRepositary.GetAll().Where(p => p.AccountManagerId == userid) select c).ToArray();

                if (company.Length > 0)
                {
                    var companyArray = (from c in company select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = companyArray;
                }
            }
            else if (userrole.DisplayName == "Sales Manager")
            {
                var teamSalesman = (from q in _TeamDetailRepository.GetAll()
                                    join r in _TeamRepository.GetAll() on q.TeamId equals r.Id
                                    where r.SalesManagerId == userid
                                    select q.SalesmanId).ToArray();
                var companyArray = new List<datadto>();

                if (teamSalesman.Length > 0)
                {
                    foreach (var data in teamSalesman)
                    {
                        var companySales = (from c in _newCompanyRepositary.GetAll() where c.AccountManagerId == data && c.Name.Contains(input.Name) select c).ToArray();
                        if (companySales.Length > 0)
                        {
                            foreach (var item in companySales)
                            {
                                companyArray.Add(new datadto { Id = item.Id, Name = item.Name });
                            }

                        }

                    }
                    sr.select2data = companyArray.ToArray();
                }

            }
            else
            {
                if (company.Length > 0)
                {
                    var companyArray = (from c in company select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                    sr.select2data = companyArray;
                }
            }


            return sr;
        }
        //public async Task<Select2sales> GetCompanyWithSales()
        //{
        //    Select2sales sr = new Select2sales();

        //    var company = (from c in _newCompanyRepositary.GetAll() select c).ToArray();

        //    if (company.Length > 0)
        //    {
        //        var companyArray = (from c in company select new Select2salesDto {
        //            Id = c.Id,
        //            Name = c.Name,
        //            SalesManId = c.AccountManagerId != null ? c.AccountManagerId : 0,
        //            SalesMan = ""
        //        }).ToArray();

        //        foreach(var data in companyArray)
        //        {
        //            if(data.SalesManId  >0)
        //            {
        //                var Account = (from u in UserManager.Users  where u.Id == data.SalesManId select u).FirstOrDefault();
        //                data.SalesMan = Account.UserName;
        //            }
        //        }
        //        sr.selectCompdata = companyArray;
        //    }
        //    return sr;
        //}

        public async Task<Select2sales> GetCompanyWithSales()
        {
            Select2sales sr = new Select2sales();
            try
            {

            long userid = (int)AbpSession.UserId;

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var company = (from c in _newCompanyRepositary.GetAll() select c);

            if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                company = (from c in _newCompanyRepositary.GetAll().Where(p => p.AccountManagerId == userid) select c);

                if (company.Count() > 0)
                {
                    var companyArray = (from c in company
                                        join ur in UserManager.Users on c.AccountManagerId equals ur.Id into urJoined
                                        from ur in urJoined.DefaultIfEmpty()
                                        select new Select2salesDto
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            SalesManId = c.AccountManagerId ?? 0,
                                            SalesMan = ur != null ? ur.UserName : ""
                                        }).ToArray();

                  
                    sr.selectCompdata = companyArray;
                }
            }
            else if (userrole.DisplayName == "Sales Manager")
            {
                var teamSalesman = (from q in _TeamDetailRepository.GetAll()
                                    join r in _TeamRepository.GetAll() on q.TeamId equals r.Id
                                    where r.SalesManagerId == userid
                                    select q.SalesmanId).ToArray();
                var companyArray = new List<Select2salesDto>();

                if (teamSalesman.Length > 0)
                {
                    foreach (var data in teamSalesman)
                    {
                        var companySales = (from c in _newCompanyRepositary.GetAll().Where(p => p.AccountManagerId == data)
                                            join ur in UserManager.Users on c.AccountManagerId equals ur.Id into urJoined
                                            from ur in urJoined.DefaultIfEmpty()
                                            select new Select2salesDto {
                                                                     Id = c.Id, 
                                                                     Name = c.Name, 
                                                                     SalesManId = c.AccountManagerId, 
                                                                     SalesMan = ur != null ? ur.UserName : "" }).ToArray();

                        if (companySales.Count() > 0)
                        {
                            foreach (var item in companySales)
                            {
                                companyArray.Add(new Select2salesDto { Id = item.Id, Name = item.Name, SalesManId = item.SalesManId, SalesMan = item.SalesMan });
                            }

                        }

                    }                  
                    sr.selectCompdata = companyArray.ToArray();
                }

            }
            else
            {
                if (company.Count() > 0)
                {
                    var companyArray = (from c in company
                                        join ur in UserManager.Users on c.AccountManagerId equals ur.Id into urJoined
                                        from ur in urJoined.DefaultIfEmpty()
                                        select new Select2salesDto
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            SalesManId = c.AccountManagerId ?? 0,
                                            SalesMan = ur !=null ? ur.UserName : ""
                                        }).ToArray();

                    sr.selectCompdata = companyArray;
                }
            }
        }
            catch(Exception ex)
            {

            }
            return sr;
        }


        public async Task<Select2Result> GetLocation(LocationInput input)
        {
            Select2Result sr = new Select2Result();

            if(input.Id != null)
            {
                var location = (from c in _locationRepositary.GetAll() where c.citys.CountryId == input.Id select c).ToArray();

                if (location.Length > 0)
                {
                    var locationArray = (from c in location select new datadto { Id = c.Id, Name = c.LocationName }).ToArray();
                    sr.select2data = locationArray;
                }
            }
           
            return sr;
        }
        public async Task<Select2Result> GetAllLocation()
        {
            Select2Result sr = new Select2Result();
                var location = (from c in _locationRepositary.GetAll()  select c).ToArray();

                if (location.Length > 0)
                {
                    var locationArray = (from c in location select new datadto { Id = c.Id, Name = c.LocationName }).ToArray();
                    sr.select2data = locationArray;
                }

            return sr;
        }

        public async Task<Select2Result> GetLineType()
        {
            Select2Result sr = new Select2Result();

          
                var Lines = (from c in _lineTypeRepositary.GetAll() select c).ToArray();

                if (Lines.Length > 0)
                {
                    var lineArray = (from c in Lines select new datadto { Id = c.Id, Name = c.LineTypeName }).ToArray();
                    sr.select2data = lineArray;
                }
            return sr;
        }

        public async Task<Select2Result> GetMileStone()
        {
            Select2Result sr = new Select2Result();


            var mile = (from c in _milestoneRepositary.GetAll() select c).ToArray();

            if (mile.Length > 0)
            {
                var mileArray = (from c in mile select new datadto { Id = c.Id, Name = c.MileStoneName }).ToArray();
                sr.select2data = mileArray;
            }
            return sr;
        }

        //public async Task<Select2Result> GetDepartment(NullableIdDto input)
        //{

        //    Select2Result sr = new Select2Result();
        //    var Account = (from u in UserManager.Users join role in _userRoleRepository.GetAll() on u.Id equals role.UserId
        //                   where u.Id == input.Id select role).FirstOrDefault();

        //    var depa = (from c in _departmentRepository.GetAll() select c).ToArray();
        //    var data = (from c in depa join u in UserManager.Users on c.Id equals u.DepartmentId where u.Id != input.Id select c).ToList();
        //    if (Account.RoleId == 5)
        //    {
        //          var  department = depa.Where(f => !data.Contains(f)).ToList();
        //            if (department.Count > 0)
        //            {
        //                var depas = (from c in department select new datadto { Id = c.Id, Name = c.DepatmentName }).ToArray();
        //                sr.select2data = depas;
        //            }
        //    }
        //    else
        //    {
        //        if (depa.Length > 0)
        //        {
        //            var depas = (from c in depa select new datadto { Id = c.Id, Name = c.DepatmentName }).ToArray();
        //            sr.select2data = depas;
        //        }
        //    }

        //    return sr;
        //}
        public async Task<Select2Result> GetDepartment()
        {

            Select2Result sr = new Select2Result();
            var depa = (from c in _departmentRepository.GetAll() select c).ToArray();

                if (depa.Length > 0)
                {
                    var depas = (from c in depa select new datadto { Id = c.Id, Name = c.DepatmentName }).ToArray();
                    sr.select2data = depas;
                }
            return sr;
        }
        public async Task<Select2Result> GetDepartmentSales()
        {
            Select2Result sr = new Select2Result();
            var depa = (from c in _departmentRepository.GetAll() select c).ToArray();

            if (depa.Length > 0)
            {

                var Account = (from c in depa
                               join u in UserManager.Users on c.Id equals u.DepartmentId
                               group c by c into g
                               select new datadto { Id = g.Key.Id, Name = g.Key.DepatmentName }).ToArray();
                sr.select2data = Account;
            }

            return sr;
        }
        public async Task<Select2Result> GetSalesFromDepatment(NullableIdDto input)
        {
            Select2Result sr = new Select2Result();
            var depa = (from c in _departmentRepository.GetAll() where c.Id == input.Id select c).ToArray();

            if (depa.Length > 0)
            {
                var Account = (from c in depa
                               join u in UserManager.Users on c.Id equals u.DepartmentId
                               join role in _userRoleRepository.GetAll() on u.Id equals role.UserId
                               where role.RoleId == 4 || role.RoleId == 17
                               group u by u into g
                               select new datadto { Id = (int)g.Key.Id, Name = g.Key.UserName }).ToArray();

                sr.select2data = Account;
            }

            return sr;
        }

        public async Task<Select2Result> GetActivityTypes()
        {
            Select2Result sr = new Select2Result();
            var activity = (from c in _activityRepository.GetAll() select c).ToArray();

            if (activity.Length > 0)
            {
                var activitys = (from c in activity select new datadto { Id = c.Id, Name = c.ActivityName }).ToArray();

                sr.select2data = activitys;
            }

            return sr;
        }


        public async Task<Select2Result> GetContactTypeInfo()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _newInfoTypeRepositary.GetAll() where c.Info == true select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.ContactName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetCompanyTypeinfo()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _newInfoTypeRepositary.GetAll() where c.Info == false select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.ContactName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetNewCompanyType()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _newCustomerTypeRepositary.GetAll() where c.Company == true select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Title }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetNewCustomerType()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _newCustomerTypeRepositary.GetAll() where c.Company == false select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Title }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Contact> GetCompanyContacts(NullableIdDto input)
        {
            Select2Contact sr = new Select2Contact();
            var src = (from c in _newContactRepositary.GetAll() where c.NewCompanyId == input.Id && c.NewCompanyId != null select c).ToArray();
            if (src.Length > 0)
            {
                var srcarray = (from c in src select new contactdto { Id = c.Id, Name = c.Name,FullName = c.Name +" "+c.LastName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }

        public async Task<Select2Result> GetContactEmail(NullableIdDto input)
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _NewContactInfoRepository.GetAll() where c.NewContacId == input.Id && c.NewInfoTypeId == 4  select c).ToArray();
            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.InfoData}).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetContactMobile(NullableIdDto input)
        {
            Select2Result sr = new Select2Result(); 
            var src = (from c in _NewContactInfoRepository.GetAll() where c.NewContacId == input.Id && c.NewInfoTypeId == 7 select c).ToArray();
            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.InfoData }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }

        public async Task<Select2Result> GetTitle()
        {
            Select2Result sr = new Select2Result();
            var title = (from c in _TitleRepository.GetAll() select c).ToArray();

            if (title.Length > 0)
            {
                var titleDtos = (from c in title select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = titleDtos;
            }

            return sr;
        }

        public async Task<Select2Result> GetEnquiryStatus()
        {
            Select2Result sr = new Select2Result();
            var title = (from c in _EnquiryStatusRepository.GetAll() select c).ToArray();
            var stages = new List<datadto>();

            foreach(var data in title)
            {
                var comp = _StageDetailRepository.GetAll().Where(p => p.StageId == data.Id).FirstOrDefault();
                if(comp == null)
                {
                    stages.Add(new datadto { Id = data.Id, Name = data.EnqStatusName });
                }
            }
            sr.select2data = stages.ToArray();

            return sr;

        }
        public async Task<Stage2Result> GetEnquiryStages(NullableIdDto input)
        {
            Stage2Result sr = new Stage2Result();
            var title = (from c in _EnquiryStatusRepository.GetAll()
                         join d in _StageDetailRepository.GetAll() on c.Id equals d.StageId where d.MileStoneId == input.Id
                         select c).ToArray();

            if (title.Length > 0)
            {
                var titleDtos = (from c in title select new Stagedto {
                    Id = c.Id,
                    Name = c.EnqStatusName,
                    ColorCode = c.EnqStatusColor,
                    Value = (int)(c.Percentage ?? 0),
                    Status = ""
                }).ToArray();

                foreach(var r in titleDtos)
                {
                    var data = _EnquiryStatusRepository.GetAll().Where(p => p.Id == r.Id).Select(t => t.Stagestatess).FirstOrDefault();
                    if(data != null)
                    {
                        r.Status = data.Name;
                    }
                }

                sr.Select2data = titleDtos;
            }
            return sr;
        }

        public async Task<Select2Result> GetIndustry()
        {
            Select2Result sr = new Select2Result();
            var title = (from c in _IndustryRepository.GetAll() select c).ToArray();

            if (title.Length > 0)
            {
                var titleDtos = (from c in title select new datadto { Id = c.Id, Name = c.IndustryName }).ToArray();
                sr.select2data = titleDtos;
            }

            return sr;
        }

        public async Task<Select2product> GetProduct()
        {
            Select2product sr = new Select2product();

            var product = (from c in _productRepository.GetAll() select c).ToArray();
            if (product.Length > 0)
            {
                var products = (from c in product
                                select new productdto
                                {
                                    Id = c.Id,
                                    Name = c.ProductCode,
                                    Prize = (int)(c.Price ?? 0),
                                    SpecId = c.ProductSpecificationId != null ? c.ProductSpecificationId : 0,
                                    Discount = false
                                }).ToArray();
                if (products.Length > 0)
                {
                   
                    foreach (var pro in products)
                    {
                        if (pro.SpecId == 0)
                        {
                            pro.Discount = false;
                        }
                        else
                        {
                            var Family = (from s in _ProductSpecificationRepository.GetAll()
                                          join g in _productGroupRepositary.GetAll() on s.ProductGroupId equals g.Id
                                          where s.Id == pro.SpecId
                                          select g.prodFamily).FirstOrDefault();

                            pro.Discount = (bool)(Family.Discount != null ? Family.Discount : false);
                        }
                    }
                }
                sr.select2data = products;
            }

            return sr;
        }

        public async Task<Select2Result> GetSection(NullableIdDto input)
        {
            Select2Result sr = new Select2Result();

            var section = (from c in _sectionRepository.GetAll() where c.QuotationId == input.Id select c).ToArray();

            if (section.Length > 0)
            {
                var sections = (from c in section select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = sections;
            }

            return sr;
        }

        public async Task<Select2Result> GetCompatitorCompany()
        {
            Select2Result sr = new Select2Result();

            var company = (from c in _newCompanyRepositary.GetAll() where c.NewCustomerTypeId == 6 select c).ToArray();

            if (company.Length > 0)
            {
                var companyArray = (from c in company select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = companyArray;
            }
            return sr;
        }
        public async Task<Select2Attribute> GetAttribute()
        {
            Select2Attribute sr = new Select2Attribute();
            var AttributesDto = (from c in _ProductAttributeRepository.GetAll() select c);

            if (AttributesDto.Count() > 0)
            {
                var AttributesDtos = (from c in AttributesDto select new AttributeDto { Id = c.Id, Name = c.AttributeName, Path = c.Imageurl });
                sr.select3data = AttributesDtos.ToArray();
            }

            return sr;
        }

        public async Task<Select2Result> GetAttributeGroup()
        {
            Select2Result sr = new Select2Result();

            var AttributeGroups = (from c in _ProductAttributeGroupRepository.GetAll() select c).ToArray();

            if (AttributeGroups.Length > 0)
            {
                var AttributeGroupsArray = (from c in AttributeGroups select new datadto { Id = c.Id, Name = c.AttributeGroupName }).ToArray();
                sr.select2data = AttributeGroupsArray;
            }

            return sr;
        }
        public async Task<Select2Result> GetProductFamily()
        {
            Select2Result sr = new Select2Result();
            var title = (from c in _productFamilyRepository.GetAll() select c).ToArray();

            if (title.Length > 0)
            {
                var titleDtos = (from c in title select new datadto { Id = c.Id, Name = c.ProductFamilyName }).ToArray();
                sr.select2data = titleDtos;
            }

            return sr;
        }
        public async Task<Select2Result> GetCollection()
        {
            Select2Result sr = new Select2Result();

            var Collection = (from c in _CollectionRepository.GetAll() select c).ToArray();

            if (Collection.Length > 0)
            {
                var CollectionArray = (from c in Collection select new datadto { Id = c.Id, Name = c.CollectionName }).ToArray();
                sr.select2data = CollectionArray;
            }

            return sr;
        }

        public async Task<Select2Result> GetProductGroup()
        {
            Select2Result sr = new Select2Result();
            var prouctGroup = (from c in _productGroupRepositary.GetAll() select c).ToArray();

            if (prouctGroup.Length > 0)
            {
                var prouctGrouparray = (from c in prouctGroup select new datadto { Id = c.Id, Name = c.ProductGroupName }).ToArray();
                sr.select2data = prouctGrouparray;
            }

            return sr;
        }
        public async Task<Select2Product> GetProductSpecification()
        {
            Select2Product sr = new Select2Product();
            var ProdDto = (from c in _ProductSpecificationRepository.GetAll() select c);

            if (ProdDto.Count() > 0)
            {
                var ProdDtos = (from c in ProdDto select new proddto { Id = c.Id, Name = c.Name, ProductFamily = "" });
                sr.select2data = ProdDtos.ToArray();
            }

            return sr;
        }
        public async Task<Select2Result> GetQuotationStatus()
        {
            Select2Result sr = new Select2Result();
            var Status = (from c in _quotationStatusRepository.GetAll() where c.Id > 1 select c).ToArray();

            if (Status.Count() > 0)
            {
                var Statuss = (from c in Status select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = Statuss.ToArray();
            }

            return sr;
        }
        //public async Task<Select2Result> GetInquiry()
        //{
        //    Select2Result sr = new Select2Result();
        //    var Inquiry = (from c in _inquiryRepository.GetAll() where c.MileStoneId > 3 && c.IsClosed != true select c).ToArray();

        //    if (Inquiry.Count() > 0)
        //    {
        //        var Inquirys = (from c in Inquiry select new datadto { Id = c.Id, Name = c.Name }).ToArray();
        //        sr.select2data = Inquirys.ToArray();
        //    }

        //    return sr;
        //}
        public async Task<Select2Result> GetInquiry()
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            Select2Result sr = new Select2Result();

            var Inquiry = (from c in _inquiryRepository.GetAll() where c.MileStoneId > 3 && c.IsClosed != true && c.Won != true && c.Lost != true select c).ToArray();

            if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                Inquiry = (from enq in _inquiryRepository.GetAll()
                           join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                           where enq.MileStoneId > 3 && enq.IsClosed != true && enqDetail.AssignedbyId == userid
                           select enq).ToArray();
            }
            else if (userrole.DisplayName == "Sales Manager")
            {
                Inquiry = (from enq in _inquiryRepository.GetAll()
                           join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                           join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                           where enq.MileStoneId > 3 && enq.IsClosed != true && team.SalesManagerId == userid
                           select enq).ToArray();
            }
            else if (userrole.DisplayName == "Designer")
            {
                Inquiry = (from enq in _inquiryRepository.GetAll()
                           join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                           where enq.MileStoneId > 3 && enq.IsClosed != true && leadDetail.DesignerId == userid
                           select enq
                        ).ToArray();
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                Inquiry = (from enq in _inquiryRepository.GetAll()
                           join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                           where enq.MileStoneId > 3 && enq.IsClosed != true && leadDetail.CoordinatorId == userid
                           select enq
                        ).ToArray();
            }
            else if (userrole.DisplayName == "Sales Coordinator / Sales Executive")
            {
                Inquiry = (from enq in _inquiryRepository.GetAll()
                           join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                           where enq.MileStoneId > 3 && enq.IsClosed != true && enqDetail.AssignedbyId == userid
                           select enq).Union(from enq in _inquiryRepository.GetAll()
                                             join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                                             where enq.MileStoneId > 3 && enq.IsClosed != true && leadDetail.CoordinatorId == userid
                                             select enq).Distinct().OrderBy(p => p.Id).ToArray();
            }
            else
            {
                Inquiry = (from c in _inquiryRepository.GetAll() where c.MileStoneId > 3 && c.IsClosed != true select c).ToArray();

            }
            if (Inquiry.Length > 0)
            {
                var Inquirys = (from c in Inquiry select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = Inquirys.ToArray();
            }

            return sr;
        }
        public async Task<Select2productdetailsdto> GetProductDetails(Select2Input input)
        {
            Select2productdetailsdto sr = new Select2productdetailsdto();
            var query = (from c in _productRepository.GetAll() where c.ProductCode.Contains(input.Name) select c );

            if (query.Count() > 0)
            {
            var productdtos = (from c in query select new Productdetailsdto {
                                Id = c.Id,
                                ProductCode = c.ProductCode,
                                ProductName = c.ProductName,
                                SpecificationName = c.ProductSpecifications.Name,
                                Description = c.Description,
                                ImageUrl = "",
                                Price = c.Price != null ? (decimal)c.Price : 0,
                                Discount = false,
                                SpecificationId = (int)(c.ProductSpecificationId ?? 0) }).ToArray();

                if (productdtos.Length > 0)
                {

                    foreach (var pro in productdtos)
                    {
                        var imageqry = _productImageUrlRepository.GetAll().Where(p => p.ProductId == pro.Id).FirstOrDefault();

                        if (imageqry != null)
                            pro.ImageUrl = imageqry.ImageUrl;

                        if (pro.SpecificationId == 0)
                        {
                            pro.Discount = false;
                        }
                        else
                        {
                            var Family = (from s in _ProductSpecificationRepository.GetAll()
                                          join g in _productGroupRepositary.GetAll() on s.ProductGroupId equals g.Id
                                          where s.Id == pro.SpecificationId
                                          select g.prodFamily).FirstOrDefault();

                            pro.Discount = (bool)(Family.Discount ?? false);
                        }
                    }
                }

                sr.select2data = productdtos.OrderByDescending(p => p.SpecificationId).ToArray();
            }

            return sr;
        }
        public async Task<Select3Result> GetSalesManager()
        {
            Select3Result sr = new Select3Result();
            {
                var salesManager = (from c in UserManager.Users
                                    join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                                    where role.RoleId == 5 || role.RoleId == 10
                                    select c).ToArray();


                var salesManagers = (from c in salesManager select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = salesManagers;
            }

            return sr;
        }

        public async Task<Select3Result> GetOtherSalesman()
        {
            Select3Result sr = new Select3Result();
            {
                var Salesman = (from c in UserManager.Users
                                join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                                where role.RoleId == 4 || role.RoleId == 17
                                select c).ToArray();
                var Salesmans = new List<datadtos>();

                foreach (var data in Salesman)
                {
                    var Teamdetail = _TeamDetailRepository.GetAll().Where(p => p.SalesmanId == data.Id).FirstOrDefault();
                    if (Teamdetail == null)
                    {
                        Salesmans.Add(new datadtos { Id = data.Id, Name = data.UserName });
                        sr.select3data = Salesmans.ToArray();
                    }
                }

            }

            return sr;
        }
        public async Task<Select3Result> GetAllSalesman()
        {
            Select3Result sr = new Select3Result();
            {
                var Salesman = (from c in UserManager.Users
                                join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                                where role.RoleId == 4
                                select c).ToArray();
                var Salesmans = new List<datadtos>();

                foreach (var data in Salesman)
                {

                        Salesmans.Add(new datadtos { Id = data.Id, Name = data.UserName });
                        sr.select3data = Salesmans.ToArray();

                }

            }

            return sr;
        }
        public async Task<Select3Result> GetOtherSalesManager()
        {
            Select3Result sr = new Select3Result();
            {
                var salesManager = (from c in UserManager.Users
                                    join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                                    where role.RoleId == 5 || role.RoleId == 10 || role.RoleId == 11
                                    select c).ToArray();
                var salesManagers = new List<datadtos>();

                foreach (var data in salesManager)
                {
                    var Team = _TeamRepository.GetAll().Where(p => p.SalesManagerId == data.Id).FirstOrDefault();
                    if (Team == null)
                    {
                        salesManagers.Add(new datadtos { Id = data.Id, Name = data.UserName });
                        sr.select3data = salesManagers.ToArray();
                    }
                }

            }

            return sr;
        }
        public async Task<Select3Result> GetTeamSalesman(NullableIdDto input)
        {
            var teamSalesmans = new List<datadtos>();
            Select3Result sr = new Select3Result();
            long userid = (int)AbpSession.UserId;
            var roles = (from c in UserManager.Users
                         join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                         join role in _roleManager.Roles on urole.RoleId equals role.Id
                         where urole.UserId == userid
                         select role).FirstOrDefault();

            if (roles.DisplayName == "Sales Executive" || roles.DisplayName == "Sales Coordinator / Sales Executive")
            {

                var Salesman = (from c in _TeamDetailRepository.GetAll() 
                                where c.TeamId == input.Id && c.SalesmanId == userid
                                select c.Salesman).FirstOrDefault();
                if(Salesman != null)
                {
                    var name = Salesman.UserName;
                    teamSalesmans.Add(new datadtos { Id = userid, Name = name });
                }
            }
            else
            {

                var teamSalesman = (from c in UserManager.Users
                                    join team in _TeamDetailRepository.GetAll() on c.Id equals team.SalesmanId
                                    where team.TeamId == input.Id
                                    select c).ToArray();
                var data = (from c in teamSalesman select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                foreach(var t in data)
                {
                    teamSalesmans.Add(new datadtos { Id = t.Id, Name = t.Name });
                }

                var manager = (from c in UserManager.Users
                               join team in _TeamRepository.GetAll() on c.Id equals team.SalesManagerId
                               where team.Id == input.Id
                               select c).FirstOrDefault();
                if (manager != null)
                {
                    var userrole = (from c in UserManager.Users
                                    join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                                    join role in _roleManager.Roles on urole.RoleId equals role.Id
                                    where urole.UserId == manager.Id
                                    select role).FirstOrDefault();
                    if ( userrole.DisplayName == "Sales Manager / Sales Executive")
                    {
                        teamSalesmans.Add(new datadtos { Id = manager.Id, Name = manager.UserName });
                    }
                }


            }
            sr.select3data = teamSalesmans.ToArray();
            return sr;
        }
        public async Task<Select2sales> GetCompanyWithSalesman()
        {
            Select2sales sr = new Select2sales();
            var company = (from c in _newCompanyRepositary.GetAll() select c).ToArray();

            if (company.Length > 0)
            {
                var companyArray = (from c in company
                                    select new Select2salesDto
                                    {
                                        Id = c.Id,
                                        Name = c.Name,
                                        SalesManId = c.AccountManagerId != null ? c.AccountManagerId : 0,
                                        SalesMan = ""
                                    }).ToArray();

                var companySalesman = new List<Select2salesDto>();

                foreach (var data in companyArray)
                {
                    if (data.SalesManId > 0)
                    {
                        var Account = (from u in UserManager.Users where u.Id == data.SalesManId select u).FirstOrDefault();
                        data.SalesMan = Account.UserName;

                        companySalesman.Add(new Select2salesDto { Id = data.Id, Name = data.Name, SalesManId = data.SalesManId, SalesMan = data.SalesMan });
                    }
                }
                sr.selectCompdata = companySalesman.ToArray();
            }
            return sr;
        }
        public async Task<Select2Result> GetTemporaryProduct()
        {
            Select2Result sr = new Select2Result();

            var product = (from c in _temporaryProductRepository.GetAll() where c.Updated == false select c).ToArray();
            if (product.Length > 0)
            {
                var products = (from c in product
                                select new datadto
                                {
                                    Id = c.Id,
                                    Name = c.SuspectCode
                                }).ToArray();
                sr.select2data = products;
            }

            return sr;
        }

        public async Task<Select2Inquiry> GetInquiryDetails(NullableIdDto input)
        {
            Select2Inquiry sr = new Select2Inquiry();

            var enquiry = (from c in _enquiryDetailRepository.GetAll() where c.InquiryId == input.Id select c).ToArray();

            if (enquiry.Length > 0)
            {
                var enquirys = (from c in enquiry
                                select new Select2InquiryDto
                                {
                                    Id = c.Id,
                                    CompanyId = c.CompanyId,
                                    CompanyName = "",
                                    ContactId = c.ContactId,
                                    ContactName = "",
                                    SalesManId = c.AssignedbyId ?? 0,
                                    SalesMan = ""
                                }).ToArray();
                foreach (var data in enquirys)
                {
                    var qry = _newCompanyRepositary.GetAll().Where(p => p.Id == data.CompanyId).FirstOrDefault();
                    data.CompanyName = qry.Name;
                    var qry1 = _newContactRepositary.GetAll().Where(p => p.Id == data.ContactId).FirstOrDefault();
                    data.ContactName = qry1.Name;
                    if (qry.AccountManagerId > 0)
                        data.SalesManId = qry.AccountManagerId;
                    var Account = (from u in UserManager.Users where u.Id == data.SalesManId select u).FirstOrDefault();
                    data.SalesMan = Account.UserName;

                }
                sr.select2inq = enquirys;

            }

            return sr;
        }

        public async Task<Select2Team> GetTeam()
        {
            long userid = (int)AbpSession.UserId;

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            Select2Team sr = new Select2Team();

            var team = (from c in _TeamRepository.GetAll() select c).ToArray();

            if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                team = (from r in _TeamRepository.GetAll().Where(p => p.SalesManagerId == userid) select r).ToArray();
            }
            else if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Coordinator / Sales Executive")
            {
                team = (from r in _TeamRepository.GetAll()
                        join t in _TeamDetailRepository.GetAll() on r.Id equals t.TeamId
                        where t.SalesmanId == userid
                        select r).ToArray();
            }

            if (team.Length > 0)
            {
                var teamArray = (from c in team
                                 select new Select2TeamDto
                                 {
                                     Id = c.Id,
                                     Name = c.Name,
                                     DepartmentId = c.DepartmentId,
                                     DepartmentName = ""
                                 }).ToArray();
                foreach (var data in teamArray)
                {
                    var dapartment = _departmentRepository.GetAll().Where(p => p.Id == data.DepartmentId).FirstOrDefault();
                    data.DepartmentName = dapartment.DepatmentName;
                }
                sr.selectData = teamArray;
            }
            return sr;

        }

        public async Task<Select2Result> GetProductCategory()
        {
            Select2Result sr = new Select2Result();
            var Category = (from c in _productCategoryRepository.GetAll() select c).ToArray();

            if (Category.Length > 0)
            {
                var Categorys = (from c in Category select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = Categorys;
            }

            return sr;
        }

        public async Task<Select2Result> GetWhyBafco()
        {
            Select2Result sr = new Select2Result();
            var src = (from c in _ybafcoRepository.GetAll() select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.YbafcoName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Result> GetOpportunitySource()
        {

            Select2Result sr = new Select2Result();
            var src = (from c in _opportunitySourceRepository.GetAll() select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }

        public async Task<Select2Result> GetStagestate()
        {
            Select2Result sr = new Select2Result();
            var stage = (from c in _StagestateRepository.GetAll() select c).ToArray();

            if (stage.Length > 0)
            {
                var stageDtos = (from c in stage select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = stageDtos;
            }

            return sr;
        }

        public async Task<Select2Company> GetCompanyDetails(Select2CompanyInput input)
        {
            var company = (from c in _newCompanyRepositary.GetAll().Where(p => p.Name == input.Name && p.Id == 0) select c);
            var output = new Select2Company();
            if(input.CompanyId > 0)
            {
                 company = (from c in _newCompanyRepositary.GetAll().Where(p => p.Name == input.Name && p.Id == input.CompanyId) select c);
            }
            else if (input.Name != null)
            {
                 company = (from c in _newCompanyRepositary.GetAll().Where(p => p.Name == input.Name && p.Name == input.Name) select c);
            }

            if (company.Count() > 0)
            {
                var companyArray = (from c in company select new Select2CompanyDto
                {
                    CompanyId = c.Id,
                    CompanyName = c.Name,
                    SalesManId = c.AccountManagerId ?? 0,
                    SalesMan = "",
                    TeamId = 0,
                    TeamName = "",
                    DivisionId = 0,
                    DivisionName = "",
                    Email = "",
                    Phonenumber = "",
                    Website = ""
            }).FirstOrDefault();

           if(input.CompanyId > 0)
                {
                    var email = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == companyArray.CompanyId && p.NewInfoTypeId == 4).FirstOrDefault();
                    var phone = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == companyArray.CompanyId && p.NewInfoTypeId == 9).FirstOrDefault();
                    var website = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == companyArray.CompanyId && p.NewInfoTypeId == 10).FirstOrDefault();
                    if (email != null)
                    {
                        companyArray.Email = email.InfoData;
                    }
                    if (phone != null)
                    {
                        companyArray.Phonenumber = phone.InfoData;
                    }
                    if (website != null)
                    {
                        companyArray.Website = website.InfoData;
                    }
                }

                if (companyArray.SalesManId > 0 )
                {
             
                    var SalesmanName = (from s in UserManager.Users where s.Id == companyArray.SalesManId select s.UserName).FirstOrDefault();
                    companyArray.SalesMan = SalesmanName;

                    var TeamInfo = (from q in _TeamDetailRepository.GetAll()
                                    join r in _TeamRepository.GetAll() on q.TeamId equals r.Id
                                    where q.SalesmanId == companyArray.SalesManId
                                    select r).FirstOrDefault();

                    if (TeamInfo != null)
                    {
                        var TeamDivisionName = (from d in _departmentRepository.GetAll() where d.Id == TeamInfo.DepartmentId select d.DepatmentName).FirstOrDefault();

                        companyArray.TeamId = TeamInfo.Id;
                        companyArray.TeamName = TeamInfo.Name;
                        companyArray.DivisionId = TeamInfo.DepartmentId;
                        companyArray.DivisionName = TeamDivisionName;
                    }
                    else
                    {
                        var TeamDetails = (from r in _TeamRepository.GetAll() where r.SalesManagerId == companyArray.SalesManId select r).FirstOrDefault();
                        if (TeamDetails != null)
                        {
                            var TeamDivisionName = (from d in _departmentRepository.GetAll() where d.Id == TeamDetails.DepartmentId select d.DepatmentName).FirstOrDefault();
                            companyArray.TeamId = TeamDetails.Id;
                            companyArray.TeamName = TeamDetails.Name;
                            companyArray.DivisionId = TeamDetails.DepartmentId;
                            companyArray.DivisionName = TeamDivisionName;

                        }
                    }
                }
               
                output.select2Company = companyArray;
            }

            return output;
        }
        public async Task<Select4Result> GetAllCompanyEnquiry(Select2Input input)
        {
            Select4Result sr = new Select4Result();

            long userid = (int)AbpSession.UserId;

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var company = (from c in _newCompanyRepositary.GetAll() where c.Name.Contains(input.Name) select c).ToArray();

            if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Manager / Sales Executive" || userrole.DisplayName == "Sales Coordinator / Sales Executive" || userrole.DisplayName == "Sales Coordinator / Sales Executive")
            {
                company = (from c in _newCompanyRepositary.GetAll().Where(p => p.AccountManagerId == userid) select c).ToArray();

                if (company.Length > 0)
                {
                    var companyArray = (from c in company select new datadto3 { Id = c.Id, Name = c.Name, IndustryId = c.IndustryId }).ToArray();
                    sr.select4data = companyArray;
                }
            }
            else if (userrole.DisplayName == "Sales Manager")
            {
                var teamSalesman = (from q in _TeamDetailRepository.GetAll()
                                    join r in _TeamRepository.GetAll() on q.TeamId equals r.Id
                                    where r.SalesManagerId == userid
                                    select q.SalesmanId).ToArray();
                var companyArray = new List<datadto3>();

                if (teamSalesman.Length > 0)
                {
                    foreach (var data in teamSalesman)
                    {
                        var companySales = (from c in _newCompanyRepositary.GetAll() where c.AccountManagerId == data && c.Name.Contains(input.Name) select c).ToArray();
                        if (companySales.Length > 0)
                        {
                            foreach (var item in companySales)
                            {
                                companyArray.Add(new datadto3 { Id = item.Id, Name = item.Name, IndustryId = item.IndustryId });
                            }

                        }

                    }
                    sr.select4data = companyArray.ToArray();
                }

            }
            else
            {
                if (company.Length > 0)
                {
                    var companyArray = (from c in company select new datadto3 { Id = c.Id, Name = c.Name, IndustryId = c.IndustryId }).ToArray();
                    sr.select4data = companyArray;
                }
            }


            return sr;
        }

        public async Task<Select2Result> GetLeadStatus()
        {

            Select2Result sr = new Select2Result();
            var src = (from c in _leadStatusRepository.GetAll() where c.Id != 5 select c).ToArray();

            if (src.Length > 0)
            {
                var srcarray = (from c in src select new datadto { Id = c.Id, Name = c.LeadStatusName }).ToArray();
                sr.select2data = srcarray;
            }
            return sr;
        }
        public async Task<Select2Discount> GetCompanyDiscount(NullableIdDto input)
        {
            Select2Discount sr = new Select2Discount();
            var query = (from c in _newCompanyRepositary.GetAll().Where(p => p.Id == input.Id) select c).ToArray();
            if (query.Length > 0 && query[0].Discountable > 0 && query[0].UnDiscountable > 0)
            {
                var discountdto = (from c in query select new discountdatadto { Discountable = c.Discountable, UnDiscountable = c.UnDiscountable }).ToArray();
                sr.select2data = discountdto;
            }
            else
            {
                var disquery = _discountRepository.GetAll().FirstOrDefault();
                if (disquery != null)
                {
                    var query1 = (from c in _discountRepository.GetAll().Where(p => p.Id == disquery.Id) select c).ToArray();
                    if (query.Length > 0)
                    {
                        var discountdto = (from c in query1 select new discountdatadto { Discountable = (int)c.Discountable, UnDiscountable = (int)c.UnDiscountable }).ToArray();
                        sr.select2data = discountdto;
                    }
                }
            }
            return sr;
        }

        public async Task<Select2productdetailsdto> GetTemporaryProducts(Select2Input input)
        {
            Select2productdetailsdto sr = new Select2productdetailsdto();
            var query = (from c in _temporaryProductRepository.GetAll() where c.ProductCode.Contains(input.Name) && c.Updated == false select c);

            if (query.Count() > 0)
            {
                var productdtos = (from c in query
                                   select new Productdetailsdto
                                   {
                                       Id = c.Id,
                                       ProductCode = c.ProductCode,
                                       ProductName = c.ProductName,
                                       Description = c.Description,
                                       ImageUrl = "",
                                       Price = c.Price != null ? (decimal)c.Price : 0,
                                   }).ToArray();

                if (productdtos.Length > 0)
                {

                    foreach (var pro in productdtos)
                    {
                        var imageqry = _TempProductImageRepository.GetAll().Where(p => p.TemporaryProductId == pro.Id).FirstOrDefault();

                        if (imageqry != null)
                            pro.ImageUrl = imageqry.ImageUrl;
                    }
                }

                sr.select2data = productdtos.ToArray();

            }

            return sr;
        }
        public async Task<Select2Result> GetUserDesignation()
        {
            Select2Result sr = new Select2Result();
            var user = (from c in _userDesignationRepository.GetAll() select c).ToArray();

            if (user.Length > 0)
            {
                var userDtos = (from c in user select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = userDtos;
            }

            return sr;
        }

        public async Task<Select2CategoryResult> GetProductCategoryAll()
        {
            Select2CategoryResult sr = new Select2CategoryResult();
            var list1 = new List<categorydto>();
            var Category = (from c in _productCategoryRepository.GetAll() select c).ToArray();
            foreach (var addlist in Category)
            {
                list1.Add(new categorydto { Id = addlist.Id, Name = addlist.Name, Backgroundcolor = "#FFFFFF" });
            }
            sr.select2data = list1.OrderByDescending(c => c.Id).ToArray();
            return sr;
        }
        public async Task<Select2Result> GetProductSpecificationCategoryBased(NullableIdDto input)
        {
            Select2Result sr = new Select2Result();
            if (input.Id > 0)
            {
                var ProdDto = (from ps in _ProductSpecificationRepository.GetAll()
                               join pg in _productGroupRepositary.GetAll() on ps.ProductGroupId equals pg.Id
                               where pg.ProductCategoryId == input.Id
                               select ps);

                if (ProdDto.Count() > 0)
                {
                    var ProdDtos = (from c in ProdDto select new datadto { Id = c.Id, Name = c.Name });
                    sr.select2data = ProdDtos.ToArray();
                }
            }
            return sr;
        }

        public async Task<Select2Result> GetProductState()
        {

            Select2Result sr = new Select2Result();
            var state = (from c in _ProductStatesRepository.GetAll() select c).ToArray();

            if (state.Length > 0)
            {
                var prodstate = (from c in state select new datadto { Id = c.Id, Name = c.Name }).ToArray();
                sr.select2data = prodstate;
            }
            return sr;
        }
        //public async Task<Select2View> GetViews()
        //{

        //    Select2View sr = new Select2View();
        //    var views = (from c in _viewRepository.GetAll() select c).ToArray();

        //    if (views.Length > 0)
        //    {
        //        var viewslist = (from c in views select new ViewDto { Id = c.Id, Name = c.Name,IsEditable = c.IsEditable }).ToArray();
        //        sr.Select5data = viewslist;
        //    }
        //    return sr;
        //}
        public async Task<Select2View> GetViews()
        {
            Select2View sr = new Select2View();
            var viewslist = new List<ViewDto>();
            long userid = (int)AbpSession.UserId;

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var view = (from c in _viewRepository.GetAll() where c.IsEditable == false select c).ToArray();
            var views = (from c in _viewRepository.GetAll() where c.IsEditable == true select c).ToArray();

            foreach (var item in view)
            {
                viewslist.Add(new ViewDto { Id = item.Id, Name = item.Name,IsEditable = item.IsEditable });
            }

            if (userrole.DisplayName == "Sales Executive" || userrole.DisplayName == "Sales Coordinator" || userrole.DisplayName == "Designer")
            {
                views = (from c in views where c.CreatorUserId == userid select c).ToArray();

                if (views.Length > 0)
                {
                    foreach (var item in views)
                    {
                        viewslist.Add(new ViewDto { Id = item.Id, Name = item.Name, IsEditable = item.IsEditable });
                    }
                }
            }
            else
            {
                if (views.Length > 0)
                {
                    views = (from c in views  select c).ToArray();
                    if (views.Length > 0)
                    {
                        foreach (var item in views)
                        {
                            viewslist.Add(new ViewDto { Id = item.Id, Name = item.Name, IsEditable = item.IsEditable });
                        }
                    }
                }
            }
            sr.Select5data = viewslist.ToArray();
            return sr;
        }

        public async Task<Select2Column> GetReportColumn()
        {
            Select2Column sr = new Select2Column();
            var ReportColumn = (from r in _ReportColumnRepository.GetAll() select r).ToArray();
            if (ReportColumn.Length > 0)
            {
                var ReportColumnArray = (from r in ReportColumn select new ColumnDto { Id = r.Id, Name = r.Name ,Code = r.Code}).ToArray();
                sr.Select5data = ReportColumnArray;
            }
            return sr;
        }
        public async Task<Select2Result> GetReportFilters()
         {
                Select2Result sr = new Select2Result();
                var Reportfilter = (from r in _DateFilterRepository.GetAll() select r).ToArray();
                if (Reportfilter.Length > 0)
                {
                    var ReportColumnArray = (from r in Reportfilter select new datadto { Id = r.Id, Name = r.Name }).ToArray();
                    sr.select2data = ReportColumnArray;
                }
                return sr;
         }
        public async Task<Select3Result> GetReportAllPerson()
        {
            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 6 || role.RoleId == 7 || role.RoleId == 4 || role.RoleId == 6 || role.RoleId == 10
                               select c).ToArray();

                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.FullName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }
        public async Task<Select3UserResult> GetUserProfile()
        {
            List<userprofiledto> UserDto = new List<userprofiledto>();

            Select3UserResult sr = new Select3UserResult();
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = (from U in UserManager.Users where U.Id == 0 select new userprofiledto
            {
            }  ); 

            if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from U in UserManager.Users
                         join TD in _TeamDetailRepository.GetAll() on U.Id equals TD.SalesmanId
                         join T in _TeamRepository.GetAll() on TD.TeamId equals T.Id
                         join R in _userRoleRepository.GetAll() on TD.SalesmanId equals R.UserId
                         where T.SalesManagerId == userid
                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();

            }
            else if (userrole.DisplayName == "Sales Executive")
            {
                query = (from U in UserManager.Users
                         join TD in _TeamDetailRepository.GetAll() on U.Id equals TD.SalesmanId
                         join R in _userRoleRepository.GetAll() on TD.SalesmanId equals R.UserId
                         where TD.SalesmanId == userid && R.RoleId == 4 || R.RoleId == 17

                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();

            }
            else
            {
                query = (from U in UserManager.Users
                         join TD in _TeamDetailRepository.GetAll() on U.Id equals TD.SalesmanId
                         join role in _userRoleRepository.GetAll() on TD.SalesmanId equals role.UserId
                         where role.RoleId == 4 || role.RoleId == 17
                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();
                query = (from U in UserManager.Users
                         join T in _TeamRepository.GetAll() on U.Id equals T.SalesManagerId
                         join R in _userRoleRepository.GetAll() on T.SalesManagerId equals R.UserId
                         where R.RoleId == 10
                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                var datas = query.ToList();
               foreach(var d in datas)
                {
                    UserDto.Add(new userprofiledto { Id = d.Id, Name = d.Name, ProfilePictureId = d.ProfilePictureId });
                }

            }



            if (userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
                UserDto.Add(new userprofiledto { Id = user.Id, Name = user.UserName, ProfilePictureId = user.ProfilePictureUrl });
            }

            sr.select3data = UserDto.ToArray();
            
            return sr;
        }
        public async Task<Select3UserResult> GetUserSalesManager()
        {
            var UserDto = new List<userprofiledto>();
            Select3UserResult sr = new Select3UserResult();

            var query = UserManager.Users.ToList();
            foreach(var data in query)
            {
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(data);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Assignation.SalesManager").ToList();
                if(count.Count() > 0)
                {
                    UserDto.Add(new userprofiledto { Id = data.Id, Name = data.UserName, ProfilePictureId = data.ProfilePictureUrl });
                }

            }
            sr.select3data = UserDto.ToArray();
            return sr;
        }
        public async Task<Select3UserResult> GetUserSalesManagerToTeam()
        {
            var UserDto = new List<userprofiledto>();
            Select3UserResult sr = new Select3UserResult();

            var query = UserManager.Users.ToList();
            foreach (var data in query)
            {
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(data);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Assignation.SalesManager").ToList();
                if (count.Count() > 0)
                {
                    var Team = _TeamRepository.GetAll().Where(p => p.SalesManagerId == data.Id).FirstOrDefault();
                    if (Team == null)
                    {
                        UserDto.Add(new userprofiledto { Id = data.Id, Name = data.UserName, ProfilePictureId = data.ProfilePictureUrl });
                    }
                }

            }
            sr.select3data = UserDto.ToArray();
            return sr;
        }
        public async Task<Select3UserResult> GetUserSalesPerson()
        {
            var UserDto = new List<userprofiledto>();
            Select3UserResult sr = new Select3UserResult();

            var query = UserManager.Users.ToList();
            foreach (var data in query)
            {
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(data);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Assignation.Salesperson").ToList();
                if (count.Count() > 0)
                {
                    UserDto.Add(new userprofiledto { Id = data.Id, Name = data.UserName, ProfilePictureId = data.ProfilePictureUrl });
                }

            }
            sr.select3data = UserDto.ToArray();
            return sr;
        }
        public async Task<Select3UserResult> GetUserSalesPersonfromTeam(NullableIdDto input)
        {
            var UserDto = new List<userprofiledto>();
            Select3UserResult sr = new Select3UserResult();

            var query = UserManager.Users.ToList();
            foreach (var data in query)
            {
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(data);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Assignation.Salesperson").ToList();
                if (count.Count() > 0)
                {
                    UserDto.Add(new userprofiledto { Id = data.Id, Name = data.UserName, ProfilePictureId = data.ProfilePictureUrl });
                }

            }
            sr.select3data = UserDto.ToArray();
            return sr;
        }
        public async Task<Select3UserResult> GetUserSalesPersontoTeam()
        {
            var UserDto = new List<userprofiledto>();
            Select3UserResult sr = new Select3UserResult();

            var query = UserManager.Users.ToList();
            foreach (var data in query)
            {
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(data);
                var count = grantedPermissions.Where(p => p.Name == "Pages.Assignation.Salesperson").ToList();
                if (count.Count() > 0)
                {
                    var Teamdetail = _TeamDetailRepository.GetAll().Where(p => p.SalesmanId == data.Id).FirstOrDefault();
                    if (Teamdetail == null)
                    {
                        UserDto.Add(new userprofiledto { Id = data.Id, Name = data.UserName, ProfilePictureId = data.ProfilePictureUrl });
                    }
                }

            }
            sr.select3data = UserDto.ToArray();
            return sr;
        }
        public List<SliderDataList> GetSalesExecutive(NullableIdDto input)
        {
            var Datas = new List<SliderDataList>();
            string viewquery = "SELECT * FROM [dbo].[View_SliderUser]";
            if (input.Id > 0 && input.Id != 1000)
            {
                viewquery = "SELECT * FROM [dbo].[View_SliderUser] WHERE TeamId = " + input.Id;
            }
            DataTable viewtable = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            try
            {
                SqlConnection con = new SqlConnection(db.ConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand(viewquery, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(viewtable);
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }
            var data = (from DataRow dr in viewtable.Rows
                        select new SliderDataList
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            TeamId = Convert.ToInt32(dr["TeamId"]),
                            Name = Convert.ToString(dr["UserName"]),
                            ProfilePicture = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + Convert.ToString(dr["ProfilePictureUrl"]),
                            Email = Convert.ToString(dr["EmailAddress"]),
                            Phone = Convert.ToString(dr["PhoneNumber"])
                        });
            var Outdata = data.MapTo<List<SliderDataList>>();
            return Outdata;
        }
        public async Task<SelectDResult> GetDashboardTeam()
        {
            SelectDResult sr = new SelectDResult();
            var team = (from r in _TeamRepository.GetAll() select r).ToArray();
            if (team.Length > 0)
            {
                var teamlist = (from r in team join e in UserManager.Users on r.SalesManagerId equals e.Id select new datadtoes { Id = r.Id, Name = e.FullName + " " + "(" + r.Name + ")", Photo = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + e.ProfilePictureUrl }).ToList();
                teamlist.Add(new datadtoes { Id = 1000, Name = "All", Photo = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "/Common/Profile/default-profile-picture.png" });
                sr.selectDdata = teamlist.ToArray();

            }
            return sr;
        }
        public async Task<Select3Result> GetDesigners()
        {
            long userId = (int)AbpSession.UserId;
            var userRole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userId
                            select role).FirstOrDefault();

            Select3Result sr = new Select3Result();
            {
                var Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 7
                               select c).ToArray();

                if (userRole.DisplayName == "Designer")
                {
                    Account = (from c in UserManager.Users
                               join role in _userRoleRepository.GetAll() on c.Id equals role.UserId
                               where role.RoleId == 7 && c.Id == userId
                               select c).ToArray();
                }
                var Accounts = (from c in Account select new datadtos { Id = c.Id, Name = c.UserName }).ToArray();
                sr.select3data = Accounts;
            }

            return sr;
        }
        public async Task<Select3UserResult> GetDesignerProfile()
        {
            List<userprofiledto> UserDto = new List<userprofiledto>();

            Select3UserResult sr = new Select3UserResult();
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = (from U in UserManager.Users
                         where U.Id == 0
                         select new userprofiledto
                         {
                         });


            if (userrole.DisplayName != "Designer")
            {
                query = (from U in UserManager.Users
                         join R in _userRoleRepository.GetAll() on U.Id equals R.UserId
                         where R.RoleId == 7

                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });

                UserDto = query.ToList();

                UserDto.Add(new userprofiledto
                {
                    Id = 1002,
                    Name = "W/O Designer",
                    ProfilePictureId = "Common/Profile/default-profile-picture.png",
                });
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from U in UserManager.Users
                         join R in _userRoleRepository.GetAll() on U.Id equals R.UserId
                         where R.RoleId == 7 && U.Id == userid
                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();
            }

            sr.select3data = UserDto.ToArray();

            return sr;
        }
        public async Task<Select3UserResult> GetCoordinatrProfile()
        {
            List<userprofiledto> UserDto = new List<userprofiledto>();

            Select3UserResult sr = new Select3UserResult();
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = (from U in UserManager.Users
                         where U.Id == 0
                         select new userprofiledto
                         {
                         });


            if (userrole.DisplayName != "Sales Coordinator")
            {
                query = (from U in UserManager.Users
                         join R in _userRoleRepository.GetAll() on U.Id equals R.UserId
                         where R.RoleId == 6

                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();
                UserDto.Add(new userprofiledto
                {
                    Id = 1001,
                    Name = "W/O Coordinator",
                    ProfilePictureId = "Common/Profile/default-profile-picture.png",
                });

            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from U in UserManager.Users
                         join R in _userRoleRepository.GetAll() on U.Id equals R.UserId
                         where R.RoleId == 6 && U.Id == userid
                         select new userprofiledto
                         {
                             Id = U.Id,
                             Name = U.UserName,
                             ProfilePictureId = U.ProfilePictureUrl
                         });
                UserDto = query.ToList();
            }

            sr.select3data = UserDto.ToArray();

            return sr;
        }

    }
    public class categorydto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Backgroundcolor { get; set; }
    }
    public class Select2CategoryResult
    {
        public categorydto[] select2data { get; set; }
    }
    public class discountdatadto
    {
        public int Discountable { get; set; }
        public int UnDiscountable { get; set; }

    }
    public class Select2Discount
    {
        public discountdatadto[] select2data { get; set; }
    }

    public class datadto3
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IndustryId { get; set; }

    }
    public class Select4Result
    {
        public datadto3[] select4data { get; set; }
    }
    public class Select2CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long? SalesManId { get; set; }
        public string SalesMan { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Website { get; set; }
    }
    public class Select2Company
    {
        public Select2CompanyDto select2Company { get; set; }
    }

    public class Select2Team
    {
        public Select2TeamDto[] selectData { get; set; }
    }
    public class Select2TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
    public class Select2Attribute
    {
        public AttributeDto[] select3data { get; set; }
    }
    public class AttributeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class Select3Result
    {
        public datadtos[] select3data { get; set; }
    }
    public class proddto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductFamily { get; set; }

    }
    public class Select2Product
    {
        public proddto[] select2data { get; set; }
    }
    public class datadtos
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class LocationInput
    {
        public int Id { get; set; }
    }
    public class datadto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Select2Result
    {
        public datadto[] select2data { get; set; }
    }
    public class Stagedto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Value { get; set; }
        public string ColorCode { get; set; }
        public string Status { get; set; }

    }
    public class Stage2Result
    {
        public Stagedto[] Select2data { get; set; }
    }

    public class citydto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

    }
    public class Select2City
    {
        public citydto[] select2data { get; set; }
    }

    public class contactdto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }

    public class Productdetailsdto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int SpecificationId { get; set; }
        public string SpecificationName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool Discount { get; set; }

    }
    public class Select2productdetailsdto
    {
        public Productdetailsdto[] select2data { get; set; }
    }

    public class Select2Contact
    {
        public contactdto[] select2data { get; set; }
    }
    public class deptdto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
    }
    public class Select2ResultDept
    {
        public deptdto[] select2data { get; set; }
    }

    public class Select2sales
    {
        public Select2salesDto[] selectCompdata { get; set; }
    }
    public class Select2salesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long? SalesManId { get; set; }
        public string SalesMan { get; set; }
    }

    public class productdto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Prize { get; set; }
        public int? SpecId { get; set; }
        public bool Discount { get; set; }

    }
    public class Select2product
    {
        public productdto[] select2data { get; set; }
    }

    public class Select2Inquiry
    {
        public Select2InquiryDto[] select2inq { get; set; }
    }
    public class Select2InquiryDto
    {
        public int Id { get; set; }
        public long? SalesManId { get; set; }
        public string SalesMan { get; set; }
        public long? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long? ContactId { get; set; }
        public string ContactName { get; set; }
        
    }
    public class Select2Input
    {
        public string Name { get; set; }

    }
    public class ColumnDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }


    }
    public class Select2Column
    {
        public ColumnDto[] Select5data { get; set; }
    }
    public class ViewDto
    {
        public int Id { get; set; }
        public bool IsEditable { get; set; }
        public string Name { get; set; }


    }
    public class Select2View
    {
        public ViewDto[] Select5data { get; set; }
    }

    public class userprofiledto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProfilePictureId { get; set; }
    }
    public class Select3UserResult
    {
        public userprofiledto[] select3data { get; set; }
    }

    public class SliderDataList
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int TeamId { get; set; }

        public decimal Conversionratio { get; set; }
        public decimal TConversionratio { get; set; }

        public int ConversionCount { get; set; }
        public int TConversionCount { get; set; }

    }
    public class SelectDResult
    {
        public datadtoes[] selectDdata { get; set; }
    }
    public class datadtoes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public bool IsSales { get; set; }
    }
}
