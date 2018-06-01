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
using tibs.stem.Inquirys.Dto;
using tibs.stem.Milestones;
using tibs.stem.AcitivityTracks;
using tibs.stem.ActivityTrackComments;
using tibs.stem.Sources;
using tibs.stem.Locations;
using tibs.stem.Companies.Dto;
using tibs.stem.Locations.Dto;
using tibs.stem.Citys;
using tibs.stem.Designations;
using tibs.stem.EnquirySources;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.NewCompanyContacts.Dto;
using tibs.stem.EnquiryDetails;
using tibs.stem.Authorization.Users.Profile.Dto;
using tibs.stem.Storage;
using tibs.stem.Inquirys.Exporting;
using tibs.stem.Dto;
using tibs.stem.Industrys;
using tibs.stem.LeadDetails;
using tibs.stem.Quotationss.Dto;
using tibs.stem.Quotations;
using Microsoft.AspNetCore.Identity;
using Abp.Authorization.Users;
using tibs.stem.Authorization.Roles;
using tibs.stem.EnquiryContacts;
using tibs.stem.Team;
using System.Globalization;
using System.Drawing;
using System.Linq.Dynamic;
using tibs.stem.Discounts;
using tibs.stem.Views;
using tibs.stem.QuotationStatuss;
using tibs.stem.Departments;

namespace tibs.stem.Inquirys
{
    public class InquiryAppService : stemAppServiceBase, IInquiryAppService
    {
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<MileStone> _milestoneRepository;
        private readonly IRepository<AcitivityTrack> _acitivityTrackRepository;
        private readonly IRepository<ActivityTrackComment> _activityTrackCommentsRepository;
        private readonly IRepository<Source> _sourceRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<City> _CityRepository;
        private readonly IRepository<Designation> _DesignationRepository;
        private readonly IRepository<EnquirySource> _enquirySourceRepository;
        private readonly IRepository<NewCompany> _NewCompanyRepository;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;
        private readonly IRepository<NewContact> _NewContactRepository;
        private readonly IRepository<NewInfoType> _NewInfoTypeRepository;
        private readonly IRepository<NewAddressInfo> _NewAddressInfoRepository;
        private readonly IRepository<NewContactInfo> _NewContactInfoRepository;
        private readonly IRepository<LeadDetail> _LeadDetailRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IGeneralInquiryListExcelExporter _generalInquiryListExcelExporter;
        private readonly ISalesInquiryListExcelExporter _salesInquiryListExcelExporter;
        private readonly ILeadInquiryListExcelExporter _leadInquiryListExcelExporter;
        private readonly IRepository<Industry> _IndustryRepository;
        private readonly IRepository<Quotation> _quotationRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<EnquiryContact> _enquiryContactRepository;
        private readonly IRepository<Teams> _TeamRepository;
        private readonly IRepository<JobActivity> _jobActivityRepository;
        private readonly IRepository<Discount> _DiscountRepository;
        private readonly IClosedInquiryListExcelExporter _closedInquiryListExcelExporter;
        private readonly ISalesQuotationsListExcelExporter _salesQuotationsListExcelExporter;
        private readonly IRepository<View> _viewRepository;
        private readonly IRepository<QuotationStatus> _quotationstatusRepository;
        private readonly IRepository<Department> _DepartmentsRepository;
        public InquiryAppService(
            IBinaryObjectManager binaryObjectManager,
            IRepository<Discount> DiscountRepository,
            IRepository<QuotationStatus> quotationstatusRepository,
            IRepository<Department> DepartmentsRepository,
            ISalesQuotationsListExcelExporter salesQuotationsListExcelExporter,
            IRepository<EnquirySource> enquirySourceRepository,
            IRepository<EnquiryDetail> enquiryDetailRepository,
            IRepository<NewCompany> NewCompanyRepository,
            IRepository<City> CityRepository,
            IRepository<Designation> DesignationRepository,
            IRepository<Source> sourceRepository,
            IRepository<Location> locationRepository,
            IRepository<ActivityTrackComment> activityTrackCommentsRepository,
            IRepository<AcitivityTrack> acitivityTrackRepository, 
            IRepository<Inquiry> inquiryRepository, 
            IRepository<MileStone> milestoneRepository,
            IRepository<NewContact> NewContactRepository,
            IRepository<NewInfoType> NewInfoTypeRepository,
            IRepository<NewAddressInfo> NewAddressInfoRepository,
            IRepository<NewContactInfo> NewContactInfoRepository,
            IRepository<Industry> IndustryRepository,
            IRepository<LeadDetail> LeadDetailRepository,
            IGeneralInquiryListExcelExporter generalInquiryListExcelExporter,
            ISalesInquiryListExcelExporter salesInquiryListExcelExporter,
            IRepository<Quotation> quotationRepository,
            ILeadInquiryListExcelExporter leadInquiryListExcelExporter,
            RoleManager roleManager,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<EnquiryContact> enquiryContactRepository,
            IRepository<Teams> TeamRepository,
            IRepository<JobActivity> jobActivityRepository,
            IClosedInquiryListExcelExporter closedInquiryListExcelExporter,
            IRepository<View> viewRepository
            )
        {
            _inquiryRepository = inquiryRepository;
            _milestoneRepository = milestoneRepository;
            _quotationstatusRepository = quotationstatusRepository;
            _DepartmentsRepository = DepartmentsRepository;
            _acitivityTrackRepository = acitivityTrackRepository;
            _activityTrackCommentsRepository = activityTrackCommentsRepository;
            _sourceRepository = sourceRepository;
            _locationRepository = locationRepository;
            _DiscountRepository = DiscountRepository;
            _CityRepository = CityRepository;
            _DesignationRepository = DesignationRepository;
            _enquirySourceRepository = enquirySourceRepository;
            _NewCompanyRepository = NewCompanyRepository;
            _enquiryDetailRepository = enquiryDetailRepository;
            _binaryObjectManager = binaryObjectManager;
            _generalInquiryListExcelExporter = generalInquiryListExcelExporter;
            _salesInquiryListExcelExporter = salesInquiryListExcelExporter;
            _leadInquiryListExcelExporter = leadInquiryListExcelExporter;
            _NewContactRepository = NewContactRepository;
            _NewInfoTypeRepository = NewInfoTypeRepository;
            _NewAddressInfoRepository = NewAddressInfoRepository;
            _NewContactInfoRepository = NewContactInfoRepository;
            _IndustryRepository = IndustryRepository;
            _LeadDetailRepository = LeadDetailRepository;
            _quotationRepository = quotationRepository;
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;
            _enquiryContactRepository = enquiryContactRepository;
            _TeamRepository = TeamRepository;
            _jobActivityRepository = jobActivityRepository;
            _closedInquiryListExcelExporter = closedInquiryListExcelExporter;
            _salesQuotationsListExcelExporter = salesQuotationsListExcelExporter;
            _viewRepository = viewRepository;
        }

        public async Task<PagedResultDto<InquiryListDto>> GetInquiry(GetInquiryInput input)
        {
            var query = _inquiryRepository.GetAll().Where(p => p.MileStoneId < 5 && p.Junk == null && p.IsClosed != true)
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p =>
                     p.CompanyName.Contains(input.Filter) ||
                     p.Name.Contains(input.Filter) ||
                     p.Designations.DesiginationName.Contains(input.Filter) ||
                     p.MileStones.MileStoneName.Contains(input.Filter) ||
                     p.Email.Contains(input.Filter)
                );

            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStoneId > 0 ? a.MileStones.MileStoneName : "",
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               CompanyName = enqDetail.CompanyId > 0 ? enqDetail.Companys.Name : a.CompanyName,
                               DepartmentName = enqDetail.DepartmentId > 0 ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId ?? 0,
                               TeamName = enqDetail.TeamId > 0 ? enqDetail.Team.Name : "",
                               CreatedBy = ur != null ? ur.UserName : "",
                               SalesMan = pr != null ? pr.UserName : "",
                               CreationTime = a.CreationTime
                           });
            var inquiryCount = await inquiry.CountAsync();

            var inquirys = await inquiry
              .OrderByDescending(p => p.CreationTime)
              .PageBy(input)
              .ToListAsync();

            if (input.Sorting != "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy")
            {
                inquirys = await inquiry
              .OrderBy(input.Sorting)
              .PageBy(input)
              .ToListAsync();
            }          
            var inquirylistoutput = inquirys.MapTo<List<InquiryListDto>>();
            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<PagedResultDto<InquiryListDto>> GetJunkInquiry2(GetInquiryInput input)
        {
            var query = _inquiryRepository.GetAll().Where(p => p.Junk == true)
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p =>
                     p.CompanyName.Contains(input.Filter) ||
                     p.Name.Contains(input.Filter) ||
                     p.Designations.DesiginationName.Contains(input.Filter) ||
                     p.MileStones.MileStoneName.Contains(input.Filter) ||
                     p.Email.Contains(input.Filter)
                );

            var inquiry = (from a in query
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.MileStoneName,
                               CompanyName = a.CompanyName,
                               DesignationName = a.DesignationName,
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               JunkDate = a.JunkDate,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime
                           });

            var datas = inquiry.ToList();
            try
            {
                foreach (var data in datas)
                {
                    var test = (from enqDetail in _enquiryDetailRepository.GetAll()
                                where enqDetail.InquiryId == data.Id
                                select new enqDetailDt
                                {
                                    CompanyId = enqDetail.CompanyId,
                                    DesignationId = enqDetail.DesignationId,
                                    CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : data.CompanyName,
                                    DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : data.DesignationName,
                                    DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                    DepartmentId = enqDetail.DepartmentId ?? 0,
                                    AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                    AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                    TeamId = enqDetail.TeamId ?? 0,
                                    TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : ""

                                }).FirstOrDefault();


                    data.CompanyId = test.CompanyId;
                    data.DesignationId = test.DesignationId;
                    data.CompanyName = test.CompanyName;
                    data.DesignationName = test.DesignationName;
                    data.DepartmentName = test.DepartmentName;
                    data.DepartmentId = test.DepartmentId;
                    data.AssignedbyId = test.AssignedbyId;
                    data.AssignedTime = test.AssignedTime;
                    data.TeamId = test.TeamId;
                    data.TeamName = test.TeamName;

                }
            }
            catch (Exception ex)
            {

            }

            var inquiryCount = datas.Count();

            if (input.Sorting == "DesignationName,CompanyName")
            {
                datas = datas.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(a => a.CreationOrModification).ToList();
            }
            else
            {
                //datas = datas.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            }


            var inquirylistoutput = datas.MapTo<List<InquiryListDto>>();
            foreach (var data in inquirylistoutput)
            {
                if (data.CreatorUserId > 0)
                {
                    try
                    {
                        var user = await UserManager.GetUserByIdAsync(data.CreatorUserId);
                        data.CreatedBy = user.UserName;
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }

            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<PagedResultDto<InquiryListDto>> GetSalesInquiry(GetInquiryInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && enqDetail.AssignedbyId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && team.SalesManagerId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.DesignerId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.CoordinatorId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed != true);

            }

           
            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.MileStoneName,
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               DesignationId = enqDetail.DesignationId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy = ur != null ? ur.UserName : "",
                               SalesMan = pr != null ? pr.UserName : "",
                               DisableQuotation = a.DisableQuotation,
                               Won = a.Won,
                               CreationTime = a.CreationTime,
                               Lost = a.Lost,
                               SclosureDate = enqDetail.ClosureDate != null ? enqDetail.ClosureDate.ToString() : "",
                               SlastActivity = enqDetail.LastActivity != null ? enqDetail.LastActivity.ToString() : ""
                           });

            inquiry = inquiry.WhereIf(
                           !input.Filter.IsNullOrEmpty(),
                           p => p.SubMmissionId.Contains(input.Filter) ||
                                p.Name.Contains(input.Filter) ||
                                p.CompanyName.Contains(input.Filter) ||
                                p.MileStoneName.Contains(input.Filter) ||
                                p.DepartmentName.Contains(input.Filter) ||
                                p.TeamName.Contains(input.Filter) ||
                                p.SalesMan.Contains(input.Filter) ||
                                p.CreatedBy.Contains(input.Filter));

            var inquiryCount = await inquiry.CountAsync();

            var inquirys = await inquiry
                .OrderByDescending(p => p.CreationTime)
                .PageBy(input)
                .ToListAsync();


            if(input.Sorting != "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy")
            {
               inquirys = await inquiry
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }

            var inquirylistoutput = ObjectMapper.Map<List<InquiryListDto>>(inquirys);
            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<PagedResultDto<InquiryListDto>> GetSalesInquiryReport(GetInquiryReportInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if(input.Id == 1)
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3);
            }
            else if (input.Id == 2)
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed == true && p.Won != true);
            }
            else if (input.Id == 3)
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed == true && p.CreationTime.Year == DateTime.Now.Year && p.Won != true);
            }
            else if (input.Id == 4)
            {
                if (userrole.DisplayName == "Sales Executive")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                             join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                             where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && enqDetail.AssignedbyId == userid && enq.IsClosed != true
                             select enq
                            );
                }
                else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                             join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                             join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                             where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && team.SalesManagerId == userid && enq.IsClosed != true
                             select enq
                            );
                }
                else if (userrole.DisplayName == "Designer")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                             where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.DesignerId == userid && enq.IsClosed != true
                             select enq
                            );
                }
                else if (userrole.DisplayName == "Sales Coordinator")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                             where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.CoordinatorId == userid && enq.IsClosed != true
                             select enq
                            );
                }
                else
                {
                    query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed != true);

                }
            }
            else if (input.Id == 5)
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed != true && p.Won != true);

            }
            else if (input.Id == 6)
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.Won == true );

            }

           

            query = query.WhereIf(
                 !input.Filter.IsNullOrEmpty(),
                 p =>
                      p.CompanyName.Contains(input.Filter) ||
                      p.Name.Contains(input.Filter) ||
                      p.Designations.DesiginationName.Contains(input.Filter) ||
                      p.MileStones.MileStoneName.Contains(input.Filter) ||
                      p.Email.Contains(input.Filter)
                 );

            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.MileStoneName,
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               DesignationId = enqDetail.DesignationId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy = ur != null ? ur.UserName: "",
                               SalesMan = pr != null ? pr.UserName : "",
                               DisableQuotation = a.DisableQuotation,
                               Won = a.Won,
                               EstimationValue = enqDetail.EstimationValue
                           });
            var inquiryCount = await inquiry.CountAsync();
            var inquirys = await inquiry
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            foreach (var p in inquirys)
            {
                try
                {
                    var MaxMileStonesId = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id select r).Max(c => c.MileStoneId);
                    if (MaxMileStonesId > 0)
                    {
                        var mile = await _milestoneRepository.GetAsync((int)MaxMileStonesId);
                        var fvalue = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id && r.Revised != true && r.Archieved != true && r.Void != true && r.Total > 0 && r.MileStoneId == MaxMileStonesId select r).Min(a => a.Negotiation == true ? a.Total - a.OverAllDiscountAmount : a.Total);
                        if (fvalue > 0)
                        {
                            p.EstimationValue = fvalue;
                            p.EstimationValueformat = p.EstimationValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.');
                            p.MileStoneName = mile != null ? mile.MileStoneName : p.MileStoneName;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }

            var inquirylistoutput = ObjectMapper.Map<List<InquiryListDto>>(inquirys);
            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<PagedResultDto<InquiryListDto>> GetLeadInquiry(GetInquiryInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();
            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from inq in _inquiryRepository.GetAll()
                         join enq in _enquiryDetailRepository.GetAll() on inq.Id equals enq.InquiryId
                         join team in _TeamRepository.GetAll() on enq.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where inq.MileStoneId == 3 || inq.MileStoneId == 4 && inq.Junk == null && team.SalesManagerId == userid && inq.IsClosed != true
                         select inq
                         );
            }

            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId == 3 || p.MileStoneId == 4 && p.Junk == null && p.IsClosed != true);
            }

            query = query
               .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p =>
                    p.Name.Contains(input.Filter) ||
                    p.Designations.DesiginationName.Contains(input.Filter) ||
                    p.MileStones.MileStoneName.Contains(input.Filter) ||
                    p.Email.Contains(input.Filter) ||
                    p.Companys.Name.Contains(input.Filter) ||
                    p.CompanyName.Contains(input.Filter)
               );

            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           where a.IsDeleted == false
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStoneId>0 ? a.MileStones.MileStoneName : "",
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               DesignationId = enqDetail.DesignationId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy =  ur != null ? ur.UserName : "",
                               SalesMan = pr != null ? pr.UserName : "",
                               CreationTime = a.CreationTime
                           });

            var inquiryCount = inquiry.Count();

            var datas = inquiry
              .OrderByDescending(p => p.CreationTime)
              .PageBy(input)
              .ToList();

            if (input.Sorting != "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy")
            {
                datas = inquiry
                              .OrderBy(input.Sorting)
                              .PageBy(input)
                              .ToList();
            }

            var inquirylistoutput = datas.MapTo<List<InquiryListDto>>();

            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<GetInquirys> GetInquiryForEdit(NullableIdDto input)
        {
            var output = new GetInquirys();
            var query = _inquiryRepository
               .GetAll().Where(p => p.Id == input.Id);

            var enqsource = (await _sourceRepository.GetAll().Where(p => p.TypeId == 1 && p.Id < 4).OrderBy(u => u.SourceName).Select(r => new Sourcelist { SourceId = r.Id, SourceName = r.SourceName, IsAssigned = false }).ToArrayAsync());

            var inquiry = (from a in query
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId ?? 0,
                               MileStoneName = a.MileStones.MileStoneName ?? "",
                               CompanyName = a.CompanyName,
                               DesignationName = a.DesignationName,
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               LandlineNumber = a.LandlineNumber,
                               Junk = a.Junk,
                               JunkDate = a.JunkDate,
                               StatusId = a.StatusId ?? 0,
                               StatusColorCode = a.EnqStatus.EnqStatusColor ?? "",
                               StatusName = a.EnqStatus.EnqStatusName ?? "",
                               Opportunity = (int)(a.EnqStatus.Percentage ?? 0),
                               WhyBafcoId = a.WhyBafcoId ?? 0,
                               OpportunitySourceId = a.OpportunitySourceId ?? 0,
                               WhyBafcoName = a.whyBafco.YbafcoName ?? "",
                               OpportunitySourceName = a.opportunitySource.Name ?? "",
                               LocationId = a.LocationId,
                               LocationName = a.Locations.LocationName ?? "",
                               IsClosed = a.IsClosed,
                               Archieved = a.Archieved,
                               CEmail = a.CEmail,
                               CLandlineNumber = a.CLandlineNumber,
                               CMbNo = a.CMbNo,
                               LeadStatusId = a.LeadStatusId ?? 0,
                               LeadStatusName = a.LeadStatuss.LeadStatusName ?? ""
                           }).FirstOrDefault();

            if (inquiry != null)
            {
                var enqsurce2 = (await _enquirySourceRepository.GetAll().Where(p => p.InquiryId == input.Id).OrderBy(u => u.Source.SourceName).Select(r => new Sourcelist { SourceId = r.SourceId, SourceName = r.Source.SourceName }).ToListAsync());
                foreach (var p in enqsource)
                {
                    foreach (var s in enqsurce2)
                    {
                        if (p.SourceId == s.SourceId)
                        {
                            p.IsAssigned = true;
                        }
                    }
                }

                var datas = (from enqDetail in _enquiryDetailRepository.GetAll()
                             where enqDetail.InquiryId == inquiry.Id
                             select new EnquiryDt
                             {
                                 CompanyId = enqDetail.CompanyId ?? 0,
                                 DesignationId = enqDetail.DesignationId,
                                 CompanyName = enqDetail.CompanyId > 0 ? enqDetail.Companys.Name : inquiry.CompanyName,
                                 DesignationName = enqDetail.DesignationId > 0 ? enqDetail.Designations.DesiginationName : inquiry.DesignationName,
                                 DepartmentName = enqDetail.DepartmentId > 0 ? enqDetail.Departments.DepatmentName : "",
                                 DepartmentId = enqDetail.DepartmentId ?? 0,
                                 AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                 AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                 ContactId = enqDetail.ContactId,
                                 ContactName = enqDetail.ContactId > 0 ? enqDetail.Contacts.Name : "",
                                 LeadTypeId = enqDetail.LeadTypeId ?? 0,
                                 LeadTypeName = enqDetail.LeadTypeId > 0 ? enqDetail.LeadTypes.LeadTypeName : "",
                                 CompatitorsId = enqDetail.CompatitorsId ?? 0,
                                 CompatitorName = enqDetail.CompatitorsId > 0 ? enqDetail.Compatitor.Name : "",
                                 EstimationValue = enqDetail.EstimationValue,
                                 Size = enqDetail.Size,
                                 Summary = enqDetail.Summary,
                                 TeamId = enqDetail.TeamId ?? 0,
                                 TeamName = enqDetail.TeamId > 0 ? enqDetail.Team.Name : "",
                                 Approved = enqDetail.CompanyId > 0 ? enqDetail.Companys.IsApproved : false,
                                 ClosureDate = enqDetail.ClosureDate,
                                 LastActivity = enqDetail.LastActivity

                             }).FirstOrDefault();

                if (datas != null)
                {
                    inquiry.CompanyId = datas.CompanyId;
                    inquiry.DesignationId = datas.DesignationId;
                    inquiry.CompanyName = datas.CompanyName;
                    inquiry.DesignationName = datas.DesignationName;
                    inquiry.DepartmentName = datas.DepartmentName;
                    inquiry.DepartmentId = datas.DepartmentId;
                    inquiry.AssignedbyId = datas.AssignedbyId;
                    if (inquiry.CompanyId > 0)
                    {
                        var compdata = _NewCompanyRepository.GetAll().Where(p => p.Id == inquiry.CompanyId).Select(p => p.Industries).FirstOrDefault();
                        if (compdata != null)
                        {
                            inquiry.IndustryId = compdata.Id;
                            inquiry.IndustryName = compdata.IndustryName ?? "";
                        }
                    }
                    if (datas.AssignedbyId > 0)
                    {
                        byte[] bytes = new byte[0];
                        var Account = (from c in UserManager.Users where c.Id == (long)datas.AssignedbyId select c).FirstOrDefault();
                        var profilePictureId = Account.ProfilePictureId;
                        inquiry.AssignedbyImage = "/assets/common/images/default-profile-picture.png";
                        if (profilePictureId != null)
                        {
                            var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                            if (file != null)
                            {
                                bytes = file.Bytes;
                                GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                inquiry.AssignedbyImage = "data:image/jpeg;base64," + img.ProfilePicture;
                            }
                        }
                    }
                    inquiry.AssignedTime = datas.AssignedTime;
                    inquiry.ContactId = datas.ContactId;
                    inquiry.ContactName = datas.ContactName;
                    inquiry.LeadTypeId = datas.LeadTypeId;
                    inquiry.LeadTypeName = datas.LeadTypeName;
                    inquiry.CompatitorsId = datas.CompatitorsId;
                    inquiry.CompatitorName = datas.CompatitorName;
                    inquiry.EstimationValue = datas.EstimationValue;
                    inquiry.Size = datas.Size;
                    inquiry.Summary = datas.Summary;
                    inquiry.TeamId = datas.TeamId;
                    inquiry.TeamName = datas.TeamName;
                    inquiry.Approved = datas.Approved;
                    inquiry.ClosureDate = datas.ClosureDate;
                    inquiry.LastActivity = datas.LastActivity;
                }

            }
            output = new GetInquirys
            {
                Inquirys = inquiry,
                SelectedSource = enqsource
            };

            if (inquiry != null)
            {
                var leadDetail = _LeadDetailRepository.GetAll().Where(p => p.InquiryId == inquiry.Id);
                if (leadDetail.Count() > 0)
                {
                    var inquirylist = (from a in leadDetail
                                       select new LeadDetailListDto
                                       {
                                           LeadSourceId = a.LeadSourceId ?? 0,
                                           LeadSourceName = a.LeadSourceId != null ? a.LeadSources.LeadSourceName : "",
                                           LeadTypeId = a.LeadTypeId ?? 0,
                                           LeadTypeName = a.LeadTypeId != null ? a.LeadTypes.LeadTypeName : "",
                                           SalesManagerId = a.SalesManagerId ?? 0,
                                           SalesManagerName = a.SalesManagerId != null ? a.SalesManagers.UserName : "",
                                           DesignerId = a.DesignerId ?? 0,
                                           DesignerName = a.DesignerId != null ? a.Designers.UserName : "",
                                           CoordinatorId = a.CoordinatorId ?? 0,
                                           CoordinatorName = a.CoordinatorId != null ? a.Coordinators.UserName : "",
                                           InquiryId = a.InquiryId,
                                           Id = a.Id,
                                           Size = a.Size,
                                           EstimationValue = a.EstimationValue
                                       }).FirstOrDefault();

                    if (inquirylist.CoordinatorId > 0)
                    {
                        byte[] bytes = new byte[0];
                        var Account = (from c in UserManager.Users where c.Id == (long)inquirylist.CoordinatorId select c).FirstOrDefault();
                        var profilePictureId = Account.ProfilePictureId;
                        inquirylist.CoordinatorImage = "/assets/common/images/default-profile-picture.png";
                        if (profilePictureId != null)
                        {
                            var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                            if (file != null)
                            {
                                bytes = file.Bytes;
                                GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                inquirylist.CoordinatorImage = "data:image/jpeg;base64," + img.ProfilePicture;
                            }
                        }
                    }
                    if (inquirylist.DesignerId > 0)
                    {
                        byte[] bytes = new byte[0];
                        var Account = (from c in UserManager.Users where c.Id == (long)inquirylist.DesignerId select c).FirstOrDefault();
                        var profilePictureId = Account.ProfilePictureId;
                        inquirylist.DesignerImage = "/assets/common/images/default-profile-picture.png";
                        if (profilePictureId != null)
                        {
                            var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                            if (file != null)
                            {
                                bytes = file.Bytes;
                                GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                inquirylist.DesignerImage = "data:image/jpeg;base64," + img.ProfilePicture;
                            }
                        }
                    }
                    var Datas = new List<InquiryLockedQuotation>();
                    string viewquery = "SELECT * FROM [dbo].[EnquiryQuotationLock](" + inquiry.Id + ")";
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
                    var dataret = (from DataRow dr in viewtable.Rows
                                   select new InquiryLockedQuotation
                                   {
                                       QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                       QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                   });
                    if (dataret != null)
                    {
                        output.InquiryLock = dataret.FirstOrDefault();
                    }
                    output.InquiryDetails = inquirylist;
                }
                if (inquiry.ContactId > 0)
                {
                    ContactInput imp = new ContactInput()
                    {
                        Id = inquiry.ContactId
                    };
                    var data = await GetNewContactForEdit(imp);
                    output.ContactEdit = data;
                }
            }

            return output;
        }

        //public async Task<GetInquirys> GetInquiryForEdit(NullableIdDto input)
        //{
        //    var output = new GetInquirys();
        //    var query = _inquiryRepository
        //       .GetAll().Where(p => p.Id == input.Id);
        //    var enqsource = (await _sourceRepository.GetAll().Where(p => p.TypeId == 1 && p.Id < 4).OrderBy(u => u.SourceName).Select(r => new Sourcelist { SourceId = r.Id, SourceName = r.SourceName, IsAssigned = false }).ToArrayAsync());
        //    var inquiry = (from a in query
        //                   join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
        //                   join company in _NewCompanyRepository.GetAll() on enqDetail.CompanyId equals company.Id
        //                   select new InquiryListDto
        //                   {
        //                       Id = a.Id,
        //                       MileStoneId = a.MileStoneId ?? 0,
        //                       MileStoneName = a.MileStones.MileStoneName ?? "",
        //                       Name = a.Name,
        //                       Address = a.Address,
        //                       WebSite = a.WebSite,
        //                       Email = a.Email,
        //                       MbNo = a.MbNo,
        //                       Remarks = a.Remarks,
        //                       SubMmissionId = a.SubMmissionId,
        //                       IpAddress = a.IpAddress,
        //                       Browcerinfo = a.Browcerinfo,
        //                       LandlineNumber = a.LandlineNumber,
        //                       Junk = a.Junk,
        //                       JunkDate = a.JunkDate,
        //                       StatusId = a.StatusId ?? 0,
        //                       StatusColorCode = a.EnqStatus.EnqStatusColor ?? "",
        //                       StatusName = a.EnqStatus.EnqStatusName ?? "",
        //                       Opportunity = (int)(a.EnqStatus.Percentage ?? 0),
        //                       WhyBafcoId = a.WhyBafcoId ?? 0,
        //                       OpportunitySourceId = a.OpportunitySourceId ?? 0,
        //                       WhyBafcoName = a.whyBafco.YbafcoName ?? "",
        //                       OpportunitySourceName = a.opportunitySource.Name ?? "",
        //                       LocationId = a.LocationId,
        //                       LocationName = a.Locations.LocationName ?? "",
        //                       IsClosed = a.IsClosed,
        //                       Archieved = a.Archieved,
        //                       CEmail = a.CEmail,
        //                       CLandlineNumber = a.CLandlineNumber,
        //                       CMbNo = a.CMbNo,
        //                       LeadStatusId = a.LeadStatusId ?? 0,
        //                       LeadStatusName = a.LeadStatuss.LeadStatusName ?? "",
        //                       CompanyId = enqDetail.CompanyId ?? 0,
        //                       DesignationId = enqDetail.DesignationId,
        //                       CompanyName = enqDetail.CompanyId > 0 ? enqDetail.Companys.Name : a.CompanyName,
        //                       DesignationName = enqDetail.DesignationId > 0 ? enqDetail.Designations.DesiginationName : a.DesignationName,
        //                       DepartmentId = enqDetail.DepartmentId ?? 0,
        //                       DepartmentName = enqDetail.DepartmentId > 0 ? enqDetail.Departments.DepatmentName : "",
        //                       AssignedbyId = enqDetail.AssignedbyId ?? 0,
        //                       AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
        //                       ContactId = enqDetail.ContactId,
        //                       ContactName = enqDetail.ContactId > 0 ? enqDetail.Contacts.Name : "",
        //                       LeadTypeId = enqDetail.LeadTypeId ?? 0,
        //                       LeadTypeName = enqDetail.LeadTypeId > 0 ? enqDetail.LeadTypes.LeadTypeName : "",
        //                       CompatitorsId = enqDetail.CompatitorsId ?? 0,
        //                       CompatitorName = enqDetail.CompatitorsId > 0 ? enqDetail.Compatitor.Name : "",
        //                       EstimationValue = enqDetail.EstimationValue,
        //                       Size = enqDetail.Size,
        //                       Summary = enqDetail.Summary,
        //                       TeamId = enqDetail.TeamId ?? 0,
        //                       TeamName = enqDetail.TeamId > 0 ? enqDetail.Team.Name : "",
        //                       Approved = enqDetail.CompanyId > 0 ? enqDetail.Companys.IsApproved : false,
        //                       ClosureDate = enqDetail.ClosureDate,
        //                       LastActivity = enqDetail.LastActivity,
        //                       IndustryId = company.IndustryId ?? 0,
        //                       IndustryName = company.IndustryId > 0 ? company.Industries.IndustryName : "",
        //                       DisableQuotation = a.DisableQuotation,
        //                       Won = a.Won,
        //                       Total = a.Total
        //                   }).FirstOrDefault();

        //    if (inquiry != null)
        //    {
        //        var enqsurce2 = (await _enquirySourceRepository.GetAll().Where(p => p.InquiryId == input.Id).OrderBy(u => u.Source.SourceName).Select(r => new Sourcelist { SourceId = r.SourceId, SourceName = r.Source.SourceName }).ToListAsync());
        //        foreach (var p in enqsource)
        //        {
        //            foreach (var s in enqsurce2)
        //            {
        //                if (p.SourceId == s.SourceId)
        //                {
        //                    p.IsAssigned = true;
        //                }
        //            }
        //        }


        //        if (inquiry.AssignedbyId > 0)
        //        {
        //            byte[] bytes = new byte[0];
        //            var Account = (from c in UserManager.Users where c.Id == (long)inquiry.AssignedbyId select c).FirstOrDefault();
        //            var profilePictureId = Account.ProfilePictureId;
        //            inquiry.AssignedbyImage = "/assets/common/images/default-profile-picture.png";
        //            if (profilePictureId != null)
        //            {
        //                var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
        //                if (file != null)
        //                {
        //                    bytes = file.Bytes;
        //                    GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
        //                    inquiry.AssignedbyImage = "data:image/jpeg;base64," + img.ProfilePicture;
        //                }
        //            }
        //        }

        //        output.Inquirys = inquiry;

        //        var leadDetail = _LeadDetailRepository.GetAll().Where(p => p.InquiryId == inquiry.Id);
        //        if (leadDetail.Count() > 0)
        //        {
        //            var inquirylist = (from a in leadDetail
        //                               select new LeadDetailListDto
        //                               {
        //                                   LeadSourceId = a.LeadSourceId ?? 0,
        //                                   LeadSourceName = a.LeadSourceId != null ? a.LeadSources.LeadSourceName : "",
        //                                   LeadTypeId = a.LeadTypeId ?? 0,
        //                                   LeadTypeName = a.LeadTypeId != null ? a.LeadTypes.LeadTypeName : "",
        //                                   SalesManagerId = a.SalesManagerId ?? 0,
        //                                   SalesManagerName = a.SalesManagerId != null ? a.SalesManagers.UserName : "",
        //                                   DesignerId = a.DesignerId ?? 0,
        //                                   DesignerName = a.DesignerId != null ? a.Designers.UserName : "",
        //                                   CoordinatorId = a.CoordinatorId ?? 0,
        //                                   CoordinatorName = a.CoordinatorId != null ? a.Coordinators.UserName : "",
        //                                   InquiryId = a.InquiryId,
        //                                   Id = a.Id,
        //                                   Size = a.Size,
        //                                   EstimationValue = a.EstimationValue
        //                               }).FirstOrDefault();

        //            if (inquirylist.CoordinatorId > 0)
        //            {
        //                byte[] bytes = new byte[0];
        //                var Account = (from c in UserManager.Users where c.Id == (long)inquirylist.CoordinatorId select c).FirstOrDefault();
        //                var profilePictureId = Account.ProfilePictureId;
        //                inquirylist.CoordinatorImage = "/assets/common/images/default-profile-picture.png";
        //                if (profilePictureId != null)
        //                {
        //                    var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
        //                    if (file != null)
        //                    {
        //                        bytes = file.Bytes;
        //                        GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
        //                        inquirylist.CoordinatorImage = "data:image/jpeg;base64," + img.ProfilePicture;
        //                    }
        //                }
        //            }
        //            if (inquirylist.DesignerId > 0)
        //            {
        //                byte[] bytes = new byte[0];
        //                var Account = (from c in UserManager.Users where c.Id == (long)inquirylist.DesignerId select c).FirstOrDefault();
        //                var profilePictureId = Account.ProfilePictureId;
        //                inquirylist.DesignerImage = "/assets/common/images/default-profile-picture.png";
        //                if (profilePictureId != null)
        //                {
        //                    var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
        //                    if (file != null)
        //                    {
        //                        bytes = file.Bytes;
        //                        GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
        //                        inquirylist.DesignerImage = "data:image/jpeg;base64," + img.ProfilePicture;
        //                    }
        //                }
        //            }
        //            output.InquiryDetails = inquirylist;
        //        }
        //        if (inquiry.ContactId > 0)
        //        {
        //            ContactInput imp = new ContactInput()
        //            {
        //                Id = inquiry.ContactId
        //            };
        //            var data = await GetNewContactForEdit(imp);
        //            output.ContactEdit = data;
        //        }


        //    }

        //    return output;
        //}
        public async Task<Array> GetNewContactForEdit(ContactInput input)
        {
            var Addinfo = _NewAddressInfoRepository.GetAll().Where(p => p.NewContacId == input.Id);

            var contactinfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == input.Id);

            var SubListout = new List<GetNewContacts>();

            var comp = _NewContactRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            if (comp != null)
            {
                SubListout.Add(new GetNewContacts
                {
                    Id = comp.Id,
                    Name = comp.Name,
                    NewCustomerTypeId = comp.NewCustomerTypeId,
                    CompanyId = comp.NewCompanyId != null ? comp.NewCompanyId : 0,
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
        public async Task<int> CreateOrUpdateInquiry(InquiryInputDto input)
        {
            int Id = 0;
            if (input.Id != 0)
            {
                await UpdateInquiryAsync(input);
                Id = input.Id;
            }
            else
            {
                Id = await CreateInquiryAsync(input);
            }

            return Id;
        }
        public virtual async Task <int> CreateInquiryAsync(InquiryInputDto input)
        {
            int data = 0;
            var date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            var checkDupicate = _inquiryRepository.GetAll().Where(p => p.Name == input.Name).FirstOrDefault();
            if (checkDupicate == null)
            {
                var query = input.MapTo<Inquiry>();
                var checkCount = _inquiryRepository.GetAll().Where(p => p.CreationTime.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) == date).Count() + 1;
                if(checkCount > 1)
                {
                    query.SubMmissionId = "L" + date + checkCount.ToString("0000");
                }
                else
                {
                    query.SubMmissionId = "L" + date + "0001";
                }
                try
                {
                    data = _inquiryRepository.InsertAndGetId(query);
                    if (input.CompanyId > 0 || input.DesignationId > 0 || input.ContactId > 0)
                    {
                        LinkedCompanyInput linkedinput = new LinkedCompanyInput()
                        {
                            Id = 0,
                            DesignationId = input.DesignationId,
                            CompanyId = input.CompanyId,
                            InquiryId = data,
                            ContactId = input.ContactId,
                            TeamId = input.TeamId
                        };
                        linkCopanyCreate(linkedinput);
                    }
                    if (input.SourceId.Length > 0)
                    {
                        foreach (var remarks in input.SourceId)
                        {
                            if (remarks != 0)
                            {
                                EnquirySource ec = new EnquirySource()
                                {
                                    InquiryId = data,
                                    SourceId = remarks
                                };
                                try
                                {
                                    await _enquirySourceRepository.InsertAsync(ec);

                                    //NullableIdDto input2 = new NullableIdDto()
                                    //{
                                    //    Id = data
                                    //};
                                    //await CreateInquiryContactInfo(input2);
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry Name ...");
            }
            return data;
        }
        public virtual async Task CreateSalesInquiry(InquiryInputDto input)
        {
            var date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            var checkDupicate = _inquiryRepository.GetAll().Where(p => p.Name == input.Name).FirstOrDefault();
            if (checkDupicate == null)
            {
                var query = input.MapTo<Inquiry>();
                var checkCount = _inquiryRepository.GetAll().Where(p => p.CreationTime.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) == date).Count() + 1;
                if (checkCount > 0)
                {
                    query.SubMmissionId = "L" + date + checkCount.ToString("0000");
                }
                else
                {
                    query.SubMmissionId = "L" + date + "0001";
                }
                try
                {
                    var data = _inquiryRepository.InsertAndGetId(query);
                    if (input.ContactId > 0)
                    {
                        CreateSalesLinkedContact(data, (int)input.ContactId);
                    }
                    if (input.CompanyId > 0 || input.DesignationId > 0 || input.ContactId > 0)
                    {
                        var linkcomp = new EnquiryDetail()
                        {
                            Id = 0,
                            DesignationId = input.DesignationId,
                            CompanyId = input.CompanyId,
                            InquiryId = data,
                            DepartmentId = input.DepartmentId,
                            ContactId = input.ContactId,
                            CompatitorsId = null,
                            EstimationValue = input.EstimationValue,
                            Size = input.Size,
                            Summary = input.Summary,
                            LeadTypeId = null,
                            TeamId = input.TeamId,
                            ClosureDate = input.ClosureDate,
                            LastActivity = input.LastActivity
                        };
                        if (input.AssignedbyId > 0)
                        {
                            linkcomp.AssignedbyId = input.AssignedbyId;
                            linkcomp.AssignedbyDate = DateTime.Now;
                        }
                        await _enquiryDetailRepository.InsertAsync(linkcomp);
                    }
                    if (input.SourceId.Length > 0)
                    {
                        foreach (var remarks in input.SourceId)
                        {
                            if (remarks != 0)
                            {
                                EnquirySource ec = new EnquirySource();
                                ec.InquiryId = data;
                                ec.SourceId = remarks;
                                try
                                {
                                    await _enquirySourceRepository.InsertAsync(ec);
                                    NullableIdDto input2 = new NullableIdDto()
                                    {
                                        Id = data
                                    };
                                    await CreateInquiryContactInfo(input2);
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry Name ...");
            }
        }
        public virtual async Task<int> CreateSalesInquiryInformation(InquiryInputDto input)
        {

            var data = 0;
            var date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            var checkDupicate = _inquiryRepository.GetAll().Where(p => p.Name == input.Name).FirstOrDefault();
            if (checkDupicate == null)
            {
                var query = input.MapTo<Inquiry>();
                var checkCount = _inquiryRepository.GetAll().Where(p => p.CreationTime.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) == date).Count() + 1;
                if (checkCount > 0)
                {
                    query.SubMmissionId = "L" + date + checkCount.ToString("0000");
                }
                else
                {
                    query.SubMmissionId = "L" + date + "0001";
                }
                try
                {
                    data = _inquiryRepository.InsertAndGetId(query);

                    if (input.ContactId > 0)
                    {
                        CreateSalesLinkedContact(data, (int)input.ContactId);
                    }
                    if (input.CompanyId > 0 || input.DesignationId > 0 || input.ContactId > 0)
                    {
                        var linkcomp = new EnquiryDetail()
                        {
                            Id = 0,
                            DesignationId = input.DesignationId,
                            CompanyId = input.CompanyId,
                            InquiryId = data,
                            DepartmentId = input.DepartmentId,
                            ContactId = input.ContactId,
                            CompatitorsId = null,
                            EstimationValue = input.EstimationValue,
                            Size = input.Size,
                            Summary = input.Summary,
                            LeadTypeId = null,
                            TeamId = input.TeamId,
                            ClosureDate = input.ClosureDate,
                            LastActivity = input.LastActivity
                        };
                        if (input.AssignedbyId > 0)
                        {
                            linkcomp.AssignedbyId = input.AssignedbyId;
                            linkcomp.AssignedbyDate = DateTime.Now;
                        }
                        await _enquiryDetailRepository.InsertAsync(linkcomp);
                    }
                    if (input.SourceId.Length > 0)
                    {
                        foreach (var remarks in input.SourceId)
                        {
                            if (remarks != 0)
                            {
                                EnquirySource ec = new EnquirySource();
                                ec.InquiryId = data;
                                ec.SourceId = remarks;
                                try
                                {
                                    await _enquirySourceRepository.InsertAsync(ec);

                                }
                                catch (Exception ex)
                                {

                                }
                            }

                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry Name ...");
            }

            return data;
        }
        void CreateSalesLinkedContact(int Enqid,int ContactId)
        {
            EnquiryContact input = new EnquiryContact()
            {
                ContactId = ContactId,
                InquiryId = Enqid
            };
            var EnqContact = input.MapTo<EnquiryContact>();
            _enquiryContactRepository.InsertAsync(EnqContact);
        }
        public void linkCopanyCreate(LinkedCompanyInput input)
        {
            var linkcomp = input.MapTo<EnquiryDetail>();
             _enquiryDetailRepository.InsertAsync(linkcomp);
        }
        public void linkCopanyUpdate(LinkedCompanyInput input)
        {
            var linkcomp = input.MapTo<EnquiryDetail>();
            _enquiryDetailRepository.UpdateAsync(linkcomp);
        }
        public virtual async Task UpdateInquiryAsync(InquiryInputDto input)
        {
            var checkDupicate = _inquiryRepository.GetAll().Where(p => p.Name == input.Name && p.Id != input.Id).FirstOrDefault();
            if (checkDupicate == null)
            {
                try
                {
                    var query = await _inquiryRepository.GetAsync(input.Id);
                    ObjectMapper.Map(input, query);

                    var idser = _enquirySourceRepository.GetAll().Where(p => p.InquiryId == input.Id).ToList();
                    foreach (var id in idser)
                    {
                        _enquirySourceRepository.Delete(id.Id);
                    }
                    if (input.SourceId.Length > 0)
                    {
                        foreach (var data in input.SourceId)
                        {
                            if (data != 0)
                            {
                                EnquirySource ec = new EnquirySource()
                                {
                                    InquiryId = input.Id,
                                    SourceId = data//2
                                };
                                await _enquirySourceRepository.InsertAsync(ec);
                            }

                        }
                    }
                    await _inquiryRepository.UpdateAsync(query);

                    var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.Id).FirstOrDefault();

                    if (input.CompanyId > 0 || input.DesignationId > 0 || input.ContactId > 0)
                    {
                        if (enquiryDetail != null)
                        {
                            var linkcomp = enquiryDetail.MapTo<EnquiryDetail>();
                            linkcomp.CompanyId = input.CompanyId;
                            linkcomp.DesignationId = input.DesignationId;
                            linkcomp.DepartmentId = input.DepartmentId;
                            linkcomp.ContactId = input.ContactId;
                            linkcomp.CompatitorsId = input.CompatitorsId;
                            linkcomp.EstimationValue = input.EstimationValue;
                            linkcomp.Size = input.Size;
                            linkcomp.Summary = input.Summary;
                            linkcomp.LeadTypeId = input.LeadTypeId;
                            linkcomp.TeamId = input.TeamId;
                            linkcomp.ClosureDate = input.ClosureDate;
                            linkcomp.LastActivity = input.LastActivity;
                            if (input.AssignedbyId > 0)
                            {
                                linkcomp.AssignedbyId = input.AssignedbyId;
                                linkcomp.AssignedbyDate = DateTime.Now;
                            }
                            await _enquiryDetailRepository.UpdateAsync(linkcomp);

                            if (input.AssignedbyId > 0)
                            {
                                var AccountManager = _NewCompanyRepository.GetAll().Where(p => p.Id == input.CompanyId).FirstOrDefault();
                                if (AccountManager.AccountManagerId == null)
                                {
                                    AccountManager.AccountManagerId = input.AssignedbyId;

                                    await _NewCompanyRepository.UpdateAsync(AccountManager);
                                }

                            }
                        }
                        else
                        {
                            LinkedCompanyInput linkedinput = new LinkedCompanyInput();
                            linkedinput.Id = 0;
                            linkedinput.DesignationId = input.DesignationId;
                            linkedinput.CompanyId = input.CompanyId;
                            linkedinput.ContactId = input.ContactId;
                            if (input.AssignedbyId > 0)
                            {
                                linkedinput.AssignedbyId = input.AssignedbyId;
                                linkedinput.AssignedbyDate = DateTime.Now;
                            }
                            linkedinput.InquiryId = input.Id;
                            linkCopanyCreate(linkedinput);
                        }

                    }
                    NullableIdDto input2 = new NullableIdDto()
                    {
                        Id = input.Id
                    };
                    await CreateInquiryContactInfo(input2);
                    await CreateInquiryCompanyInfo(input2);

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Inquiry Name ...");
            }

        }
        public async Task CreateOrUpdateLeadDetails(LeadDetailInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateInquiryDetailAsync(input);
            }
            else
            {
                await CreateInquiryDetailAsync(input);
            }
        }
        public virtual async Task CreateInquiryDetailAsync(LeadDetailInputDto input)
        {
            var inq = _inquiryRepository.GetAll().Where(p => p.Id == input.InquiryId).FirstOrDefault();
            var query = input.MapTo<LeadDetail>();


            if (input.DesignerId != null)
            {
                JobActivity actinput = new JobActivity()
                {
                    InquiryId = input.InquiryId,
                    DesignerId = input.DesignerId,
                    Title = "Job Activity",
                    JobNumber = inq.SubMmissionId + "-D1",
                    Remark = " Auto Job Creation (Designer alloted to the Enquiry)",
                    AllottedDate = DateTime.Now,
                    Isopen = true,
                };
                var Jobactivity = actinput.MapTo<JobActivity>();
                await _jobActivityRepository.InsertAsync(Jobactivity);
            }
            await _LeadDetailRepository.InsertAsync(query);


        }
        public virtual async Task UpdateInquiryDetailAsync(LeadDetailInputDto input)
        {
            var inq = _inquiryRepository.GetAll().Where(p => p.Id == input.InquiryId).FirstOrDefault();
            var query = await _LeadDetailRepository.GetAsync(input.Id);


            var designer = (from r in _LeadDetailRepository.GetAll().Where(p => p.InquiryId == input.InquiryId && p.Id == input.Id) select r).FirstOrDefault();
            if (designer.DesignerId == null && input.DesignerId != null)
            {
                JobActivity actinput = new JobActivity()
                {
                    InquiryId = input.InquiryId,
                    DesignerId = input.DesignerId,
                    Title = "Job Activity",
                    JobNumber = inq.SubMmissionId + "-D1",
                    Remark = " Auto Job Creation (Designer alloted to the Enquiry)",
                    AllottedDate = DateTime.Now,
                    Isopen = true,
                };
                var Jobactivity = actinput.MapTo<JobActivity>();
                await _jobActivityRepository.InsertAsync(Jobactivity);
            }
            if (designer.DesignerId != null && input.DesignerId != null && input.DesignerId != designer.DesignerId)
            {
                var OldJobActivity = (from r in _jobActivityRepository.GetAll() where r.DesignerId == designer.DesignerId && r.InquiryId == designer.InquiryId select r).ToList();
                foreach (var data in OldJobActivity)
                {
                    data.DesignerId = input.DesignerId;
                    await _jobActivityRepository.UpdateAsync(data);
                }
            }

            ObjectMapper.Map(input, query);
            await _LeadDetailRepository.UpdateAsync(query);
        }
        public async Task<Array> GetInquiryTickets(GetTicketInput input)
        {
            var SupportMileStones = (from r in _milestoneRepository.GetAll() where r.Id < 5 select r).ToArray();
            var query = _inquiryRepository.GetAll().Where(p => p.MileStoneId < 5 && p.Junk == null && p.IsClosed != true)
                    .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p =>
                     p.Name.Contains(input.Filter) ||
                     p.SubMmissionId.Contains(input.Filter) ||
                     p.Companys.Name.Contains(input.Filter) ||
                     p.CompanyName.Contains(input.Filter)
                );

            var NewStatuss = (from a in query
                              join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                              join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                              from ur in urJoined.DefaultIfEmpty()
                              select new InquiryListDto
                              {
                                  Id = a.Id,
                                  MileStoneId = a.MileStoneId,
                                  MileStoneName = a.MileStones.MileStoneName,
                                  Name = a.Name,
                                  Address = a.Address,
                                  WebSite = a.WebSite,
                                  Email = a.Email,
                                  MbNo = a.MbNo,
                                  Remarks = a.Remarks,
                                  SubMmissionId = a.SubMmissionId,
                                  IpAddress = a.IpAddress,
                                  Browcerinfo = a.Browcerinfo,
                                  CreatorUserId = a.LastModifierUserId != null ? (int)a.LastModifierUserId : (a.CreatorUserId ?? 0),
                                  CreationTime = a.CreationTime,
                                  StatusId = a.StatusId ?? 0,
                                  StatusColorCode = a.EnqStatus.EnqStatusColor,
                                  StatusName = a.EnqStatus.EnqStatusName ?? "",
                                  Percentage = (int)(a.EnqStatus.Percentage ?? 0),
                                  CompanyId = enqDetail.CompanyId,
                                  DesignationId = enqDetail.DesignationId,
                                  CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                                  DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                                  DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                  DepartmentId = enqDetail.DepartmentId ?? 0,
                                  AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                  AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                  ContactId = enqDetail.ContactId ?? 0,
                                  TeamId = enqDetail.TeamId ?? 0,
                                  TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                                  SalesMan = enqDetail.AbpAccountManager.UserName ?? "",
                                  EstimationValueformat = ((int)enqDetail.EstimationValue).ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                                  ClosureDate = enqDetail.ClosureDate,
                                  LastActivity = enqDetail.LastActivity,
                                  IsExpire = false,
                                  EstimationValue = (int)enqDetail.EstimationValue,
                                  ProfilePicture = "",
                                  UserName =  ur != null ? ur.UserName : ""
                              }).ToList();


            foreach (var d in NewStatuss)
            {
                try
                {

                    var date = DateTime.Now.AddDays(7);
                    if (date >= d.ClosureDate)
                    {
                        d.IsExpire = true;
                    }

                    var acttype = (from a in _acitivityTrackRepository.GetAll()
                                   where (a.EnquiryId == d.Id)
                                   group a by new { a.ActivityId, a.Activity.ActivityName, a.Activity.ColorCode }
                into b
                                   select new ActivityColor { ActivityId = b.Key.ActivityId, ActivityName = b.Key.ActivityName, ActivityColors = b.Key.ColorCode }).ToArray();
                    foreach (var de in acttype)
                    {
                        de.ActivityCount = (from a in _acitivityTrackRepository.GetAll() where (a.EnquiryId == d.Id && a.ActivityId == de.ActivityId) select a.Id).Count().ToString();
                    }
                    d.ActivityColors = acttype;
                }
                catch (Exception ex)
                {

                }
            }



            var NewStatusdtos = NewStatuss.MapTo<List<InquiryListDto>>();

            var SubList = new List<InquiryListDto>();

            var SubListout = new List<TickectRegistrationOutput>();


            foreach (var newsts in SupportMileStones)
            {
                var TotValue = (from r in NewStatuss where r.MileStoneId == newsts.Id select r).Sum(p => p.EstimationValue);
                SubListout.Add(new TickectRegistrationOutput
                {
                    Name = newsts.MileStoneName,
                    StatusId = newsts.Id,
                    Total = TotValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                    GetTicketReservation = (from r in NewStatuss where r.MileStoneId == newsts.Id select r).OrderByDescending(p => p.CreationTime).ToArray()
                });

            }
            return SubListout.ToArray();
        }
        private async Task<GetProfilePictureOutput> GetProfilePictureByIdInternal(Guid profilePictureId)
        {
            var bytes = await GetProfilePictureByIdOrNull(profilePictureId);
            if (bytes == null)
            {
                return new GetProfilePictureOutput(string.Empty);
            }

            return new GetProfilePictureOutput(Convert.ToBase64String(bytes));
        }
        private async Task<byte[]> GetProfilePictureByIdOrNull(Guid profilePictureId)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(profilePictureId);
            if (file == null)
            {
                return null;
            }

            return file.Bytes;
        }
        public async Task<Array> GetSalesInquiryTickets(GetTicketInput input)
        {
            string viewquery = "SELECT * FROM [dbo].[View_Inquiry]";
            long userid = (int)AbpSession.UserId;
            if (input.SalesId > 0)
            {
                userid = input.SalesId;
            }
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();
            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);


            if (userrole.DisplayName == "Sales Executive")
            {
                viewquery = "SELECT * FROM [dbo].[View_Inquiry]  where AssignedbyId=" + userid.ToString();
            }
            else if (userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                if (input.SalesId > 0)
                    viewquery = "SELECT * FROM [dbo].[View_Inquiry]  where AssignedbyId = " + userid.ToString();
                else
                    viewquery = "SELECT * FROM [dbo].[View_Inquiry]  where SalesManagerId=" + userid.ToString();
            }
            else if (userrole.DisplayName == "Sales Manager")
            {
                viewquery = "SELECT * FROM [dbo].[View_Inquiry]  where SalesManagerId=" + userid.ToString();
            }
            else if (userrole.DisplayName == "Designer")
            {
                viewquery = "SELECT * FROM [dbo].[View_Inquiry] where DesignerId=" + userid.ToString();
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                viewquery = "SELECT * FROM [dbo].[View_Inquiry] where CoordinatorId=" + userid.ToString();
            }
            else
            {
                viewquery = "SELECT * FROM [dbo].[View_Inquiry]";
            }

            var SupportMileStones = (from r in _milestoneRepository.GetAll() where r.Id > 3 && r.IsQuotation == false select r).ToArray();
            var QSupportMileStones = (from r in _milestoneRepository.GetAll() where r.Id > 3 && r.IsQuotation == true select r).ToArray();
            var SubListout = new List<TickectRegistrationOutput>();

            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con3 = new SqlConnection(db.ConnectionString());
                con3.Open();
                SqlCommand cmd3 = new SqlCommand(viewquery, con3);
                DataTable dt3 = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                {
                    sda.Fill(dt);
                }
            }
            catch (Exception ex)
            {

            }
            //int ccc = dt.Rows.Count;
            var NormalTicket = (from DataRow dr in dt.Rows
                                select new InquiryListDto
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    QuotationId = Convert.ToInt32(dr["QId"]),
                                    MileStoneId = Convert.ToInt32(dr["MileStoneId"]),
                                    MileStoneName = Convert.ToString(dr["MileStoneName"]),
                                    IsQuotation = Convert.ToBoolean(dr["IsQuotation"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Address = Convert.ToString(dr["LocationName"]),
                                    SubMmissionId = Convert.ToString(dr["SubMmissionId"]),
                                    CreationTime = Convert.ToDateTime(dr["CreationTime"]),
                                    StatusId = (int)dr["StatusId"],
                                    StatusColorCode = Convert.ToString(dr["EnqStatusColor"]),
                                    StatusName = Convert.ToString(dr["EnqStatusName"]),
                                    Percentage = Convert.ToInt32(dr["Percentage"]),
                                    QuotationCount = Convert.ToInt32(dr["NewCount"]),
                                    Total = Convert.ToInt32(dr["Total"]),
                                    IsEditable = true,
                                    CompanyId = Convert.ToInt32(dr["CompanyId"]),
                                    DesignationId = Convert.ToInt32(dr["NewCustomerTypeId"]),
                                    CompanyName = Convert.ToString(dr["CompanyName"]),
                                    DepartmentName = Convert.ToString(dr["DepatmentName"]),
                                    DepartmentId = Convert.ToInt32(dr["DepartmentId"]),
                                    AssignedbyId = Convert.ToInt32(dr["AssignedbyId"]),
                                    TeamId = Convert.ToInt32(dr["TeamId"]),
                                    TeamName = Convert.ToString(dr["TeamName"]),
                                    SalesMan = Convert.ToString(dr["Salesperson"]),
                                    DesignerName = Convert.ToString(dr["Designer"]),
                                    EstimationValueformat = Convert.ToString(dr["valueFormat"]),
                                    EstimationValue = Convert.ToDecimal(dr["EstimationValue"]),
                                    AssignedbyImage = Convert.ToString(dr["SalesUrl"]),
                                    DesignerImage = Convert.ToString(dr["DesignerUrl"]),
                                    CoordinatorId = Convert.ToInt32(dr["CoordinatorId"]),
                                    CoordinatorName = Convert.ToString(dr["CoordinatorName"]),
                                    CoordinatorImage = Convert.ToString(dr["CoordinatorUrl"]),
                                    QuotationsNew = Convert.ToString(dr["QuotationsNew"]),
                                    IsExpire = Convert.ToBoolean(dr["IsClosed"]),
                                    IsLast = Convert.ToBoolean(dr["IsLastActivity"]),
                                    IsOptional= Convert.ToBoolean(dr["Optional"]),
                                    Browcerinfo = Convert.ToString(dr["DateString"]),
                                    ContactId = Convert.ToInt32(dr["ContactId"])

                                });

            var NewStatuss = NormalTicket;

            NewStatuss = NewStatuss.WhereIf(
                  !input.Filter.IsNullOrEmpty(),
                  p =>
                       p.Name.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                       p.SubMmissionId.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                       p.CompanyName.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty)) ||
                       p.QuotationsNew.ToLower().Replace(" ", string.Empty).Contains(input.Filter.ToLower().Replace(" ", string.Empty))
                  );

            var NewStatusdtos = NewStatuss.MapTo<List<InquiryListDto>>();

            var SubList = new List<InquiryListDto>();



            var Ticketdata = new List<QuotationTicketOutput>();



            foreach (var newsts in SupportMileStones)
            {

                var TotValue = (from r in NewStatuss where r.MileStoneId == newsts.Id select r).Sum(p => p.EstimationValue);

                SubListout.Add(new TickectRegistrationOutput
                {
                    Name = newsts.MileStoneName,
                    StatusId = newsts.Id,
                    Total = TotValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                    IsQuotation = false,
                    Class = "enquiryclass",
                    GetTicketReservation = (from r in NewStatuss where r.MileStoneId == newsts.Id && r.IsQuotation == false select r).OrderByDescending(p => p.CreationTime).ToArray()
                });


            }


            foreach (var Qnewsts in QSupportMileStones)
            {
                var TotValue = (from r in NewStatuss where r.MileStoneId == Qnewsts.Id select r).Sum(p => p.EstimationValue);
                SubListout.Add(new TickectRegistrationOutput
                {
                    Name = Qnewsts.MileStoneName,
                    Total = TotValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                    IsQuotation = true,
                    StatusId = Qnewsts.Id,
                    Class = "quotationclass",
                    GetTicketReservation = (from r in NewStatuss where r.MileStoneId == Qnewsts.Id && r.IsQuotation == true select r).OrderByDescending(p => p.CreationTime).ToArray()
                });
            }


            return SubListout.ToArray();
        }
        public async Task<PagedResultDto<QuotationListDto>> GetSalesQuotations(GetQuotationInput input)
        {

            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _quotationRepository.GetAll().Where(p => p.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from q in _quotationRepository.GetAll()
                         where q.Revised == false && q.SalesPersonId == userid && q.MileStoneId > 3 && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where q.Revised == false && team.SalesManagerId == userid && q.MileStoneId > 3  && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.DesignerId == userid && q.MileStoneId > 3  && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.CoordinatorId == userid && q.MileStoneId > 3  && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else
            {
                query = _quotationRepository.GetAll().Where(p => p.MileStoneId > 3  && p.Revised == false && p.IsClosed != true && p.Archieved != true && p.Void != true);

            }
            var inquiry = (from r in query
                           join d in _LeadDetailRepository.GetAll() on r.InquiryId equals d.InquiryId
                           select new QuotationListDto
                           {
                               Id = r.Id,
                               RefNo = r.RefNo,
                               Name = r.Name,
                               CustomerId = r.CustomerId,
                               TermsandCondition = r.TermsandCondition,
                               NewCompanyId = r.NewCompanyId,
                               CompanyName = r.NewCompanyId != null ? r.NewCompanys.Name : "",
                               StatusName = r.Quotationstatus.Name,
                               QuotationStatusId = r.QuotationStatusId,
                               SalesPersonId = r.SalesPersonId,
                               SalesPersonName = r.SalesPersonId != null ? r.SalesPerson.UserName : "",
                               CreationTime = r.CreationTime,
                               SCreationTime = r.CreationTime.ToString(),
                               Email = r.Email,
                               MobileNumber = r.MobileNumber,
                               AttentionName = r.AttentionContactId != null ? (r.AttentionContact.Name + " " + r.AttentionContact.LastName) : "",
                               LostDate = r.LostDate,
                               WonDate = r.WonDate,
                               SubmittedDate = r.SubmittedDate,
                               Lost = r.Lost,
                               Won = r.Won,
                               Submitted = r.Submitted,
                               InquiryId = r.InquiryId,
                               InquiryName = r.InquiryId != null ? r.Inquiry.Name : "",
                               DesignerName = d.DesignerId != null ? d.Designers.Name : "",
                               Optional = r.Optional

                           });
            inquiry = inquiry.WhereIf(
                          !input.Filter.IsNullOrWhiteSpace(),
                          u =>
                             u.RefNo.Contains(input.Filter) ||
                             u.InquiryName.Contains(input.Filter) ||
                             u.CompanyName.Contains(input.Filter) ||
                             u.AttentionName.Contains(input.Filter) ||
                             u.SalesPersonName.Contains(input.Filter) ||
                             u.StatusName.Contains(input.Filter)
                             );

            var Count = await inquiry.CountAsync();

            var list = await inquiry
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();

            var ListDtos = list.MapTo<List<QuotationListDto>>();


            return new PagedResultDto<QuotationListDto>(Count, ListDtos);
        }
        public async Task<ListResultDto<EnqActList>> GetOverAllEnquiryActivitys(GetEnqActOverAllInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var actvity = _acitivityTrackRepository.GetAll().Where(r => r.Id == 0);


            if (userrole.DisplayName == "Sales Executive")
            {
                actvity = (from act in _acitivityTrackRepository.GetAll()
                           join enqDetail in _enquiryDetailRepository.GetAll() on act.EnquiryId equals enqDetail.InquiryId
                           join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                           where enqDetail.AssignedbyId == userid
                           select act
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                if (input.SalesmanId > 0)
                {
                    actvity = (from act in _acitivityTrackRepository.GetAll()
                               join enqDetail in _enquiryDetailRepository.GetAll() on act.EnquiryId equals enqDetail.InquiryId
                               join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                               join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                               where team.SalesManagerId == userid && enqDetail.AssignedbyId == input.SalesmanId
                               select act);
                }
                else
                {
                    actvity = (from act in _acitivityTrackRepository.GetAll()
                               join enqDetail in _enquiryDetailRepository.GetAll() on act.EnquiryId equals enqDetail.InquiryId
                               join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                               join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                               where team.SalesManagerId == userid
                               select act);
                }
            }


            else
            {
                if (input.SalesmanId > 0)
                {
                    actvity = (from act in _acitivityTrackRepository.GetAll()
                               join enqDetail in _enquiryDetailRepository.GetAll() on act.EnquiryId equals enqDetail.InquiryId
                               join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                               where enqDetail.AssignedbyId == input.SalesmanId
                               select act
                          );
                }
                else
                {
                    actvity = _acitivityTrackRepository.GetAll();
                }
            }


            actvity = actvity
                .WhereIf(
                   !input.Filter.IsNullOrWhiteSpace(),
                   u =>
                       u.Message.Contains(input.Filter) ||
                       u.Title.Contains(input.Filter));

            var EnquiryList = (from c in actvity
                               join r in _inquiryRepository.GetAll() on c.EnquiryId equals r.Id
                               select new EnqActList
                               {
                                   Id = c.Id,
                                   EnquiryId = c.EnquiryId,
                                   ActivityId = c.ActivityId,
                                   EnquiryNo = r.SubMmissionId ?? "",
                                   MileStoneId = r.MileStoneId ?? 0,
                                   ActivityName = c.Activity.ActivityName ?? "",
                                   ContactId = c.ContactId ?? 0,
                                   ContactName = c.ContactId != null ? c.NewContacts.Name : "",
                                   Title = c.Title,
                                   Message = c.Message,
                                   CreationTime = c.CreationTime,
                                   CreatedId = c.CreatorUserId,
                                   FrontMessage = c.Message,
                                   PreviousStatus = c.PreviousStatus,
                                   CurrentStatus = c.CurrentStatus,
                                   ColorName = c.Activity.ColorCode
                               }).OrderByDescending(p => p.CreationTime).ToList();

            foreach (var li in EnquiryList)
            {

                var actvitydet = _activityTrackCommentsRepository
                .GetAll().Where(p => p.ActivityTrackId == li.Id).OrderByDescending(p => p.Id).FirstOrDefault();


                switch (li.ActivityId)
                {
                    case 1:
                        li.ClassName = "fa-phone";
                        break;
                    case 2:
                        li.ClassName = "fa-volume-control-phone";
                        break;
                    case 3:
                        li.ClassName = "fa-eye";
                        break;
                    case 4:
                        li.ClassName = "fa-home";
                        break;
                    case 5:
                        li.ClassName = "fa-handshake-o";
                        break;
                    case 6:
                        li.ClassName = "fa-check";
                        break;
                    case 7:
                        li.ClassName = "fa-hand-o-right";
                        break;
                }
                if (actvitydet != null)
                {
                    li.FrontMessage = actvitydet.Message;
                }
                else
                {
                    li.FrontMessage = li.Message;
                }

                if (li.CreatedId != null)
                {
                    long creatorid = (long)li.CreatedId;
                    var user = await UserManager.GetUserByIdAsync(creatorid);
                    GetProfilePictureOutput dp = new GetProfilePictureOutput("");
                    if (user != null)
                    {
                        if (user.ProfilePictureId != null)
                        {
                            // dp = await GetProfilePictureByIdInternal(user.ProfilePictureId.Value);
                            li.ProfilePicture = "";
                        }

                        li.Createdby = user.UserName;
                    }

                }

            }
            return new ListResultDto<EnqActList>(EnquiryList.MapTo<List<EnqActList>>());
        }
        public async Task<ListResultDto<EnqActList>> GetEnquiryActivitys(GetEnqActInput input)
        {
            var actvity = _acitivityTrackRepository
                .GetAll().Where(p => p.EnquiryId == input.EnqId);
            var query = actvity
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Message.Contains(input.Filter) ||
                        u.Title.Contains(input.Filter));

            var EnquiryList = (from c in query
                               join r in _inquiryRepository.GetAll() on c.EnquiryId equals r.Id
                               select new EnqActList
                               {
                                   Id = c.Id,
                                   EnquiryId = c.EnquiryId,
                                   ActivityId = c.ActivityId,
                                   EnquiryNo = r.SubMmissionId ?? "",
                                   ActivityName = c.Activity.ActivityName ?? "",
                                   ContactId = c.ContactId ?? 0,
                                   ContactName = c.ContactId != null ? c.NewContacts.Name : "",
                                   Title = c.Title,
                                   Message = c.Message,
                                   CreationTime = c.CreationTime,
                                   CreatedId = c.CreatorUserId,
                                   PreviousStatus = c.PreviousStatus,
                                   CurrentStatus = c.CurrentStatus,
                                   ColorName = c.Activity.ColorCode
                               }).OrderByDescending(p => p.CreationTime).ToList();

            foreach (var li in EnquiryList)
            {

                switch (li.ActivityId)
                {
                    case 1:
                        li.ClassName = "fa-phone";
                        break;
                    case 2:
                        li.ClassName = "fa-volume-control-phone";
                        break;
                    case 3:
                        li.ClassName = "fa-eye";
                        break;
                    case 4:
                        li.ClassName = "fa-home";
                        break;
                    case 5:
                        li.ClassName = "fa-handshake-o";
                        break;
                    case 6:
                        li.ClassName = "fa-check";
                        break;
                    case 7:
                        li.ClassName = "fa-hand-o-right";
                        break;
                }


                if (li.CreatedId != null)
                {
                    long creatorid = (long)li.CreatedId;
                    var user = await UserManager.GetUserByIdAsync(creatorid);
                    GetProfilePictureOutput dp = new GetProfilePictureOutput("");
                    if (user != null)
                    {
                        if (user.ProfilePictureId != null)
                        {
                           // dp = await GetProfilePictureByIdInternal(user.ProfilePictureId.Value);
                            li.ProfilePicture = "";
                        }

                        li.Createdby = user.UserName;
                    }
                }

            }

            return new ListResultDto<EnqActList>(EnquiryList.MapTo<List<EnqActList>>());
        }
        public async Task CreateOrUpdateEnquiryActivitys(EnqActCreate input)
        {
            if (input.Id != 0)
            {
                await UpdateEnquiryActivitysAsync(input);
            }
            else
            {
                await CreateEnquiryActivitysAsync(input);
            }
        }
        public virtual async Task CreateEnquiryActivitysAsync(EnqActCreate input)
        {

            var Activity = input.MapTo<AcitivityTrack>();
            await _acitivityTrackRepository.InsertAsync(Activity);

        }
        public virtual async Task UpdateEnquiryActivitysAsync(EnqActCreate input)
        {
            try
            {
                var Activity = await _acitivityTrackRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, Activity);
                await _acitivityTrackRepository.UpdateAsync(Activity);
            }

            catch (Exception ex)
            {

            }
        }
        public async Task GetDeleteEnquiryActivity(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DeleteAcitivityTrack", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 19);
                sqlComm.Parameters.AddWithValue("@Id", input.Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public async Task<ListResultDto<EnqActCommentList>> GetEnqActComment(GetEnqActCommentInput input)
        {
            long userid = (long)AbpSession.UserId;
            var actvity = _activityTrackCommentsRepository
                .GetAll().Where(p => p.ActivityTrackId == input.ActId);
            var query = actvity
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Message.Contains(input.Filter))
                .ToList();

            var EnquiryList = (from c in query
                               select new EnqActCommentList
                               {
                                   Id = c.Id,
                                   ActivityTrackId = c.ActivityTrackId,
                                   Message = c.Message,
                                   CreationTime = c.CreationTime,
                                   CreatedId = c.CreatorUserId,
                               }).ToList();
            foreach (var li in EnquiryList)
            {
                if (li.CreatedId != null)
                {
                    long creatorid = (long)li.CreatedId;
                    var user = await UserManager.GetUserByIdAsync(creatorid);
                    GetProfilePictureOutput dp = new GetProfilePictureOutput("");
                    if (user != null)
                    {
                        if (user.ProfilePictureId != null)
                        {
                            dp = await GetProfilePictureByIdInternal(user.ProfilePictureId.Value);
                            li.ProfilePicture = dp.ProfilePicture;
                        }

                        li.Createdby = user.UserName;
                        li.SessionId = userid;
                    }
                }

            }

            return new ListResultDto<EnqActCommentList>(EnquiryList.MapTo<List<EnqActCommentList>>());

        }
        public async Task CreateOrUpdateEnquiryActivitysComment(EnqActCommentCreate input)
        {
            if (input.Id != 0)
            {
                await UpdateEnquiryActivitysCommentAsync(input);
            }
            else
            {
                await CreateEnquiryActivitysCommentAsync(input);
            }
        }
        public virtual async Task CreateEnquiryActivitysCommentAsync(EnqActCommentCreate input)
        {

            var message = input.MapTo<ActivityTrackComment>();

            await _activityTrackCommentsRepository.InsertAsync(message);

        }
        public virtual async Task UpdateEnquiryActivitysCommentAsync(EnqActCommentCreate input)
        {

            var message = await _activityTrackCommentsRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, message);
            await _activityTrackCommentsRepository.UpdateAsync(message);
        }
        public async Task<GetEActivity> GetActivityForEdit(NullableIdDto input)
        {
            var output = new GetEActivity();
            var query = _acitivityTrackRepository
               .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() > 0)
            {
                var ActivityDto = (from c in query
                                   select new EnqActList
                                   {
                                       EnquiryId = c.EnquiryId != null ? c.EnquiryId : 0,
                                       EnquiryNo = c.Enquiry.SubMmissionId != null ? c.Enquiry.SubMmissionId : "",
                                       ContactId = c.ContactId,
                                       ContactName = c.NewContacts.Name,
                                       Title = c.Title,
                                       Message = c.Message,
                                       ActivityName = c.Activity.ActivityName,
                                       ActivityId = c.ActivityId,

                                   }).FirstOrDefault();


                output = new GetEActivity
                {
                    Activities = ActivityDto
                };
            }
            return output;

        }
        public int NewLocationCreate(LocationInputDto input)
        {
            int id = 0;
            var city = _CityRepository.GetAll().Where(p => p.CountryId == input.CityId).FirstOrDefault();
            LocationInputDto data = new LocationInputDto();
            data.LocationCode = input.LocationCode;
            data.LocationName = input.LocationName;
            data.CityId = city.Id;

            var location = data.MapTo<Location>();
            id = _locationRepository.InsertAndGetId(location);
            return id;
        }
        public int NewDesignationCreate(DesignationInputDto input)
        {
            int id = 0;
            DesignationInputDto data = new DesignationInputDto();
            data.DesiginationName = input.DesiginationName;
            data.DesignationCode = input.DesignationCode;
            var desg = data.MapTo<Designation>();
            id =  _DesignationRepository.InsertAndGetId(desg);
            return id;

        }
        public int NewCompanyCreate(CompanyCreateInput input)
        {
            var id = 0;
            var data = _NewCompanyRepository.GetAll().Where(p => p.Name == input.CompanyName && p.NewCustomerTypeId == 1).ToList();
            var discount = _DiscountRepository.GetAll().FirstOrDefault();    // 03.01.18
            if (data.Count == 0)
            {
                if(input.InSales == true)
                {
                    CreateCompany company = new CreateCompany()
                    {
                        Name = input.CompanyName,
                        NewCustomerTypeId = 1,
                        ApprovedById = null,
                        IsApproved = false,
                        IndustryId = input.IndustryId,
                        Discountable = discount != null ? (int)discount.Discountable : 0,
                        UnDiscountable = discount != null ? (int)discount.UnDiscountable : 0,
                    };
                    var companys = company.MapTo<NewCompany>();
                    id = _NewCompanyRepository.InsertAndGetId(companys);

                    var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();
                    comp.CustomerId = "FN-" + input.CompanyName.Substring(0, 3).ToUpper() + id;
                    _NewCompanyRepository.UpdateAsync(comp);
                }
                else
                {
                    CreateCompany company = new CreateCompany()
                    {
                        Name = input.CompanyName,
                        NewCustomerTypeId = 1,
                        ApprovedById = AbpSession.UserId,
                        IsApproved = true,
                        IndustryId = input.IndustryId,
                        Discountable = discount != null ? (int)discount.Discountable : 0,
                        UnDiscountable = discount != null ? (int)discount.UnDiscountable : 0,
                    };
                    var companys = company.MapTo<NewCompany>();
                    id = _NewCompanyRepository.InsertAndGetId(companys);

                    var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();
                    comp.CustomerId = "FN-" + input.CompanyName.Substring(0, 3).ToUpper() + id;
                    _NewCompanyRepository.UpdateAsync(comp);
                }
               
            }
            else
            {
                id = data[0].Id;
            }
            return id;
        }
        public virtual async Task GetDeleteInquiry(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 9);
                sqlComm.Parameters.AddWithValue("@Id", input.Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }
        public virtual async Task GetCompanyUpdate(CompanyUpdateInput input)
        {
            try
            {
                var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.EnquiryId).FirstOrDefault();

                if (input.CompanyId > 0)
                {
                    if (enquiryDetail != null)
                    {
                        var linkcomp = enquiryDetail.MapTo<EnquiryDetail>();
                        linkcomp.CompanyId = input.CompanyId;
                        if (input.DesignationId>0)
                        {
                            linkcomp.DesignationId = input.DesignationId;
                        }
                        await _enquiryDetailRepository.UpdateAsync(linkcomp);
                    }
                    else
                    {
                        LinkedCompanyInput linkedinput = new LinkedCompanyInput();
                        linkedinput.Id = 0;
                        linkedinput.CompanyId = input.CompanyId;                     
                        linkedinput.InquiryId = input.EnquiryId;
                        if (input.DesignationId > 0)
                        {
                            linkedinput.DesignationId = input.DesignationId;
                        }
                        linkCopanyCreate(linkedinput);
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }
        public async Task<FileDto> GetGeneralInquiryToExcel()
        {
            var quiry = _inquiryRepository.GetAll().Where(p => p.MileStoneId < 5 && p.Junk == null && p.IsClosed != true);
            var inquiry = (from a in quiry
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStoneId > 0 ? a.MileStones.MileStoneName : "",
                               Name = a.Name,
                               SubMmissionId = a.SubMmissionId,
                               SCreationTime = a.CreationTime.ToString(),
                               CompanyId = enqDetail.CompanyId,
                               CompanyName = enqDetail.CompanyId > 0 ? enqDetail.Companys.Name : a.CompanyName,
                               DepartmentName = enqDetail.DepartmentId > 0 ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               TeamId = enqDetail.TeamId ?? 0,
                               TeamName = enqDetail.TeamId > 0 ? enqDetail.Team.Name : "",
                               CreatedBy = ur != null ? ur.UserName : "",
                               CreationTime = a.CreationTime
                           });

            var inquirys = await inquiry
             .OrderByDescending(p => p.CreationTime)
             .ToListAsync();

            var inquiryListDtos = inquirys.MapTo<List<InquiryListDto>>();

            return _generalInquiryListExcelExporter.ExportToFile(inquiryListDtos);
        }
        public async Task<FileDto> GetSalesInquiryToExcel()

        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && enqDetail.AssignedbyId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && team.SalesManagerId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.DesignerId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.CoordinatorId == userid && enq.IsClosed != true
                         select enq
                        );
            }
            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == false && p.Junk == null && p.IsClosed != true);

            }
            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.MileStoneName,
                               Name = a.Name,
                               SubMmissionId = a.SubMmissionId,
                               CompanyId = enqDetail.CompanyId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               TeamId = enqDetail.TeamId,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy = ur.UserName,
                               SCreationTime = a.CreationTime.ToString(),
                               SalesMan = pr.UserName,
                               CreationTime = a.CreationTime,
                               SclosureDate = enqDetail.ClosureDate != null ? enqDetail.ClosureDate.ToString() : "",
                               SlastActivity = enqDetail.LastActivity != null ? enqDetail.LastActivity.ToString() : ""

                           });
            var inquirys = await inquiry
               .OrderByDescending(p => p.CreationTime)
               .ToListAsync();

            var salesinquiryListDtos = inquirys.MapTo<List<InquiryListDto>>();

            return _salesInquiryListExcelExporter.ExportToFile(salesinquiryListDtos);

        }
        public async Task<FileDto> GetLeadInquiryToExcel()
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();
            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from inq in _inquiryRepository.GetAll()
                         join enq in _enquiryDetailRepository.GetAll() on inq.Id equals enq.InquiryId
                         join team in _TeamRepository.GetAll() on enq.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where inq.MileStoneId == 3 || inq.MileStoneId == 4 && inq.Junk == null && team.SalesManagerId == userid && inq.IsClosed != true
                         select inq
                         );
            }

            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.MileStoneId == 3 || p.MileStoneId == 4 && p.Junk == null && p.IsClosed != true);
            }

            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           where a.IsDeleted == false
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStoneId > 0 ? a.MileStones.MileStoneName : "",
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               DesignationId = enqDetail.DesignationId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy = ur != null ? ur.UserName : "",
                               SalesMan = pr != null ? pr.UserName : "",
                               CreationTime = a.CreationTime
                           });

            var inquiryCount = inquiry.Count();

            var datas = inquiry
              .OrderByDescending(p => p.CreationTime)
              .ToList();

            var inquirylistoutput = datas.MapTo<List<InquiryListDto>>();

            return _leadInquiryListExcelExporter.ExportToFile(inquirylistoutput);
        }
        public async Task<PagedResultDto<QuotationListDto>> GetEnquiryQuotations(GetInquiryQuotationInput input)
        {

            var query = (from r in _quotationRepository.GetAll() where r.InquiryId == input.InquiryId && r.Revised == false select r);

            var inquiry = (from r in query
                           select new QuotationListDto
                           {
                               Id = r.Id,
                               RefNo = r.RefNo,
                               Name = r.Name,
                               CustomerId = r.CustomerId,
                               TermsandCondition = r.TermsandCondition,
                               NewCompanyId = r.NewCompanyId,
                               CompanyName = r.NewCompanyId != null ? r.NewCompanys.Name : "",
                               StatusName = r.Quotationstatus.Name,
                               QuotationStatusId = r.QuotationStatusId,
                               SalesPersonId = r.SalesPersonId,
                               SalesPersonName = r.SalesPersonId != null ? r.SalesPerson.UserName : "",
                               SCreationTime = r.CreationTime.ToString(),
                               CreationTime = r.LastModificationTime ?? r.CreationTime,
                               Email = r.Email,
                               MobileNumber = r.MobileNumber,
                               AttentionName = r.AttentionContactId != null ? (r.AttentionContact.Name + " " + r.AttentionContact.LastName) : "",
                               LostDate = r.LostDate,
                               WonDate = r.WonDate,
                               SubmittedDate = r.SubmittedDate,
                               Lost = r.Lost,
                               Won = r.Won,
                               Submitted = r.Submitted,
                               InquiryId = r.InquiryId,
                               InquiryName = r.InquiryId != null ? r.Inquiry.Name : "",
                               Void = r.Void,
                               Total = r.IsVat == true ? (r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total) + r.VatAmount : r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total,
                               Probability = r.EnqStatus.Percentage ?? 0
                           });
            var Count = await inquiry.CountAsync();
            var list = await inquiry
               .OrderBy(p => p.CreationTime)
               .PageBy(input)
               .ToListAsync();

            var ListDtos = list.MapTo<List<QuotationListDto>>();


            return new PagedResultDto<QuotationListDto>(Count, ListDtos);
        }
        public bool CheckInquiryDuplicate(CheckInquiryInput input)
        {
            var data = false;
            var inq = _inquiryRepository.GetAll().Where(p => p.Name == input.InquiryName).FirstOrDefault();
            if (inq != null)
            {
                data = true;
            }
            return data;
        }
        public virtual async Task CreateInquiryContactInfo(NullableIdDto input)
        {

            var Inquiry = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.Id).FirstOrDefault();

            if (Inquiry.ContactId != null)
            {
                var InquiryInfo = _inquiryRepository.GetAll().Where(q => q.Id == input.Id).FirstOrDefault();

                if (string.IsNullOrEmpty(InquiryInfo.LandlineNumber) == false)
                {
                    var LandLineInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == Inquiry.ContactId && p.InfoData == InquiryInfo.LandlineNumber && p.NewInfoTypeId == 9).FirstOrDefault();
                    if (LandLineInfo == null)
                    {
                        NewContactInfo NewContactType = new NewContactInfo()
                        {
                            NewContacId = Inquiry.ContactId,
                            NewInfoTypeId = 9,
                            InfoData = InquiryInfo.LandlineNumber
                        };
                        await _NewContactInfoRepository.InsertAsync(NewContactType);
                    }
                }
                if (string.IsNullOrEmpty(InquiryInfo.Email) == false)
                {
                    var EmailInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == Inquiry.ContactId && p.InfoData == InquiryInfo.Email && p.NewInfoTypeId == 4).FirstOrDefault();
                    if (EmailInfo == null)
                    {
                        NewContactInfo NewContactType = new NewContactInfo()
                        {
                            NewContacId = Inquiry.ContactId,
                            NewInfoTypeId = 4,
                            InfoData = InquiryInfo.Email
                        };
                        await _NewContactInfoRepository.InsertAsync(NewContactType);
                    }
                }
                if (string.IsNullOrEmpty(InquiryInfo.MbNo) == false)
                {
                    var MobileInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewContacId == Inquiry.ContactId && p.InfoData == InquiryInfo.MbNo && p.NewInfoTypeId == 7).FirstOrDefault();
                    if (MobileInfo == null)
                    {
                        NewContactInfo NewContactType = new NewContactInfo()
                        {
                            NewContacId = Inquiry.ContactId,
                            NewInfoTypeId = 7,
                            InfoData = InquiryInfo.MbNo
                        };
                        await _NewContactInfoRepository.InsertAsync(NewContactType);

                    }
                }

            }

        }
        public virtual async Task CreateInquiryCompanyInfo(NullableIdDto input)
        {

            var Inquiry = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.Id).FirstOrDefault();

            if (Inquiry.CompanyId != null)
            {
                var InquiryInfo = _inquiryRepository.GetAll().Where(q => q.Id == input.Id).FirstOrDefault();

                if (InquiryInfo.CLandlineNumber != null)
                {
                    var CLandLineInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == Inquiry.CompanyId && p.InfoData == InquiryInfo.CLandlineNumber && p.NewInfoTypeId == 9).FirstOrDefault();
                    if (CLandLineInfo == null)
                    {
                        NewContactInfo NewCompanyType = new NewContactInfo()
                        {
                            NewCompanyId = Inquiry.CompanyId,
                            NewInfoTypeId = 9,
                            InfoData = InquiryInfo.CLandlineNumber
                        };
                        await _NewContactInfoRepository.InsertAsync(NewCompanyType);
                    }
                }
                if (InquiryInfo.CEmail != null)
                {
                    var CEmailInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == Inquiry.CompanyId && p.InfoData == InquiryInfo.CEmail && p.NewInfoTypeId == 4).FirstOrDefault();
                    if (CEmailInfo == null)
                    {
                        NewContactInfo NewCompanyType = new NewContactInfo()
                        {
                            NewCompanyId = Inquiry.CompanyId,
                            NewInfoTypeId = 4,
                            InfoData = InquiryInfo.CEmail
                        };
                        await _NewContactInfoRepository.InsertAsync(NewCompanyType);
                    }
                }
                if (InquiryInfo.CMbNo != null)
                {
                    var CMobileInfo = _NewContactInfoRepository.GetAll().Where(p => p.NewCompanyId == Inquiry.CompanyId && p.InfoData == InquiryInfo.CMbNo && p.NewInfoTypeId == 10).FirstOrDefault();
                    if (CMobileInfo == null)
                    {
                        NewContactInfo NewCompanyType = new NewContactInfo()
                        {
                            NewCompanyId = Inquiry.CompanyId,
                            NewInfoTypeId = 10,
                            InfoData = InquiryInfo.CMbNo
                        };
                        await _NewContactInfoRepository.InsertAsync(NewCompanyType);

                    }
                }

            }

        }
        public ListResultDto<JobActivityList> GetJobActivity(NullableIdDto input)
        {
            var query = _jobActivityRepository.GetAll().Where(p => p.InquiryId == input.Id);
 
            var JobActivity = (from a in query
                               select new JobActivityList
                               {
                                   Id = a.Id,
                                   Title = a.Title,
                                   Remark = a.Remark,
                                   DesignerId = a.DesignerId,
                                   DesignerName = a.Designer.Name,
                                   InquiryId = a.InquiryId,
                                   InquiryName = a.Inquiry.Name,
                                   Isopen = a.Isopen,
                                   AllottedDate = a.AllottedDate,
                                   EndDate = a.EndDate,
                                   StartDate = a.StartDate,
                                   JobNumber = a.JobNumber
                               });
 
            var JobActivitylistoutput = JobActivity.MapTo<List<JobActivityList>>();
            return new ListResultDto<JobActivityList>(JobActivitylistoutput);
        }
        public async Task<GetJobActivity> GetJobActivityForEdit(NullableIdDto input)
        {
            var output = new GetJobActivity { };
            var query = _jobActivityRepository
               .GetAll().Where(p => p.Id == input.Id);


            if (query.Count() > 0)
            {
                var JobActivity = (from a in query
                                   select new JobActivityList
                                   {
                                       Id = a.Id,
                                       Title = a.Title,
                                       Remark = a.Remark,
                                       DesignerId = a.DesignerId,
                                       DesignerName = a.Designer.Name,
                                       InquiryId = a.InquiryId,
                                       InquiryName = a.Inquiry.Name,
                                       Isopen = a.Isopen,
                                       AllottedDate = a.AllottedDate,
                                       EndDate = a.EndDate,
                                       StartDate = a.StartDate,
                                       JobNumber = a.JobNumber
                                   }).FirstOrDefault();

                output = new GetJobActivity
                {
                    jobActivityList = JobActivity
                };
            }
            return output;
        }
        public async Task CreateOrUpdateJobActivity(CreateJobActivityInput input)
        {
            if (input.Id != 0)
            {
                await UpdateJobActivityAsync(input);
            }
            else
            {
                await CreateJobActivityAsync(input);
            }
        }
        public virtual async Task CreateJobActivityAsync(CreateJobActivityInput input)
        {
            var inq = _inquiryRepository.GetAll().Where(p => p.Id == input.InquiryId).FirstOrDefault();
            var jobcount = _jobActivityRepository.GetAll().Where(p => p.InquiryId == input.InquiryId).Count()+1;
            input.JobNumber = inq.SubMmissionId + "-D" + jobcount;
  
            var JobActivity = input.MapTo<JobActivity>();
            await _jobActivityRepository.InsertAsync(JobActivity);
        }
        public virtual async Task UpdateJobActivityAsync(CreateJobActivityInput input)
        {
            var JobActivity = await _jobActivityRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, JobActivity);
            await _jobActivityRepository.UpdateAsync(JobActivity);

        }
        public async Task GetDeleteJobActivity(EntityDto input)
        {
            await _jobActivityRepository.DeleteAsync(input.Id);
        }
        public async Task<Array> GetClosureDateInquiryTickets(GetClosureTicketInput input)
        {

            int c = 1, mm = -2;
            var Monthlist = new List<monthdto>();

            if (input.ClosureDate != null)
            {
                DateTime givendt = DateTime.Parse(input.ClosureDate);
                while (mm <= 3)
                {
                    DateTime dt = givendt.AddMonths(mm);
                    Monthlist.Add(new monthdto { Id = c, MonthNo = dt.Month, MonthName = dt.ToString("MMM"), Year = dt.Year });
                    mm = mm + 1;
                    c = c + 1;
                }
            }
            else
            {
                DateTime today = DateTime.Now;
                while (mm <= 3)
                {
                    DateTime dt = today.AddMonths(mm);
                    Monthlist.Add(new monthdto { Id = c, MonthNo = dt.Month, MonthName = dt.ToString("MMM"), Year = dt.Year });
                    mm = mm + 1;
                    c = c + 1;
                }
            }


            var SupportMonth = Monthlist.ToArray();


            long userid = (int)AbpSession.UserId;
            var userrole = (from a in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on a.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();


            var query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         where enq.Junk != true && enqDetail.ClosureDate != null && enq.Archieved != true && enq.IsClosed != true
                         select enqDetail);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.Junk != true && enqDetail.ClosureDate != null && enq.Archieved != true && enq.IsClosed != true && enqDetail.AssignedbyId == userid
                         select enqDetail
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.Junk != true && enqDetail.ClosureDate != null && enq.Archieved != true && enq.IsClosed != true && team.SalesManagerId == userid
                         select enqDetail
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join leadDetail in _LeadDetailRepository.GetAll() on enqDetail.InquiryId equals leadDetail.InquiryId
                         where enq.Junk != true && enqDetail.ClosureDate != null && enq.Archieved != true && enq.IsClosed != true && leadDetail.DesignerId == userid
                         select enqDetail
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join leadDetail in _LeadDetailRepository.GetAll() on enqDetail.InquiryId equals leadDetail.InquiryId
                         where enq.Junk != true && enqDetail.ClosureDate != null && enq.Archieved != true && enq.IsClosed != true && leadDetail.CoordinatorId == userid
                         select enqDetail
                        );
            }


            query = query.WhereIf(
                !input.Filter.IsNullOrEmpty(),
                 p =>
                      p.Inquirys.Name.Contains(input.Filter) ||
                      p.Inquirys.Designations.DesiginationName.Contains(input.Filter) ||
                      p.Inquirys.MileStones.MileStoneName.Contains(input.Filter) ||
                      p.Inquirys.Email.Contains(input.Filter) ||
                      p.Inquirys.Companys.Name.Contains(input.Filter) ||
                      p.Inquirys.CompanyName.Contains(input.Filter)
                );

            var NewStatuss = (from a in query
                              select new InquiryClosureDateListDto
                              {
                                  Id = a.Inquirys.Id,
                                  MileStoneId = a.Inquirys.MileStoneId,
                                  MileStoneName = a.Inquirys.MileStones.MileStoneName,
                                  CompanyName = a.Inquirys.CompanyName,
                                  DesignationName = a.Inquirys.DesignationName,
                                  Name = a.Inquirys.Name,
                                  Address = a.Inquirys.Address,
                                  WebSite = a.Inquirys.WebSite,
                                  Email = a.Inquirys.Email,
                                  MbNo = a.Inquirys.MbNo,
                                  Remarks = a.Inquirys.Remarks,
                                  SubMmissionId = a.Inquirys.SubMmissionId,
                                  IpAddress = a.Inquirys.IpAddress,
                                  Browcerinfo = a.Inquirys.Browcerinfo,
                                  CreatorUserId = a.Inquirys.LastModifierUserId != null ? (int)a.Inquirys.LastModifierUserId : (int)a.Inquirys.CreatorUserId,
                                  CreationTime = a.Inquirys.CreationTime,
                                  DepartmentId = 0,
                                  DepartmentName = "",
                                  StatusId = a.Inquirys.StatusId ?? 0,
                                  StatusColorCode = a.Inquirys.EnqStatus.EnqStatusColor,
                                  StatusName = a.Inquirys.EnqStatus.EnqStatusName ?? "",
                                  Percentage = (int)(a.Inquirys.EnqStatus.Percentage ?? 0),
                                  ClosureDate = (DateTime)a.ClosureDate
                              }).ToList();

            foreach (var d in NewStatuss)
            {
                try
                {
                    var user = await UserManager.GetUserByIdAsync((long)d.CreatorUserId);

                    GetProfilePictureOutput dp = new GetProfilePictureOutput("");

                    if (user != null)
                    {
                        if (user.ProfilePictureId != null)
                        {
                            //dp = await GetProfilePictureByIdInternal(user.ProfilePictureId.Value);
                            d.ProfilePicture = "";
                        }

                        d.UserName = user.UserName;
                    }

                    var test = (from enqDetail in _enquiryDetailRepository.GetAll()
                                where enqDetail.InquiryId == d.Id && enqDetail.ClosureDate != null
                                select new enqDetailDt
                                {
                                    CompanyId = enqDetail.CompanyId,
                                    DesignationId = enqDetail.DesignationId,
                                    CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : d.CompanyName,
                                    DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : d.DesignationName,
                                    DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                    DepartmentId = enqDetail.DepartmentId ?? 0,
                                    AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                    AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                    ContactId = enqDetail.ContactId ?? 0,
                                    TeamId = enqDetail.TeamId ?? 0,
                                    TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                                    SalesMan = enqDetail.AbpAccountManager.UserName ?? "",
                                    Estimationvalue = ((int)enqDetail.EstimationValue).ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                                    ClosureDate = enqDetail.ClosureDate,
                                    LastActivity = enqDetail.LastActivity,
                                    IsExpire = false,
                                    Estimation = (int)enqDetail.EstimationValue  // 06.02.18
                                }).FirstOrDefault();


                    d.EstimationValue = test.Estimation;   // 06.02.18
                    d.EstimationValueformat = test.Estimationvalue;
                    //d.ClosureDate = test.ClosureDate;
                    var date = DateTime.Now.AddDays(7);
                    if (date >= test.ClosureDate)
                    {
                        d.IsExpire = true;
                    }

                    d.ClosureDate = (DateTime)test.ClosureDate;
                    d.CompanyId = test.CompanyId;
                    d.DesignationId = test.DesignationId;
                    d.CompanyName = test.CompanyName;
                    d.DesignationName = test.DesignationName;
                    d.DepartmentName = test.DepartmentName;
                    d.DepartmentId = test.DepartmentId;
                    d.AssignedbyId = test.AssignedbyId;
                    d.AssignedTime = test.AssignedTime;
                    d.ContactId = test.ContactId;
                    d.TeamId = test.TeamId;
                    d.TeamName = test.TeamName;
                    d.EstimationValueformat = test.Estimationvalue;

                    // 21.02.18
                    if (test.AssignedbyId > 0)
                    {
                        byte[] bytes = new byte[0];
                        var Account = (from r in UserManager.Users where r.Id == (long)test.AssignedbyId select r).FirstOrDefault();
                        var profilePictureId = Account.ProfilePictureId;
                        d.SalesMan = Account.UserName;
                        d.AssignedbyImage = "/assets/common/images/default-profile-picture.png";
                        if (profilePictureId != null)
                        {
                            var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                            if (file != null)
                            {
                                bytes = file.Bytes;
                                GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                d.AssignedbyImage = "data:image/jpeg;base64," + img.ProfilePicture;
                            }
                        }
                    }
                    var leadDetail = _LeadDetailRepository.GetAll().Where(p => p.InquiryId == d.Id).FirstOrDefault();
                    if (leadDetail != null)
                    {
                        if (leadDetail.DesignerId > 0)
                        {

                            byte[] bytes = new byte[0];
                            var Account = (from s in UserManager.Users where s.Id == (long)leadDetail.DesignerId select s).FirstOrDefault();
                            d.DesignerName = Account.UserName;
                            var profilePictureId = Account.ProfilePictureId;
                            d.DesignerImage = "/assets/common/images/default-profile-picture.png";
                            if (profilePictureId != null)
                            {
                                var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                                if (file != null)
                                {
                                    bytes = file.Bytes;
                                    GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                    d.DesignerImage = "data:image/jpeg;base64," + img.ProfilePicture;
                                }
                            }
                        }
                    }


                    var acttype = (from a in _acitivityTrackRepository.GetAll()
                                   where (a.EnquiryId == d.Id)
                                   group a by new { a.ActivityId, a.Activity.ActivityName, a.Activity.ColorCode }
                                   into b
                                   select new ActivityColor { ActivityId = b.Key.ActivityId, ActivityName = b.Key.ActivityName, ActivityColors = b.Key.ColorCode }).ToArray();
                    foreach (var de in acttype)
                    {
                        de.ActivityCount = (from a in _acitivityTrackRepository.GetAll() where (a.EnquiryId == d.Id && a.ActivityId == de.ActivityId) select a.Id).Count().ToString();
                    }
                    d.ActivityColors = acttype;
                }
                catch (Exception ex)
                {

                }
            }


            var SubListout = new List<ClosureDateInquiry>();

            foreach (var newsts in SupportMonth)
            {
                // var queryClosuredt = (from r in NewStatuss where r.ClosureDate != null &&  r.ClosureDate.Month == newsts.MonthNo && r.ClosureDate.Year == newsts.Year select r).FirstOrDefault();
                var TotValue = (from r in NewStatuss where r.ClosureDate != null && r.ClosureDate.Month == newsts.MonthNo && r.ClosureDate.Year == newsts.Year select r).Sum(p => p.EstimationValue);
                SubListout.Add(new ClosureDateInquiry
                {
                    Id = newsts.Id,
                    MonthName = newsts.MonthName + " " + newsts.Year.ToString(),
                    TotalValueformat = TotValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.'),
                    CurrentDate = new DateTime(newsts.Year, newsts.MonthNo, 1),   //(DateTime)queryClosuredt.ClosureDate,                   
                    GetTicketReservation = (from r in NewStatuss where r.ClosureDate != null && r.ClosureDate.Month == newsts.MonthNo && r.ClosureDate.Year == newsts.Year select r).OrderByDescending(p => p.ClosureDate).ToArray(),
                });
            }

            return SubListout.ToArray();

        }
        public async Task<PagedResultDto<JobActivityList>> GetOverallJobActivity(GetoverallJobActivityInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _jobActivityRepository.GetAll().Where(r => r.Id == 0);
            if (userrole.DisplayName == "Sales Executive")
            {
                if (input.DesignerId > 0)
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on job.InquiryId equals enqDetail.InquiryId
                             join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                             where enqDetail.AssignedbyId == userid && job.DesignerId == input.DesignerId
                             select job
                        );
                }
                else
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on job.InquiryId equals enqDetail.InquiryId
                             join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                             where enqDetail.AssignedbyId == userid
                             select job
                        );
                }
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                if (input.DesignerId > 0)
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on job.InquiryId equals enqDetail.InquiryId
                             join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                             join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                             where team.SalesManagerId == userid && job.DesignerId == input.DesignerId
                             select job
                        );
                }
                else
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on job.InquiryId equals enqDetail.InquiryId
                             join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                             join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                             where team.SalesManagerId == userid
                             select job
                        );
                }
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from job in _jobActivityRepository.GetAll()
                         where job.DesignerId == userid
                         select job
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                if (input.DesignerId > 0)
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on job.InquiryId equals leadDetail.InquiryId
                             where leadDetail.CoordinatorId == userid && job.DesignerId == input.DesignerId
                             select job
                        );
                }
                else
                {
                    query = (from job in _jobActivityRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on job.InquiryId equals leadDetail.InquiryId
                             where leadDetail.CoordinatorId == userid
                             select job
                        );
                }
            }
            else
            {
                if (input.DesignerId > 0)
                {
                    query = _jobActivityRepository.GetAll().Where(p => p.DesignerId == input.DesignerId);
                }
                else
                {
                    query = _jobActivityRepository.GetAll();
                }
            }

            query = query
               .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p =>
                    p.Title.Contains(input.Filter) ||
                    p.JobNumber.Contains(input.Filter) ||
                    p.Inquiry.Name.Contains(input.Filter) ||
                    p.Designer.Name.Contains(input.Filter) ||
                    p.Remark.Contains(input.Filter)
               );

            var JobActivity = (from a in query
                               select new JobActivityList
                               {
                                   Id = a.Id,
                                   Title = a.Title,
                                   Remark = a.Remark,
                                   DesignerId = a.DesignerId,
                                   DesignerName = a.Designer.Name,
                                   InquiryId = a.InquiryId,
                                   InquiryName = a.Inquiry.Name,
                                   Isopen = a.Isopen,
                                   AllottedDate = a.AllottedDate,
                                   EndDate = a.EndDate,
                                   StartDate = a.StartDate,
                                   SallottedDate = a.AllottedDate.ToString(),
                                   SendDate = a.EndDate.ToString(),
                                   SstartDate = a.StartDate.ToString(),
                                   JobNumber = a.JobNumber,
                                   ScreationTime = a.CreationTime.ToString()
                               });

            var Count = await JobActivity.CountAsync();
            var list = await JobActivity
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();

            if (input.Sorting == "Title,DesignerName,InquiryName,Isopen,JobNumber")
            {
                list = await JobActivity
               .OrderByDescending(p => p.CreationTime)
               .PageBy(input)
               .ToListAsync();
            }

            var JobActivitylistoutput = list.MapTo<List<JobActivityList>>();

            return new PagedResultDto<JobActivityList>(Count, JobActivitylistoutput);

        }
        public virtual async Task UpdateSalesmanAll(SalesmanChange input)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_UpdateSalesman]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = input.CompanyId;
                    cmd.Parameters.Add("@SalesmanId", SqlDbType.Int).Value = input.SalesmanId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }
        public async Task<PagedResultDto<InquiryListDto>> GetClosedInquiry(GetInquiryInput input)
        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.IsClosed == true && enqDetail.AssignedbyId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.IsClosed == true && team.SalesManagerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.IsClosed == true && leadDetail.DesignerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.IsClosed == true && leadDetail.CoordinatorId == userid
                         select enq
                        );
            }
            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.IsClosed == true);
            }

            var closedinq = (from a in query
                             join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                             join d in _LeadDetailRepository.GetAll() on a.Id equals d.InquiryId
                             join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                             from ur in urJoined.DefaultIfEmpty()
                             join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                             from pr in prJoined.DefaultIfEmpty()
                             select new InquiryListDto
                             {
                                 Id = a.Id,
                                 MileStoneId = a.MileStoneId,
                                 MileStoneName = a.MileStones.MileStoneName,
                                 Name = a.Name,
                                 Address = a.Address,
                                 WebSite = a.WebSite,
                                 Email = a.Email,
                                 MbNo = a.MbNo,
                                 Remarks = a.Remarks,
                                 SubMmissionId = a.SubMmissionId,
                                 IpAddress = a.IpAddress,
                                 Browcerinfo = a.Browcerinfo,
                                 CreatorUserId = a.CreatorUserId ?? 0,
                                 SCreationTime = a.CreationTime.ToString(),
                                 CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                                 CompanyId = enqDetail.CompanyId,
                                 DesignationId = enqDetail.DesignationId,
                                 CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                                 DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                                 DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                 DepartmentId = enqDetail.DepartmentId ?? 0,
                                 AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                 AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                 TeamId = enqDetail.TeamId,
                                 TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                                 DesignerName = d.DesignerId != null ? d.Designers.Name : "",
                                 CreatedBy = ur.UserName,
                                 SalesMan = pr.UserName,
                                 IsClosed = a.IsClosed,
                                 IsReversable = true,
                                 SclosureDate = enqDetail.ClosureDate != null ? enqDetail.ClosureDate.ToString() : "",
                                 SlastActivity = enqDetail.LastActivity != null ? enqDetail.LastActivity.ToString() : ""

                             });

            closedinq = closedinq.WhereIf(
                           !input.Filter.IsNullOrEmpty(),
                           p => p.SubMmissionId.Contains(input.Filter) ||
                                p.Name.Contains(input.Filter) ||
                                p.CompanyName.Contains(input.Filter) ||
                                p.MileStoneName.Contains(input.Filter) ||
                                p.DepartmentName.Contains(input.Filter) ||
                                p.TeamName.Contains(input.Filter) ||
                                p.CreatedBy.Contains(input.Filter));

            var datas = await closedinq
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();


            var inquirylistoutput = datas.MapTo<List<InquiryListDto>>();

            inquirylistoutput = inquirylistoutput.GroupBy(i => i.Id).Select(i => i.First()).ToList();
            var inquiryCount = inquirylistoutput.Count();

            foreach (var data in inquirylistoutput)
            {
                var inqquotation = _quotationRepository.GetAll().Where(p => p.InquiryId == data.Id && p.Won == true).ToList();
                if (inqquotation.Count > 0)
                {
                    data.IsReversable = false;
                }

            }
            return new PagedResultDto<InquiryListDto>(inquiryCount, inquirylistoutput);
        }
        public async Task<PagedResultDto<InquiryListDto>> GetJunkInquiry(GetInquiryInput input)
        {
            long userid = (int)AbpSession.UserId;


            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();


            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == true && enqDetail.AssignedbyId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.MileStoneId > 3 && enq.Junk == true && team.SalesManagerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == true && leadDetail.DesignerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.MileStoneId > 3 && enq.Junk == true && leadDetail.CoordinatorId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Admin")
            {
                query = _inquiryRepository.GetAll().Where(p => p.Junk == true);
            }
            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.Junk == true && p.CreatorUserId == userid);
            }

            query = query.WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p =>
                     p.CompanyName.Contains(input.Filter) ||
                     p.Name.Contains(input.Filter) ||
                     p.Designations.DesiginationName.Contains(input.Filter) ||
                     p.MileStones.MileStoneName.Contains(input.Filter) ||
                     p.Email.Contains(input.Filter)
                );

            var inquiry = (from a in query
                           join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                           join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                           from ur in urJoined.DefaultIfEmpty()
                           join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                           from pr in prJoined.DefaultIfEmpty()
                           select new InquiryListDto
                           {
                               Id = a.Id,
                               MileStoneId = a.MileStoneId,
                               MileStoneName = a.MileStones.MileStoneName,
                               Name = a.Name,
                               Address = a.Address,
                               WebSite = a.WebSite,
                               Email = a.Email,
                               MbNo = a.MbNo,
                               Remarks = a.Remarks,
                               SubMmissionId = a.SubMmissionId,
                               IpAddress = a.IpAddress,
                               Browcerinfo = a.Browcerinfo,
                               JunkDate = a.JunkDate,
                               CreatorUserId = a.CreatorUserId ?? 0,
                               SCreationTime = a.CreationTime.ToString(),
                               CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                               CompanyId = enqDetail.CompanyId,
                               DesignationId = enqDetail.DesignationId,
                               CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                               DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                               DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                               DepartmentId = enqDetail.DepartmentId ?? 0,
                               AssignedbyId = enqDetail.AssignedbyId ?? 0,
                               AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                               TeamId = enqDetail.TeamId ?? 0,
                               TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                               CreatedBy = ur.UserName,
                           });

            var datas = inquiry.OrderBy(input.Sorting)
                .PageBy(input).ToList();


            var inquiryCount = datas.Count();

            if (input.Sorting == "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy")
            {
                datas = datas.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(a => a.CreationOrModification).ToList();
            }



            var inquirylistoutput = datas.MapTo<List<InquiryListDto>>();


            return new PagedResultDto<InquiryListDto>(
                inquiryCount, inquirylistoutput);
        }
        public async Task<FileDto> GetClosedInquiryToExcel()

        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                         where enq.IsClosed == true && enqDetail.AssignedbyId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where enq.IsClosed == true && team.SalesManagerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.IsClosed == true && leadDetail.DesignerId == userid
                         select enq
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from enq in _inquiryRepository.GetAll()
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where enq.IsClosed == true && leadDetail.CoordinatorId == userid
                         select enq
                        );
            }
            else
            {
                query = _inquiryRepository.GetAll().Where(p => p.IsClosed == true);
            }

            var closedinq = (from a in query
                             join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                             join d in _LeadDetailRepository.GetAll() on a.Id equals d.InquiryId
                             join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                             from ur in urJoined.DefaultIfEmpty()
                             join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                             from pr in prJoined.DefaultIfEmpty()
                             select new InquiryListDto
                             {
                                 Id = a.Id,
                                 SubMmissionId = a.SubMmissionId,
                                 Name = a.Name,
                                 CompanyId = enqDetail.CompanyId,
                                 CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                                 MileStoneId = a.MileStoneId,
                                 MileStoneName = a.MileStones.MileStoneName,
                                 DepartmentId = enqDetail.DepartmentId ?? 0,
                                 DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                 TeamId = enqDetail.TeamId,
                                 TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                                 CreatedBy = ur.UserName,
                                 SCreationTime = a.CreationTime.ToString(),
                                 SclosureDate = enqDetail.ClosureDate != null ? enqDetail.ClosureDate.ToString() : "",
                                 SlastActivity = enqDetail.LastActivity != null ? enqDetail.LastActivity.ToString() : ""

                             });

            var inquirylistoutput = closedinq.MapTo<List<InquiryListDto>>();

            inquirylistoutput = inquirylistoutput.GroupBy(i => i.Id).Select(i => i.First()).ToList();


            return _closedInquiryListExcelExporter.ExportToFile(inquirylistoutput);

        }
        public async Task<FileDto> GetSalesQuotationsToExcel()

        {
            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            var query = _quotationRepository.GetAll().Where(p => p.Id == 0);

            if (userrole.DisplayName == "Sales Executive")
            {
                query = (from q in _quotationRepository.GetAll()
                         where q.Revised == false && q.SalesPersonId == userid && q.MileStoneId > 3 && q.MileStones.IsQuotation == true && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                         join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                         join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                         where q.Revised == false && team.SalesManagerId == userid && q.MileStoneId > 3 && q.MileStones.IsQuotation == true && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.DesignerId == userid && q.MileStoneId > 3 && q.MileStones.IsQuotation == true && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.CoordinatorId == userid && q.MileStoneId > 3 && q.MileStones.IsQuotation == true && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else
            {
                query = _quotationRepository.GetAll().Where(p => p.MileStoneId > 3 && p.MileStones.IsQuotation == true && p.Revised == false && p.IsClosed != true && p.Archieved != true && p.Void != true);

            }
            var inquiry = (from r in query
                           join d in _LeadDetailRepository.GetAll() on r.InquiryId equals d.InquiryId
                           select new QuotationListDto
                           {
                               Id = r.Id,
                               RefNo = r.RefNo,
                               Name = r.Name,
                               CustomerId = r.CustomerId,
                               TermsandCondition = r.TermsandCondition,
                               NewCompanyId = r.NewCompanyId,
                               CompanyName = r.NewCompanyId != null ? r.NewCompanys.Name : "",
                               StatusName = r.Quotationstatus.Name,
                               QuotationStatusId = r.QuotationStatusId,
                               SalesPersonId = r.SalesPersonId,
                               SalesPersonName = r.SalesPersonId != null ? r.SalesPerson.UserName : "",
                               CreationTime = r.CreationTime,
                               SCreationTime = r.CreationTime.ToString(),
                               Email = r.Email,
                               MobileNumber = r.MobileNumber,
                               AttentionName = r.AttentionContactId != null ? (r.AttentionContact.Name + " " + r.AttentionContact.LastName) : "",
                               LostDate = r.LostDate,
                               WonDate = r.WonDate,
                               SubmittedDate = r.SubmittedDate,
                               Lost = r.Lost,
                               Won = r.Won,
                               Submitted = r.Submitted,
                               InquiryId = r.InquiryId,
                               InquiryName = r.InquiryId != null ? r.Inquiry.Name : "",
                               DesignerName = d.DesignerId != null ? d.Designers.Name : "",

                           }).ToList();


            var salesquotationListDtos = inquiry.MapTo<List<QuotationListDto>>();

            return _salesQuotationsListExcelExporter.ExportToFile(salesquotationListDtos);

        }
        public async Task<PagedResultDto<InquiryListDto>> GetSalesReportGenerator(GetInquiryReportInput input)
        {
            DateTime curDate = DateTime.Now;
            long? userid = (int)AbpSession.UserId;
            var query = _inquiryRepository.GetAll().Where(r => r.Id == 0);
            var views = _viewRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            var count = 0;
            var inquirylistoutput = ObjectMapper.Map<List<InquiryListDto>>(query);

            if (views != null)
            {
                string Refno = "Quotation Ref No";
                int start = views.Query.IndexOf(Refno);
                if (start > -1)
                {
                    userid = views.AllPersonId > 0 ? views.AllPersonId : userid;
                    var queryQuotation = _quotationRepository.GetAll().Where(p => p.Id == 0);
                    var userrole = (from c in UserManager.Users
                                    join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                                    join role in _roleManager.Roles on urole.RoleId equals role.Id
                                    where urole.UserId == userid
                                    select role).FirstOrDefault();

                    if (userrole.DisplayName == "Sales Executive")
                    {
                        queryQuotation = (from q in _quotationRepository.GetAll()
                                 where q.Revised == false && q.SalesPersonId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                                 select q
                                );
                    }
                    else if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
                    {
                        queryQuotation = (from q in _quotationRepository.GetAll()
                                 join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                                 join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                                 join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                                 join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                                 where q.Revised == false && team.SalesManagerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                                 select q
                                );
                    }
                    else if (userrole.DisplayName == "Designer")
                    {
                        queryQuotation = (from q in _quotationRepository.GetAll()
                                 join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                                 join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                                 where q.Revised == false && leadDetail.DesignerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                                 select q
                                );
                    }
                    else if (userrole.DisplayName == "Sales Coordinator")
                    {
                        queryQuotation = (from q in _quotationRepository.GetAll()
                                 join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                                 join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                                 where q.Revised == false && leadDetail.CoordinatorId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                                 select q
                                );
                    }
                    else
                    {
                        queryQuotation = _quotationRepository.GetAll().Where(p => p.Revised == false);
                    }
 
                var quotation = (from a in queryQuotation
                                 join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                                 from ur in urJoined.DefaultIfEmpty()
                                 join er in _enquiryDetailRepository.GetAll() on a.InquiryId equals er.InquiryId into erJoined
                                 from er in erJoined.DefaultIfEmpty()
                                 select new InquiryListDto
                                 {
                                     Id = a.Inquiry.Id,
                                     MileStoneId = a.Inquiry.MileStoneId,
                                     MileStoneName = a.Inquiry.MileStones.MileStoneName,
                                     Name = a.Inquiry.Name,
                                     Address = a.Inquiry.Address,
                                     WebSite = a.Inquiry.WebSite,
                                     Email = a.Inquiry.Email,
                                     MbNo = a.Inquiry.MbNo,
                                     Remarks = a.Inquiry.Remarks,
                                     SubMmissionId = a.Inquiry.SubMmissionId,
                                     IpAddress = a.Inquiry.IpAddress,
                                     Browcerinfo = a.Inquiry.Browcerinfo,
                                     CreatorUserId = a.CreatorUserId ?? 0,
                                     SCreationTime = a.CreationTime.ToString(),
                                     CreationTime = a.CreationTime,
                                     CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                                     CompanyName = a.Inquiry.CompanyName,
                                     DesignationName = a.Inquiry.DesignationName,
                                     CreatedBy = ur != null ? ur.UserName : "",
                                     SalesMan = a.SalesPersonId != null ? a.SalesPerson.UserName : "",
                                     DisableQuotation = a.Inquiry.DisableQuotation,
                                     Won = a.Won,
                                     IsClosed = a.IsClosed,
                                     LeadStatusName = a.Quotationstatus.Name,
                                     QuotationRefno = a.RefNo,
                                     QuotationId = a.Id,
                                     Total = a.Total,
                                     QuotationStatusId = a.QuotationStatusId,
                                     SclosureDate = er.ClosureDate != null ? er.ClosureDate.ToString() : "",
                                     SlastActivity = er.LastActivity != null ? er.LastActivity.ToString() : "",
                                 });

                var Quotations = quotation.ToList();


                foreach (var p in Quotations)
                {
                    var enqDetail = (from ed in _enquiryDetailRepository.GetAll() where ed.InquiryId == p.Id select ed).FirstOrDefault();

                    if (enqDetail != null)
                    {
                        var cmp = (from c in _NewCompanyRepository.GetAll() where c.Id == enqDetail.CompanyId select c).FirstOrDefault();
                        var desig = (from c in _DesignationRepository.GetAll() where c.Id == enqDetail.DesignationId select c).FirstOrDefault();
                        var dept = (from c in _DepartmentsRepository.GetAll() where c.Id == enqDetail.DepartmentId select c).FirstOrDefault();
                        var team = (from c in _TeamRepository.GetAll() where c.Id == enqDetail.TeamId select c).FirstOrDefault();

                        p.CompanyId = enqDetail.CompanyId;
                        p.DesignationId = enqDetail.DesignationId;
                        p.DepartmentId = enqDetail.DepartmentId;
                        p.TeamId = enqDetail.TeamId;
                        p.AssignedbyId = enqDetail.AssignedbyId;

                        if (cmp != null)
                            p.CompanyName = cmp.Name;
                        if (desig != null)
                            p.DesignationName = desig.DesiginationName;
                        if (dept != null)
                            p.DepartmentName = dept.DepatmentName;
                        if (team != null)
                            p.TeamName = team.Name;
                        p.EstimationValue = enqDetail.EstimationValue;
                        p.AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "";
                    }

                    var MaxMileStonesId = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id select r).Max(c => c.MileStoneId);

                    if (MaxMileStonesId > 0)
                    {
                        var mile = await _milestoneRepository.GetAsync((int)MaxMileStonesId);
                        var fvalue = (from r in _quotationRepository.GetAll()
                                      where r.InquiryId == p.Id && r.Revised != true && r.Archieved != true && r.Void != true && r.Total > 0 && r.MileStoneId == MaxMileStonesId
                                      select r).Min(a => a.Negotiation == true ? a.Total - a.OverAllDiscountAmount : a.Total);
                        if (fvalue > 0)
                        {
                            p.EstimationValue = fvalue;
                            p.EstimationValueformat = p.EstimationValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.');
                            p.MileStoneName = mile != null ? mile.MileStoneName : p.MileStoneName;
                        }
                    }
                }

                if (views.GraterAmount > 0)
                {
                    Quotations = (from r in Quotations where r.EstimationValue <= views.GraterAmount select r).ToList();
                }
                if (views.LessAmount > 0)
                {
                    Quotations = (from r in Quotations where r.EstimationValue >= views.LessAmount select r).ToList();
                }

                if (views.QuotationStatusId > 0)
                {
                    switch (views.QuotationStatusId)
                    {
                        case 1:
                            Quotations = (from r in Quotations where r.LeadStatusName == "New" select r).ToList();
                            break;
                        case 2:
                            Quotations = (from r in Quotations where r.LeadStatusName == "Submitted" select r).ToList();
                            break;
                        case 3:
                            Quotations = (from r in Quotations where r.LeadStatusName == "Revised" select r).ToList();
                            break;
                        case 4:
                            Quotations = (from r in Quotations where r.LeadStatusName == "Won" select r).ToList();
                            break;
                        case 5:
                            Quotations = (from r in Quotations where r.LeadStatusName == "Lost" select r).ToList();
                            break;
                    }
                }

                if (views.DateFilterId > 0)
                {
                    switch (views.DateFilterId)
                    {
                        case 1:
                            Quotations = (from r in Quotations where r.CreationTime >= DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek) - 1) select r).ToList();
                            break;
                        case 2:
                            Quotations = (from r in Quotations where r.CreationTime >= new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 3:
                            Quotations = (from r in Quotations where r.CreationTime >= new DateTime(curDate.Year, 1, 1) select r).ToList();
                            break;
                        case 4:
                            Quotations = (from r in Quotations where r.CreationTime >= curDate.AddMonths(-3).AddDays(1 - curDate.Day) && r.CreationTime < new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 5:
                            Quotations = (from r in Quotations where r.CreationTime >= curDate.AddMonths(-6).AddDays(1 - curDate.Day) && r.CreationTime < new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 6:
                            Quotations = (from r in Quotations where r.CreationTime >= new DateTime(DateTime.Now.Year - 1, 1, 1) && r.CreationTime < new DateTime(DateTime.Now.Year, 1, 1) select r).ToList();
                            break;
                    }
                }
                    count = Quotations.Count();
                    Quotations = Quotations.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(a => a.CreationTime).ToList();
                    inquirylistoutput = ObjectMapper.Map<List<InquiryListDto>>(Quotations);
                }
                else
                {
                userid = views.AllPersonId > 0 ? views.AllPersonId : userid;
                var auserrole = (from c in UserManager.Users
                                 join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                                 join role in _roleManager.Roles on urole.RoleId equals role.Id
                                 where urole.UserId == userid
                                 select role).FirstOrDefault();


                if (auserrole.DisplayName == "Sales Executive")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                             join usr in UserManager.Users on enqDetail.AssignedbyId equals usr.Id
                             where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && enqDetail.AssignedbyId == userid
                             select enq
                            );
                }
                else if (auserrole.DisplayName == "Sales Manager" || auserrole.DisplayName == "Sales Manager / Sales Executive")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join enqDetail in _enquiryDetailRepository.GetAll() on enq.Id equals enqDetail.InquiryId
                             join team in _TeamRepository.GetAll() on enqDetail.TeamId equals team.Id
                             join usr in UserManager.Users on team.SalesManagerId equals usr.Id
                             where enq.MileStoneId > 3 && enq.Junk == null && enq.MileStones.IsQuotation == false && team.SalesManagerId == userid
                             select enq
                            );
                }
                else if (auserrole.DisplayName == "Designer")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                             where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.DesignerId == userid
                             select enq
                            );
                }
                else if (auserrole.DisplayName == "Sales Coordinator")
                {
                    query = (from enq in _inquiryRepository.GetAll()
                             join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                             where enq.MileStoneId > 3 && enq.Junk == null && leadDetail.CoordinatorId == userid
                             select enq
                            );
                }
                else
                {
                    query = _inquiryRepository.GetAll().Where(p => p.MileStoneId > 3 && p.Junk == null);

                }

                var inquiry = (from a in query
                               join enqDetail in _enquiryDetailRepository.GetAll() on a.Id equals enqDetail.InquiryId
                               join ur in UserManager.Users on a.CreatorUserId equals ur.Id into urJoined
                               from ur in urJoined.DefaultIfEmpty()
                               join pr in UserManager.Users on enqDetail.AssignedbyId equals pr.Id into prJoined
                               from pr in prJoined.DefaultIfEmpty()
                               select new InquiryListDto
                               {
                                   Id = a.Id,
                                   MileStoneId = a.MileStoneId,
                                   MileStoneName = a.MileStones.MileStoneName,
                                   Name = a.Name,
                                   Address = a.Address,
                                   WebSite = a.WebSite,
                                   Email = a.Email,
                                   MbNo = a.MbNo,
                                   Remarks = a.Remarks,
                                   SubMmissionId = a.SubMmissionId,
                                   IpAddress = a.IpAddress,
                                   Browcerinfo = a.Browcerinfo,
                                   CreatorUserId = a.CreatorUserId ?? 0,
                                   SCreationTime = a.CreationTime.ToString(),
                                   CreationTime = a.CreationTime,
                                   CreationOrModification = a.LastModificationTime ?? a.CreationTime,
                                   CompanyId = enqDetail.CompanyId,
                                   DesignationId = enqDetail.DesignationId,
                                   CompanyName = enqDetail.CompanyId != null ? enqDetail.Companys.Name : a.CompanyName,
                                   DesignationName = enqDetail.DesignationId != null ? enqDetail.Designations.DesiginationName : a.DesignationName,
                                   DepartmentName = enqDetail.DepartmentId != null ? enqDetail.Departments.DepatmentName : "",
                                   DepartmentId = enqDetail.DepartmentId ?? 0,
                                   AssignedbyId = enqDetail.AssignedbyId ?? 0,
                                   AssignedTime = enqDetail.AssignedbyDate != null ? enqDetail.AssignedbyDate.ToString() : "",
                                   TeamId = enqDetail.TeamId,
                                   TeamName = enqDetail.TeamId != null ? enqDetail.Team.Name : "",
                                   CreatedBy = ur != null ? ur.UserName : "",
                                   SalesMan = pr != null ? pr.UserName : "",
                                   DisableQuotation = a.DisableQuotation,
                                   Won = a.Won,
                                   IsClosed = a.IsClosed,
                                   EstimationValue = enqDetail.EstimationValue,
                                   SclosureDate = enqDetail.ClosureDate != null ? enqDetail.ClosureDate.ToString() : "",
                                   SlastActivity = enqDetail.LastActivity != null ? enqDetail.LastActivity.ToString() : "",
                                   LeadStatusName = a.Won == true ? "Won" : (a.IsClosed == true ? "Closed" : "Open")
                               });

                var inquirys = inquiry.ToList();

                foreach (var p in inquirys)
                {
                    var stage = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id && r.StageId == 17 select r).FirstOrDefault();
                    if (stage != null)
                    {
                        p.LeadStatusName = "Lost";
                    }

                    var MaxMileStonesId = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id select r).Max(c => c.MileStoneId);

                    if (MaxMileStonesId > 0)
                    {
                        var mile = await _milestoneRepository.GetAsync((int)MaxMileStonesId);
                        var fvalue = (from r in _quotationRepository.GetAll() where r.InquiryId == p.Id && r.Revised != true && r.Archieved != true && r.Void != true && r.Total > 0 && r.MileStoneId == MaxMileStonesId select r).Min(a => a.Negotiation == true ? a.Total - a.OverAllDiscountAmount : a.Total);
                        if (fvalue > 0)
                        {
                            p.EstimationValue = fvalue;
                            p.EstimationValueformat = p.EstimationValue.ToString("N", new CultureInfo("en-US")).TrimEnd('0').TrimEnd('.');
                            p.MileStoneName = mile != null ? mile.MileStoneName : p.MileStoneName;
                        }
                    }


                }

                if (views.GraterAmount > 0)
                {
                    inquirys = (from r in inquirys where r.EstimationValue <= views.GraterAmount select r).ToList();
                }
                if (views.LessAmount > 0)
                {
                    inquirys = (from r in inquirys where r.EstimationValue >= views.LessAmount select r).ToList();
                }

                if (views.EnqStatusId > 0)
                {
                    switch (views.EnqStatusId)
                    {
                        case 1:
                            inquirys = (from r in inquirys where r.LeadStatusName == "Open" select r).ToList();
                            break;
                        case 2:
                            inquirys = (from r in inquirys where r.LeadStatusName == "Closed" select r).ToList();
                            break;
                        case 3:
                            inquirys = (from r in inquirys where r.LeadStatusName == "Won" select r).ToList();
                            break;
                        case 4:
                            inquirys = (from r in inquirys where r.LeadStatusName == "Lost" select r).ToList();
                            break;
                    }

                }

                if (views.DateFilterId > 0)
                {
                    switch (views.DateFilterId)
                    {
                        case 1:
                            inquirys = (from r in inquirys where r.CreationTime >= DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek) - 1) select r).ToList();
                            break;
                        case 2:
                            inquirys = (from r in inquirys where r.CreationTime >= new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 3:
                            inquirys = (from r in inquirys where r.CreationTime >= new DateTime(curDate.Year, 1, 1) select r).ToList();
                            break;
                        case 4:
                            inquirys = (from r in inquirys where r.CreationTime >= curDate.AddMonths(-3).AddDays(1 - curDate.Day) && r.CreationTime < new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 5:
                            inquirys = (from r in inquirys where r.CreationTime >= curDate.AddMonths(-6).AddDays(1 - curDate.Day) && r.CreationTime < new DateTime(curDate.Year, curDate.Month, 1) select r).ToList();
                            break;
                        case 6:
                            inquirys = (from r in inquirys where r.CreationTime >= new DateTime(DateTime.Now.Year - 1, 1, 1) && r.CreationTime < new DateTime(DateTime.Now.Year, 1, 1) select r).ToList();
                            break;
                    }
                }

                count = inquirys.Count();
                inquirys = inquirys.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(a => a.CreationTime).ToList();
                inquirylistoutput = ObjectMapper.Map<List<InquiryListDto>>(inquirys);
            }
                
            }
            return new PagedResultDto<InquiryListDto>(
                    count, inquirylistoutput);
        }
    }
    public class monthdto
    {
        public int MonthNo { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }
    }
    public class SalesmanChange
    {
        public int CompanyId { get; set; }
        public int SalesmanId { get; set; }
    }
}
