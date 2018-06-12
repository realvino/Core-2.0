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
using tibs.stem.NewCustomerCompanys;
using tibs.stem.Products;
using tibs.stem.QuotationProducts;
using tibs.stem.Quotations;
using tibs.stem.Quotationss.Dto;
using tibs.stem.QuotationStatuss;
using tibs.stem.Sections;
using tibs.stem.QuotationProductss.Dto;
using tibs.stem.ImportHistorys;
using tibs.stem.Citys;
using tibs.stem.ProductImageUrls;
using tibs.stem.Inquirys;
using tibs.stem.TemporaryProducts;
using tibs.stem.Discounts;
using System.Globalization;
using tibs.stem.EnquiryStatuss;
using tibs.stem.Authorization.Users.Profile.Dto;
using tibs.stem.LeadDetails;
using tibs.stem.Storage;
using tibs.stem.Milestones;
using Microsoft.AspNetCore.Identity;
using Abp.Authorization.Users;
using tibs.stem.EnquiryDetails;
using tibs.stem.Team;
using tibs.stem.TeamDetails;
using tibs.stem.Authorization.Roles;
using tibs.stem.Quotationss.Exporting;
using tibs.stem.Dto;
using tibs.stem.Authorization.Users;
using tibs.stem.AcitivityTracks;
using tibs.stem.Inquirys.Dto;
using tibs.stem.Views;

namespace tibs.stem.Quotationss
{
    public class QuotationAppService : stemAppServiceBase, IQuotationAppService
    {
        private readonly IRepository<Quotation> _quotationRepository;
        private readonly IRepository<NewCompany> _newCompanyRepository;
        private readonly IRepository<QuotationStatus> _quotationStatusRepository;
        private readonly IRepository<Section> _sectionRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<QuotationProduct> _quotationProductRepository;
        private readonly IRepository<ImportHistory> _ImportHistoryRepository;
        private readonly IRepository<NewAddressInfo> _NewAddressInfoRepository;
        private readonly IRepository<City> _CityRepository;
        private readonly IRepository<ProductImageUrl> _ProductImageRepository;
        private readonly IRepository<ProductImageUrl> _productImageUrlRepository;
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<TemporaryProduct> _TempProductRepository;
        private readonly IRepository<NewContactInfo> _NewContactInfoRepository;
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<EnquiryStatus> _enqStatusRepository;
        private readonly IRepository<LeadDetail> _LeadDetailRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<StageDetails> _StageDetailRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;
        private readonly IRepository<Teams> _TeamRepository;
        private readonly IRepository<TeamDetail> _TeamDetailRepository;
        private readonly IRepository<TemporaryProductImage> _TempProductImageRepository;
        private readonly IQuotationListExcelExporter _quotationListExcelExporter;
        private List<QuotationReportListDto> inquirylistdto;
        private readonly IUserEmailer _userEmailer;
        private readonly IRepository<AcitivityTrack> _acitivityTrackRepository;
        private readonly IQuotationInquiryFilterListExcelExporter _QuotationInquiryFilterListExcelExporter;
        private readonly IRepository<View> _ViewRepository;
        private readonly ITeamEnquiryReportExcelExporter _TeamEnquiryReportExcelExporter;
        private readonly ITeamReportExcelExporter _TeamReportExcelExporter;
        private readonly IAllTeamReportExcelExporter _AllTeamReportExcelExporter;

        public QuotationAppService(
            IRepository<Quotation> quotationRepository,
            IRepository<NewCompany> newCompanyRepository,
            IRepository<AcitivityTrack> acitivityTrackRepository,
            IRepository<QuotationStatus> quotationStatusRepository,
            IRepository<Section> sectionRepository,
            IRepository<Product> productRepository,
            IQuotationListExcelExporter quotationListExcelExporter,
            IRepository<LeadDetail> LeadDetailRepository,
            IRepository<QuotationProduct> quotationProductRepository,
            IRepository<ImportHistory> ImportHistoryRepository,
            IRepository<NewAddressInfo> NewAddressInfoRepository,
            IRepository<City> CityRepository,
            IRepository<ProductImageUrl> ProductImageRepository,
            IRepository<ProductImageUrl> productImageUrlRepository,
            IRepository<Inquiry> inquiryRepository,
            IRepository<TemporaryProduct> TempProductRepository,
            IRepository<NewContactInfo> NewContactInfoRepository,
            IRepository<Discount> discountRepository,
            IBinaryObjectManager binaryObjectManager,
            IRepository<EnquiryStatus> enqStatusRepository,
            IRepository<StageDetails> StageDetailRepository,
            IUserEmailer userEmailer,
            RoleManager roleManager, IRepository<UserRole, long> userRoleRepository,
            IRepository<EnquiryDetail> enquiryDetailRepository,
            IRepository<Teams> TeamRepository,
            IRepository<TeamDetail> TeamDetailRepository,
            IQuotationInquiryFilterListExcelExporter QuotationInquiryFilterListExcelExporter,
            IRepository<View> ViewRepository,
            IRepository<TemporaryProductImage> TempProductImageRepository,
            ITeamEnquiryReportExcelExporter TeamEnquiryReportExcelExporter,
            ITeamReportExcelExporter TeamReportExcelExporter,
            IAllTeamReportExcelExporter AllTeamReportExcelExporter
            )
        {
            _quotationRepository = quotationRepository;
            _LeadDetailRepository = LeadDetailRepository;
            _newCompanyRepository = newCompanyRepository;
            _quotationStatusRepository = quotationStatusRepository;
            _sectionRepository = sectionRepository;
            _productRepository = productRepository;
            _quotationProductRepository = quotationProductRepository;
            _ImportHistoryRepository = ImportHistoryRepository;
            _NewAddressInfoRepository = NewAddressInfoRepository;
            _CityRepository = CityRepository;
            _ProductImageRepository = ProductImageRepository;
            _productImageUrlRepository = productImageUrlRepository;
            _inquiryRepository = inquiryRepository;
            _TempProductRepository = TempProductRepository;
            _NewContactInfoRepository = NewContactInfoRepository;
            _discountRepository = discountRepository;
            _enqStatusRepository = enqStatusRepository;
            _binaryObjectManager = binaryObjectManager;
            _StageDetailRepository = StageDetailRepository;
            _roleManager = roleManager;
            _userRoleRepository = userRoleRepository;
            _enquiryDetailRepository = enquiryDetailRepository;
            _TeamRepository = TeamRepository;
            _TeamDetailRepository = TeamDetailRepository;
            _TempProductImageRepository = TempProductImageRepository;
            _quotationListExcelExporter = quotationListExcelExporter;
            _userEmailer = userEmailer;
            _acitivityTrackRepository = acitivityTrackRepository;
            _QuotationInquiryFilterListExcelExporter = QuotationInquiryFilterListExcelExporter;
            _ViewRepository = ViewRepository;
            _TeamEnquiryReportExcelExporter = TeamEnquiryReportExcelExporter;
            _TeamReportExcelExporter = TeamReportExcelExporter;
            _AllTeamReportExcelExporter = AllTeamReportExcelExporter;
        }
        public async Task<PagedResultDto<QuotationListDto>> GetQuotation(GetQuotationInput input)
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
                         where q.Revised == false && q.SalesPersonId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
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
                         where q.Revised == false && team.SalesManagerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.DesignerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.CoordinatorId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else
            {
                query = _quotationRepository.GetAll().Where(p => p.Revised == false);

            }
            query = query
               .Include(u => u.NewCompanys)
               .Include(u => u.Quotationstatus)
               .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.RefNo.Contains(input.Filter) ||
                    u.Id.ToString().Contains(input.Filter) ||
                    u.CustomerId.ToString().Contains(input.Filter) ||
                    u.NewCompanys.Name.Contains(input.Filter) ||
                    u.Quotationstatus.Name.Contains(input.Filter)
                    );

            var reg = (from r in query
                       join ur in UserManager.Users on r.CreatorUserId equals ur.Id

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
                           CreationTime = r.LastModificationTime ?? r.CreationTime,
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
                           Optional = r.Optional,
                           Void = r.Void,
                           InquiryName = r.InquiryId > 0 ? r.Inquiry.Name : "",
                           Total = r.IsVat == true ? (r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total) + r.VatAmount : r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total,
                           CreatedBy = ur.UserName ?? "",
                           CreatorUserId = r.CreatorUserId ?? 0,
                           CompatitorName = r.CompatitorId > 0 ? r.Compatitors.Name : "",
                           ReasonName = r.ReasonId > 0 ? r.LostReason.LeadReasonName : "",
                           ReasonId = r.ReasonId,
                           CompatitorId = r.CompatitorId,
                           ReasonRemark = r.ReasonRemark,
                           PONumber = r.PONumber,
                           Vat = r.Vat,
                           IsVat = r.IsVat,
                           VatAmount = r.VatAmount,
                           OrgDate = r.OrgDate,
                           Revised = r.Revised,
                           RefQNo = r.RefQNo,
                           RFQNo = r.RFQNo,
                           IsApproved = r.IsApproved,
                           OverAllDiscountAmount = r.OverAllDiscountAmount,
                           OverAllDiscountPercentage = r.OverAllDiscountPercentage,
                           Negotiation = r.Negotiation,
                           NegotiationDate = r.NegotiationDate,
                       });
            var quotationCount = await reg.CountAsync();

            var quotationlist = await reg
              .OrderByDescending(p => p.CreationTime)
              .PageBy(input)
              .ToListAsync();
            if (input.Sorting != "RefNo,CompanyName,InquiryName,AttentionName,SalesPersonName,SCreationTime,Void,Total,CreatedBy")
            {
                quotationlist = await reg
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }

            var QuotationListDtos = quotationlist.MapTo<List<QuotationListDto>>();
            return new PagedResultDto<QuotationListDto>(quotationCount, QuotationListDtos);

        }

        public async Task<GetQuotation> GetQuotationForEdit(NullableIdDto input)
        {
            var output = new GetQuotation();
            var query = _quotationRepository
               .GetAll().Where(p => p.Id == input.Id && p.Revised != true);

            if (query.Count() > 0)
            {
                var quotations = (from a in query
                                  select new QuotationListDto
                                  {
                                      Id = a.Id,
                                      RefNo = a.RefNo,
                                      Name = a.Name,
                                      CustomerId = a.CustomerId,
                                      TermsandCondition = a.TermsandCondition,
                                      CompanyName = a.NewCompanys.Name,
                                      StatusName = a.Quotationstatus.Name,
                                      SalesPersonName = a.SalesPerson.UserName,
                                      QuotationStatusId = a.QuotationStatusId,
                                      NewCompanyId = a.NewCompanyId,
                                      SalesPersonId = a.SalesPersonId,
                                      AttentionContactId = a.AttentionContactId,
                                      AttentionName = a.AttentionContactId != null ? (a.AttentionContact.Name + " " + a.AttentionContact.LastName) : "",
                                      Total = a.Optional == true ? a.Total : 0,
                                      TotalFormat = a.Total.ToString("N", new CultureInfo("en-US")),
                                      Email = a.Email,
                                      MobileNumber = a.MobileNumber,
                                      Discount = 0,
                                      LostDate = a.LostDate,
                                      WonDate = a.WonDate,
                                      SubmittedDate = a.SubmittedDate,
                                      Lost = a.Lost,
                                      Won = a.Won,
                                      Submitted = a.Submitted,
                                      InquiryId = a.InquiryId ?? 0,
                                      MileStoneId = a.MileStoneId,
                                      Optional = a.Optional,
                                      InquiryName = a.InquiryId != null ? a.Inquiry.Name : "",
                                      DiscountAmount = 0,
                                      Void = a.Void,
                                      StageId = a.StageId,
                                      CompatitorName = a.CompatitorId > 0 ? a.Compatitors.Name : "",
                                      ReasonName = a.ReasonId > 0 ? a.LostReason.LeadReasonName : "",
                                      ReasonId = a.ReasonId,
                                      CompatitorId = a.CompatitorId,
                                      ReasonRemark = a.ReasonRemark,
                                      PONumber = a.PONumber,
                                      Vat = a.Vat,
                                      VatAmount = a.VatAmount,
                                      IsVat = a.IsVat,
                                      OrgDate = a.OrgDate,
                                      Revised = a.Revised,
                                      RevisionId = a.RevisionId,
                                      RFQNo = a.RFQNo,
                                      RefQNo = a.RefQNo,
                                      IsApproved = a.IsApproved,
                                      OverAllDiscountAmount = a.OverAllDiscountAmount,
                                      OverAllDiscountPercentage = a.OverAllDiscountPercentage,
                                      Negotiation = a.Negotiation,
                                      NegotiationDate = a.NegotiationDate,
                                      PaymentDate = a.PaymentDate,
                                      DiscountEmail = a.DiscountEmail
                                  }).FirstOrDefault();


                var data = (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id where r.Approval == true select r.TotalAmount).Sum();
                if (data > 0)
                {
                    quotations.Total = data;
                    quotations.TotalFormat = data.ToString("N", new CultureInfo("en-US"));
                }
                var data2 = (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id where r.Approval == false select r.OverAllPrice).Sum();
                if (data2 > 0)
                {
                    quotations.Total = quotations.Total + data2;
                    quotations.TotalFormat = (quotations.Total).ToString("N", new CultureInfo("en-US"));
                }

                var TotalValue = quotations.Optional == true ? quotations.Total : (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id select r.OverAllPrice).Sum();

                decimal TotalDiscount = TotalValue - quotations.Total;

                if (TotalValue > 0)
                {
                    quotations.Discount = (float)(Math.Round((TotalDiscount * 100) / TotalValue, 2));
                    quotations.DiscountAmount = TotalDiscount;
                    quotations.DiscountAmountFormat = TotalDiscount.ToString("N", new CultureInfo("en-US"));

                }

                if (quotations.SalesPersonId > 0)
                {
                    byte[] bytes = new byte[0];
                    var Account = (from c in UserManager.Users where c.Id == (long)quotations.SalesPersonId select c).FirstOrDefault();
                    var profilePictureId = Account.ProfilePictureId;
                    quotations.SalespersonImage = "/assets/common/images/default-profile-picture.png";
                    if (profilePictureId != null)
                    {
                        var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                        if (file != null)
                        {
                            bytes = file.Bytes;
                            GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                            quotations.SalespersonImage = "data:image/jpeg;base64," + img.ProfilePicture;
                        }
                    }
                }
                var leadDetail = _LeadDetailRepository.GetAll().Where(p => p.InquiryId == quotations.InquiryId).FirstOrDefault();
                if (leadDetail != null)
                {
                    if (leadDetail.DesignerId > 0)
                    {

                        byte[] bytes = new byte[0];
                        var Account = (from c in UserManager.Users where c.Id == (long)leadDetail.DesignerId select c).FirstOrDefault();
                        quotations.DesignerName = Account.UserName;
                        var profilePictureId = Account.ProfilePictureId;
                        quotations.DesignerImage = "/assets/common/images/default-profile-picture.png";
                        if (profilePictureId != null)
                        {
                            var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                            if (file != null)
                            {
                                bytes = file.Bytes;
                                GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                                quotations.DesignerImage = "data:image/jpeg;base64," + img.ProfilePicture;
                            }
                        }
                    }
                }

                output.quotation = quotations.MapTo<QuotationListDto>();
            }
            //else
            //{

            //    throw new UserFriendlyException("Ooops!", "The requested Quotation Id is not valid '" + input.Id + "'...");

            //}
            return output;
        }
        public async Task<GetQuotation> GetQuotationRevisionForEdit(NullableIdDto input)
        {
            var output = new GetQuotation();
            var query = _quotationRepository
               .GetAll().Where(p => p.Id == input.Id);

            var quotations = (from a in query
                              select new QuotationListDto
                              {
                                  Id = a.Id,
                                  RefNo = a.RefNo,
                                  Name = a.Name,
                                  CustomerId = a.CustomerId,
                                  TermsandCondition = a.TermsandCondition,
                                  CompanyName = a.NewCompanys.Name,
                                  StatusName = a.Quotationstatus.Name,
                                  SalesPersonName = a.SalesPerson.UserName,
                                  QuotationStatusId = a.QuotationStatusId,
                                  NewCompanyId = a.NewCompanyId,
                                  SalesPersonId = a.SalesPersonId,
                                  AttentionContactId = a.AttentionContactId,
                                  AttentionName = a.AttentionContactId != null ? (a.AttentionContact.Name + " " + a.AttentionContact.LastName) : "",
                                  Total = 0,
                                  TotalFormat = a.Total.ToString("N", new CultureInfo("en-US")),
                                  Email = a.Email,
                                  MobileNumber = a.MobileNumber,
                                  Discount = 0,
                                  LostDate = a.LostDate,
                                  WonDate = a.WonDate,
                                  SubmittedDate = a.SubmittedDate,
                                  Lost = a.Lost,
                                  Won = a.Won,
                                  Submitted = a.Submitted,
                                  InquiryId = a.InquiryId ?? 0,
                                  MileStoneId = a.MileStoneId,
                                  Optional = a.Optional,
                                  InquiryName = a.InquiryId != null ? a.Inquiry.Name : "",
                                  DiscountAmount = 0,
                                  Void = a.Void,
                                  StageId = a.StageId,
                                  CompatitorName = a.CompatitorId > 0 ? a.Compatitors.Name : "",
                                  ReasonName = a.ReasonId > 0 ? a.LostReason.LeadReasonName : "",
                                  ReasonId = a.ReasonId,
                                  CompatitorId = a.CompatitorId,
                                  ReasonRemark = a.ReasonRemark,
                                  PONumber = a.PONumber,
                                  Vat = a.Vat,
                                  VatAmount = a.VatAmount,
                                  IsVat = a.IsVat,
                                  OrgDate = a.OrgDate,
                                  Revised = a.Revised,
                                  RevisionId = a.RevisionId,
                                  RFQNo = a.RFQNo,
                                  RefQNo = a.RefQNo,
                                  IsApproved = a.IsApproved,
                                  OverAllDiscountAmount = a.OverAllDiscountAmount,
                                  OverAllDiscountPercentage = a.OverAllDiscountPercentage,
                                  Negotiation = a.Negotiation,
                                  NegotiationDate = a.NegotiationDate,
                                  DiscountEmail = a.DiscountEmail
                              }).FirstOrDefault();

            var data = (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id where r.Approval == true select r.TotalAmount).Sum();
            if (data > 0)
            {
                quotations.Total = data;
                quotations.TotalFormat = data.ToString("N", new CultureInfo("en-US"));
            }
            var data2 = (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id where r.Approval == false select r.OverAllPrice).Sum();
            if (data2 > 0)
            {
                quotations.Total = quotations.Total + data2;
                quotations.TotalFormat = (quotations.Total).ToString("N", new CultureInfo("en-US"));
            }

            var TotalValue = (from r in _quotationProductRepository.GetAll() where r.QuotationId == input.Id select r.OverAllPrice).Sum();

            decimal TotalDiscount = TotalValue - quotations.Total;

            if (TotalValue > 0)
            {
                quotations.Discount = (float)(Math.Round((TotalDiscount * 100) / TotalValue, 2));
                quotations.DiscountAmount = TotalDiscount;
                quotations.DiscountAmountFormat = TotalDiscount.ToString("N", new CultureInfo("en-US"));

            }

            if (quotations.SalesPersonId > 0)
            {
                byte[] bytes = new byte[0];
                var Account = (from c in UserManager.Users where c.Id == (long)quotations.SalesPersonId select c).FirstOrDefault();
                var profilePictureId = Account.ProfilePictureId;
                quotations.SalespersonImage = "/assets/common/images/default-profile-picture.png";
                if (profilePictureId != null)
                {
                    var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                    if (file != null)
                    {
                        bytes = file.Bytes;
                        GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                        quotations.SalespersonImage = "data:image/jpeg;base64," + img.ProfilePicture;
                    }
                }
            }
            var leadDetail = _LeadDetailRepository.GetAll().Where(p => p.InquiryId == quotations.InquiryId).FirstOrDefault();
            if (leadDetail != null)
            {
                if (leadDetail.DesignerId > 0)
                {

                    byte[] bytes = new byte[0];
                    var Account = (from c in UserManager.Users where c.Id == (long)leadDetail.DesignerId select c).FirstOrDefault();
                    quotations.DesignerName = Account.UserName;
                    var profilePictureId = Account.ProfilePictureId;
                    quotations.DesignerImage = "/assets/common/images/default-profile-picture.png";
                    if (profilePictureId != null)
                    {
                        var file = await _binaryObjectManager.GetOrNullAsync((Guid)profilePictureId);
                        if (file != null)
                        {
                            bytes = file.Bytes;
                            GetProfilePictureOutput img = new GetProfilePictureOutput(Convert.ToBase64String(bytes));
                            quotations.DesignerImage = "data:image/jpeg;base64," + img.ProfilePicture;
                        }
                    }
                }
            }
            output.quotation = quotations.MapTo<QuotationListDto>();

            return output;
        }
        public async Task<int> CreateOrUpdateQuotation(CreateQuotationInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotation(input);
            }
            else
            {
                input.Id = CreateQuotation(input);
            }
            return input.Id;
        }
        public int CreateQuotation(CreateQuotationInput input)
        {
            int id = 0;
            if (input.InquiryId > 0)
            {
                var date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                if (input.Optional != true)
                {
                    input.MileStoneId = 5;
                    input.StageId = 2;
                }
                if (input.Submitted == true)
                {
                    input.SubmittedDate = DateTime.Now;
                }
                var discription = _discountRepository.GetAll().Where(p => p.Id == 1).FirstOrDefault();
                var countryCode = (from r in _NewAddressInfoRepository.GetAll().Where(p => p.NewCompanyId == input.NewCompanyId) join s in _CityRepository.GetAll() on r.CityId equals s.Id select s.Country.CountryCode).FirstOrDefault();
                var quotation = input.MapTo<Quotation>();
                if (input.AttentionContactId > 0)
                {
                    var email = _NewContactInfoRepository.GetAll().Where(c => c.NewContacId == input.AttentionContactId && c.NewInfoTypeId == 4).FirstOrDefault();
                    var mbil = _NewContactInfoRepository.GetAll().Where(c => c.NewContacId == input.AttentionContactId && c.NewInfoTypeId == 7).FirstOrDefault();
                    if (email != null)
                    {
                        quotation.Email = email.InfoData;
                    }
                    if (mbil != null)
                    {
                        quotation.MobileNumber = mbil.InfoData;
                    }
                    quotation.TermsandCondition = discription.QuotationDescription;
                    quotation.Vat = discription.Vat;
                    quotation.IsVat = true;
                    quotation.DiscountEmail = false;
                    quotation.VatAmount = input.Total > 0 ? (input.Total * discription.Vat) / 100 : 0;
                    quotation.OrgDate = DateTime.Now;
                    quotation.RevisionId = null;
                }


                id = _quotationRepository.InsertAndGetId(quotation);
                var quot = _quotationRepository.GetAll().Where(p => p.Id == id).FirstOrDefault();
                var val = _quotationRepository.GetAll().Where(p => p.CreationTime.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) == date).Count() + 1;
                quot.RefNo = "Q" + date + val.ToString("000") + "-R0";
                var val2 = _quotationRepository.GetAll().Where(p => p.RefNo == quot.RefNo).Count();

                while (val2 > 0)
                {
                    val = val + 1;
                    quot.RefNo = "Q" + date + val.ToString("000") + "-R0";
                    val2 = _quotationRepository.GetAll().Where(p => p.RefNo == quot.RefNo).Count();
                }

                quot.RevisionId = id;
                _quotationRepository.UpdateAsync(quot);


                if (input.InquiryId > 0)
                {
                    UpdateEnq((int)input.InquiryId);
                }
            }
            return id;
        }
        public void UpdateQuotationOptional(int Id)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_UpdateQuotationOptional", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QuotationId", SqlDbType.VarChar).Value = Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        void UpdateEnq(int Id)
        {
            var inquiry = (from r in _inquiryRepository.GetAll() where r.Id == Id select r).FirstOrDefault();
            inquiry.MileStoneId = 5;
            inquiry.StatusId = 2;
            var inquirys = inquiry.MapTo<Inquiry>();
            _inquiryRepository.UpdateAsync(inquirys);

            var enquiryDetail = (from r in _enquiryDetailRepository.GetAll() where r.InquiryId == Id && r.IsDeleted == false select r).FirstOrDefault();
            if (enquiryDetail.LastActivity == null)
            {
                enquiryDetail.LastActivity = DateTime.Now.AddDays(1);
            }
            if(enquiryDetail.ClosureDate == null)
            {
                enquiryDetail.ClosureDate = DateTime.Now.AddDays(1);
            }
            var linkcomp = enquiryDetail.MapTo<EnquiryDetail>();
            _enquiryDetailRepository.UpdateAsync(linkcomp);
        }
        public async Task UpdateQuotation(CreateQuotationInput input)
        {
            var oldquotations = _quotationRepository.GetAll().AsNoTracking().Where(u => u.Id == input.Id).FirstOrDefault();
            var wonStage = _enqStatusRepository.GetAll().Where(p => p.EnqStatusName == "OE Processing").Select(c => c.Id).FirstOrDefault();
            var lostStage = _enqStatusRepository.GetAll().Where(p => p.EnqStatusName == "Lost").Select(c => c.Id).FirstOrDefault();

            var stage = _StageDetailRepository.GetAll().Where(p => p.MileStoneId == input.MileStoneId && p.StageId == input.StageId).FirstOrDefault();
            if (stage == null)
            {
                var stageId = _StageDetailRepository.GetAll().Where(p => p.MileStoneId == input.MileStoneId).FirstOrDefault();

                if (stageId != null)
                {
                    input.StageId = stageId.StageId;
                }
            }

            var vallu = _quotationRepository
         .GetAll().Where(u => u.RefNo == input.RefNo && u.Id != input.Id).FirstOrDefault();

            if (input.Optional != true)
            {
                var quotationProductTotals1 = (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.Id) where r.Approval == true select r.TotalAmount).Sum();
                var quotationProductTotals2 = (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.Id) where r.Approval == false select r.OverAllPrice).Sum();
                input.Total = quotationProductTotals1 + quotationProductTotals2;
            }

            if (oldquotations.QuotationStatusId == 1 && input.QuotationStatusId == 2)
            {
                var quotationprod = _quotationProductRepository.GetAll().Where(u => u.QuotationId == input.Id).ToList();
                if (quotationprod.Count > 0 || input.Optional == true)
                {
                    input.MileStoneId = 6;
                    input.StageId = 4;
                    var quot = await _quotationRepository.GetAsync(input.Id);
                    quot.LastModificationTime = DateTime.Now;
                    ObjectMapper.Map(input, quot);


                    if (vallu == null)
                    {
                        await _quotationRepository.UpdateAsync(quot);

                        if (input.NextActivity != null)
                        {
                            var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == quot.InquiryId).FirstOrDefault();
                            var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                            enqdetail.LastActivity = input.NextActivity;
                            await _enquiryDetailRepository.UpdateAsync(enqdetail);

                            EnqActCreate actinput = new EnqActCreate()
                            {
                                EnquiryId = (int)quot.InquiryId,
                                PreviousStatus = "Pre-Quote(Initiate Contact)",
                                CurrentStatus = "Quoted(Quote 1 of Many)",
                                ActivityId = 7,
                                Title = "Status Update",
                                Message = "Ticket Status Updated Successfully"
                            };
                            var Activity = actinput.MapTo<AcitivityTrack>();
                            await _acitivityTrackRepository.InsertAsync(Activity);
                        }


                    }
                    else
                    {
                        throw new UserFriendlyException("Ooops!", "Duplicate Data Occured ");
                    }


                }
                else
                {

                    throw new UserFriendlyException("Ooops!", "There is no product in the Quotation");
                }
            }

            else
            {
                var oldquotation = _quotationRepository.GetAll().AsNoTracking().Where(u => u.Id == input.Id).FirstOrDefault();

                var val = _quotationRepository
                .GetAll().Where(u => u.RefNo == input.RefNo && u.Id != input.Id && u.PONumber != null && u.PONumber == input.PONumber).FirstOrDefault();

                if (oldquotation.Lost == false && input.Lost == true)
                {
                    input.LostDate = DateTime.Now;
                    input.WonDate = null;
                    input.PONumber = null;
                    input.MileStoneId = 10;
                    input.StageId = lostStage;
                }
                else if (input.Lost == false)
                {
                    input.LostDate = null;
                    input.ReasonId = null;
                    input.CompatitorId = null;
                    input.ReasonRemark = null;
                }

                if (oldquotation.Submitted == false && input.Submitted == true)
                {
                    input.SubmittedDate = DateTime.Now;
                }
                else if (input.Submitted == false)
                    input.SubmittedDate = null;

                if (oldquotation.Negotiation == false && input.Negotiation == true)
                {
                    input.NegotiationDate = DateTime.Now;
                    input.StageId = 9;
                    input.MileStoneId = 9;

                }

                if (oldquotation.Won == false && input.Won == true)
                {
                    input.WonDate = DateTime.Now;
                    input.LostDate = null;
                    input.ReasonId = null;
                    input.CompatitorId = null;
                    input.ReasonRemark = null;
                    input.MileStoneId = 10;
                    input.StageId = wonStage;
                }
                else if (input.Won == false)
                {
                    input.WonDate = null;
                    input.PONumber = null;
                }


                if (oldquotations.QuotationStatusId == 2 && input.QuotationStatusId == 1)
                {
                    input.MileStoneId = 5;
                    input.StageId = null;
                    input.Submitted = false;
                    input.Won = false;
                    input.Lost = false;
                    input.SubmittedDate = null;
                    input.WonDate = null;
                    input.LostDate = null;
                }
                var quot = await _quotationRepository.GetAsync(input.Id);
                quot.LastModificationTime = DateTime.Now;
                ObjectMapper.Map(input, quot);

                if (val == null)
                {
                    if (oldquotation.QuotationStatusId != 2 && input.QuotationStatusId == 2)
                    {
                        var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == oldquotation.InquiryId).FirstOrDefault();
                        var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                        enqdetail.LastActivity = input.NextActivity;
                        await _enquiryDetailRepository.UpdateAsync(enqdetail);
                    }

                    await _quotationRepository.UpdateAsync(quot);
                    if (quot.QuotationStatusId == 5 || quot.QuotationStatusId == 4)
                    {

                        await _quotationRepository.UpdateAsync(quot);
                        ConnectionAppService db = new ConnectionAppService();
                        using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("Sp_VoidQuotation", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@QuotationId", SqlDbType.Int).Value = input.Id;
                                cmd.Parameters.Add("@TableId", SqlDbType.Int).Value = quot.QuotationStatusId;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Po Number '" + input.PONumber);
                }

            }

        }
        public async Task UpdateQuotationWonorLost(UpdateQuotationInput input)
        {
            var quot = await _quotationRepository.GetAsync(input.Id);
            quot.LastModificationTime = DateTime.Now;
            quot.MileStoneId = input.MileStoneId;
            quot.StageId = input.StageId;
            quot.PaymentDate = input.PaymentDate;
            if (input.Won == true)
            {
                quot.WonDate = DateTime.Now;
                quot.Won = true;
                quot.PONumber = input.PONumber;
                quot.QuotationStatusId = 4;
            }
            if (input.Lost == true)
            {
                quot.LostDate = DateTime.Now;
                quot.ReasonId = input.ReasonId;
                quot.CompatitorId = input.CompatitorId;
                quot.Lost = true;
                quot.ReasonRemark = input.ReasonRemark;
                quot.QuotationStatusId = 5;
            }
            if (input.Lost == false)
            {
                quot.LostDate = null;
                quot.ReasonId = null;
                quot.CompatitorId = null;
                quot.Lost = false;
                quot.ReasonRemark = null;
            }
            if (input.Won == false)
            {
                quot.WonDate = null;
                quot.Won = false;
                quot.PONumber = null;
            }
            var val = 0;
            if (quot.QuotationStatusId == 5 || quot.QuotationStatusId == 4)
            {
                val = _quotationRepository
              .GetAll().Where(u => u.Id != input.Id && u.PONumber != null && u.PONumber == input.PONumber).Count();

                if (val > 0)
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Po Number '" + input.PONumber);
                }
                else
                {
                    await _quotationRepository.UpdateAsync(quot);
                    ConnectionAppService db = new ConnectionAppService();
                    using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Sp_VoidQuotation", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@QuotationId", SqlDbType.Int).Value = input.Id;
                            cmd.Parameters.Add("@TableId", SqlDbType.Int).Value = quot.QuotationStatusId;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
           
        }
        public async Task<GetSection> GetSectionForEdit(NullableIdDto input)
        {
            var output = new GetSection();
            var query = _sectionRepository
               .GetAll().Where(p => p.Id == input.Id);


            var sections = (from a in query
                            select new SectionListDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                QuotationId = a.QuotationId,
                                RefNo = a.quotation.RefNo,
                            }).FirstOrDefault();

            output.section = sections.MapTo<SectionListDto>();


            return output;
        }
        public async Task CreateOrUpdateSection(CreateSectionInput input)
        {
            if (input.Id != 0)
            {
                await UpdateSection(input);
            }
            else
            {
                await CreateSection(input);
            }
        }
        public async Task CreateSection(CreateSectionInput input)
        {
            var section = input.MapTo<Section>();
            var val = _sectionRepository
             .GetAll().Where(u => u.Name == input.Name && u.Id == input.QuotationId).FirstOrDefault();

            if (val == null)
            {
                await _sectionRepository.InsertAsync(section);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "'...");
            }
        }
        public async Task UpdateSection(CreateSectionInput input)
        {
            var section = input.MapTo<Section>();


            var val = _sectionRepository
            .GetAll().Where(u => u.Name == input.Name && u.Id != input.Id && u.Id == input.QuotationId).FirstOrDefault();

            if (val == null)
            {
                await _sectionRepository.UpdateAsync(section);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Name '" + input.Name + "' ...");
            }

        }
        public async Task<Array> GetQuotationProduct(GetQuotationProductInput input)
        {
            var sectionGroup = (from r in _sectionRepository.GetAll() where r.QuotationId == input.Id select r).ToArray();
            var query = _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.Id);

            var reg = (from r in query
                       select new QuotationProductListDto
                       {
                           Id = r.Id,
                           ProductCode = r.ProductId > 0 ? r.product.ProductCode : r.ProductCode,
                           Quantity = r.Quantity,
                           Discount = r.Discount,
                           UnitOfMeasurement = r.UnitOfMeasurement,
                           UnitOfPrice = r.UnitOfPrice,
                           SUnitOfPrice = r.UnitOfPrice.ToString("N", new CultureInfo("en-US")),
                           TotalAmount = r.Approval == true ? r.TotalAmount : r.OverAllPrice,
                           STotalAmount = (r.Approval == true ? r.TotalAmount : r.OverAllPrice).ToString("N", new CultureInfo("en-US")),
                           ProductId = r.ProductId,
                           ProductName = r.ProductId != null ? r.product.ProductName : "",
                           QuotationId = r.QuotationId,
                           RefNo = r.QuotationId > 0 ? r.quotation.RefNo : "",
                           SectionId = r.SectionId,
                           SectionName = r.SectionId != null ? r.section.Name : "",
                           Locked = r.Locked,
                           Approval = r.Approval,
                           Discountable = r.Discountable,
                           CreationTime = r.CreationTime,
                           TemporaryProductId = r.TemporaryProductId
                       }).ToList();

            foreach (var data in reg)
            {
                if (data.TemporaryProductId > 0)
                {
                    var TempImage = _TempProductImageRepository.GetAll().Where(p => p.TemporaryProductId == data.TemporaryProductId).FirstOrDefault();
                    if (TempImage != null)
                    {
                        data.ImageUrl = TempImage.ImageUrl;
                    }
                }
                else if (data.ProductId > 0)
                {
                    var Image = _ProductImageRepository.GetAll().Where(q => q.ProductId == data.ProductId).FirstOrDefault();
                    if (Image != null)
                    {
                        data.ImageUrl = Image.ImageUrl;
                    }
                }

            }


            var QuotationProductListDtos = reg.MapTo<List<QuotationProductListDto>>();


            var SubListout = new List<QuotationProductOutput>();


            foreach (var newsts in sectionGroup)
            {
                var subtotal = (from r in reg where r.SectionId == newsts.Id select r.TotalAmount).Sum();
                SubListout.Add(new QuotationProductOutput {
                    name = newsts.Name,
                    subtotal = subtotal,
                    subtotalFormat = subtotal.ToString("N", new CultureInfo("en-US")),
                    GetQuotationProduct = (from r in reg where r.SectionId == newsts.Id select r).OrderBy(p => p.CreationTime).ToArray()
                });
            }

            return SubListout.ToArray();

        }
        public async Task<GetQuotationProduct> GetQuotationProductForEdit(NullableIdDto input)
        {
            var output = new GetQuotationProduct();
            var query = _quotationProductRepository
               .GetAll().Where(p => p.Id == input.Id);


            var products = (from a in query
                            select new QuotationProductListDto
                            {
                                Id = a.Id,
                                ProductCode = a.ProductId != null ? a.product.ProductCode : a.TemporaryCode,
                                Quantity = a.Quantity,
                                Discount = a.Discount,
                                UnitOfMeasurement = a.UnitOfMeasurement,
                                UnitOfPrice = a.UnitOfPrice,
                                TotalAmount = a.TotalAmount,
                                RefNo = a.quotation.RefNo,
                                ProductName = a.product.ProductSpecifications.Name,
                                SectionName = a.section.Name,
                                SectionId = a.SectionId,
                                ProductId = a.ProductId,
                                TemporaryProductId = a.TemporaryProductId,
                                QuotationId = a.QuotationId,
                                Discountable = a.Discountable,
                                Locked = a.Locked,
                                OverAllDiscount = a.OverAllDiscount,
                                OverAllPrice = a.OverAllPrice,
                                Approval = a.Approval,
                                TemporaryCode = a.TemporaryCode,
                                ImageUrl = "",
                                CreationTime = a.CreationTime
                            }).FirstOrDefault();

            if (products.Locked == true && products.TemporaryProductId > 0)
            {
                var data = _TempProductRepository.GetAll().Where(p => p.Id == products.TemporaryProductId).FirstOrDefault();
                products.UnitOfPrice = (decimal)(data.Price ?? 0);
            }

            if (products.ProductId > 0)
            {
                var imageqry = _productImageUrlRepository.GetAll().Where(p => p.ProductId == products.ProductId).FirstOrDefault();

                if (imageqry != null)
                    products.ImageUrl = imageqry.ImageUrl;
            }

            if (products.TemporaryProductId > 0)
            {
                var tempImage = _TempProductImageRepository.GetAll().Where(q => q.TemporaryProductId == products.TemporaryProductId).FirstOrDefault();
                if (tempImage != null)
                {
                    products.ImageUrl = tempImage.ImageUrl;
                }
            }

            output.product = products.MapTo<QuotationProductListDto>();


            return output;
        }
        public async Task CreateOrUpdateQuotationProduct(QuotationProductInput input)
        {
            if (input.Id != 0)
            {
                await UpdateQuotationProduct(input);
            }
            else
            {
                await CreateQuotationProduct(input);
            }
        }
        public async Task CreateQuotationProduct(QuotationProductInput input)
        {
            decimal quotationProductTotals = 0;
            var product = input.MapTo<QuotationProduct>();
            var val = _quotationProductRepository
             .GetAll().Where(u => u.ProductCode == input.ProductCode && u.QuotationId == input.QuotationId && u.SectionId == input.SectionId).FirstOrDefault();

            if (val == null)
            {
                await _quotationProductRepository.InsertAsync(product);

                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId && p.Approval == true) select r.TotalAmount).Sum();
                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId && p.Approval == false) select r.OverAllPrice).Sum();

                var quotation = _quotationRepository.GetAll().Where(q => q.Id == input.QuotationId).FirstOrDefault();
                quotation.Total = quotationProductTotals + (input.Approval == true ? input.TotalAmount : input.OverAllPrice);
                if (quotation.Total > 0 && quotation.Vat > 0 && quotation.IsVat == true)
                {
                    quotation.VatAmount = (quotation.Total * quotation.Vat) / 100;
                }
                if (input.Approval == false)
                {
                    quotation.DiscountEmail = true;
                }
                await _quotationRepository.UpdateAsync(quotation);

            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductCode...");
            }
        }
        public async Task UpdateQuotationProduct(QuotationProductInput input)
        {
            decimal quotationProductTotals = 0;
            input.TemporaryCode = input.ProductCode;
            bool qapp = _quotationProductRepository.GetAll().Where(p => p.Id == input.Id).Select(p => p.Approval).FirstOrDefault();
            int qpcount = _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId && p.Approval == false).Count();

            var val = _quotationProductRepository
            .GetAll().Where(u => u.ProductCode == input.ProductCode && u.QuotationId == input.QuotationId && u.SectionId == input.SectionId && u.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                var product = await _quotationProductRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, product);
                await _quotationProductRepository.UpdateAsync(product);

                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId && p.Id != input.Id && p.Approval == true) select r.TotalAmount).Sum();
                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.QuotationId && p.Id != input.Id && p.Approval == false) select r.OverAllPrice).Sum();

                var quotation = _quotationRepository.GetAll().Where(q => q.Id == input.QuotationId).FirstOrDefault();
                var data =
                quotation.Total = quotationProductTotals + (input.Approval == true ? input.TotalAmount : input.OverAllPrice);
                if (quotation.Total > 0 && quotation.Vat > 0 && quotation.IsVat == true)
                {
                    quotation.VatAmount = (quotation.Total * quotation.Vat) / 100;
                }
                if (input.Approval == false && qapp == true)
                {
                    quotation.DiscountEmail = true;
                }

                if (input.Approval == true && qpcount < 2)
                {
                    quotation.DiscountEmail = false;
                }
                await _quotationRepository.UpdateAsync(quotation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductCode...");
            }

        }
        public async Task DeleteQuotationProduct(EntityDto input)
        {
            var query = (from r in _quotationProductRepository.GetAll().Where(p => p.Id == input.Id) select r).FirstOrDefault();
            int qpcount = _quotationProductRepository.GetAll().Where(p => p.QuotationId == query.QuotationId && p.Approval == false).Count();
            await _quotationProductRepository.DeleteAsync(input.Id);
            decimal quotationproductamount = 0;
            var quotation = _quotationRepository.GetAll().Where(q => q.Id == query.QuotationId).FirstOrDefault();
            if (query.Approval == true)
            {
                quotationproductamount = query.TotalAmount;
            }
            else
            {
                quotationproductamount = query.OverAllPrice;
            }
            quotation.Total = quotation.Total - quotationproductamount;
            if (quotation.Total > 0 && quotation.Vat > 0 && quotation.IsVat == true)
            {
                quotation.VatAmount = (quotation.Total * quotation.Vat) / 100;
            }
            if (query.Approval == false && qpcount < 2)
            {
                quotation.DiscountEmail = false;
            }
            await _quotationRepository.UpdateAsync(quotation);
        }
        public async Task DeleteSection(EntityDto input)
        {
            await _sectionRepository.DeleteAsync(input.Id);
        }
        public async Task GetDeleteQuotation(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 8);
                sqlComm.Parameters.AddWithValue("@Id", input.Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public async Task GetProductImport(ImportQuotationInput input)
        {

            ConnectionAppService db = new ConnectionAppService();
            try
            {
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ReadFile_QuotationProduct", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Path", SqlDbType.VarChar).Value = input.File;
                        cmd.Parameters.Add("@File", SqlDbType.VarChar).Value = input.FileName;
                        cmd.Parameters.Add("@QuotationId", SqlDbType.Int).Value = input.QuotationId;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                throw new UserFriendlyException("Ooops!", " Imported Text File is Not Supported Format");
            }
        }
        public async Task<PagedResultDto<ImportHistoryList>> GetImportHistory(GetImportHistoryInput input)
        {
            var query = _ImportHistoryRepository.GetAll().Where(p => p.QuotationId == input.QuotationId);

            var ImportHistory = (from a in query
                                 select new ImportHistoryList
                                 {
                                     Id = a.Id,
                                     FileName = a.FileName,
                                     QuotationId = a.QuotationId,
                                     ProductCode = a.ProductCode,
                                     Quantity = a.Quantity,
                                     SectionName = a.SectionName,
                                     Status = a.Status,
                                     CreationTime = a.CreationTime.ToString(),
                                 });

            var ImportHistoryCount = await ImportHistory.CountAsync();
            var importHistorylist = await ImportHistory
                                         .OrderBy(input.Sorting)
                                         .PageBy(input)
                                         .ToListAsync();

            var ImportHistoryList = importHistorylist.MapTo<List<ImportHistoryList>>();

            return new PagedResultDto<ImportHistoryList>(ImportHistoryCount, ImportHistoryList);
        }
        public async Task GetQuotationProductUnlock(ProductLinkInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_LinkQuotationProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TempProductId", SqlDbType.VarChar).Value = input.TempProductId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task GetExchangeProduct(ExchangeInput input)
        {
            var query = _quotationProductRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var product = _productRepository.GetAll().Where(r => r.Id == input.ProdId).FirstOrDefault();

            if (product != null)
            {
                query.ProductId = product.Id;
                query.ProductCode = product.ProductCode;
                query.Locked = false;
                await _quotationProductRepository.UpdateAsync(query);
            }
            else
            {
                throw new UserFriendlyException("Ooops!, Product does not exists");
            }

        }
        public async Task GetApproveProduct(EntityDto input)
        {
            var query = _quotationProductRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            var dismcount = _quotationProductRepository.GetAll().Where(p => p.QuotationId == query.QuotationId && p.Approval == false).Count();

            if (query != null)
            {
                query.Approval = true;
                //query.Locked = false;
                await _quotationProductRepository.UpdateAsync(query);

                decimal quotationProductTotals = 0;
                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == query.QuotationId && p.Approval == true && p.Id != input.Id) select r.TotalAmount).Sum();
                quotationProductTotals = quotationProductTotals + (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == query.QuotationId && p.Approval == false && p.Id != input.Id) select r.OverAllPrice).Sum();

                var quotation = _quotationRepository.GetAll().Where(q => q.Id == query.QuotationId).FirstOrDefault();
                quotation.Total = quotationProductTotals + query.TotalAmount;
                if (dismcount < 2)
                {
                    quotation.DiscountEmail = false;
                }
                if (quotation.Total > 0 && quotation.Vat > 0 && quotation.IsVat == true)
                {
                    quotation.VatAmount = (quotation.Total * quotation.Vat) / 100;
                }
                await _quotationRepository.UpdateAsync(quotation);
            }
            else
            {
                throw new UserFriendlyException("Ooops!, Product does not exists");
            }

        }
        public bool CheckQuotationIsOptional(NullableIdDto input)
        {
            var data = false;
            var query = _quotationRepository.GetAll().Where(p => p.InquiryId == input.Id).ToList();

            if (query.Count() > 0)
            {
                data = true;
            }

            return data;
        }
        public ListResultDto<QuotationListDto> GetInquiryWiseQuotation(NullableIdDto input)
        {
            var query = _quotationRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var reg = (from r in _quotationRepository.GetAll() where r.InquiryId == query.InquiryId && r.Id != input.Id && r.Revised == false
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
                           CreationTime = r.LastModificationTime ?? r.CreationTime,
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
                           Optional = r.Optional,
                           Void = r.Void,
                           InquiryName = r.InquiryId > 0 ? r.Inquiry.Name : "",
                           Total = r.Total,
                           CreatedBy = "",
                           CreatorUserId = r.CreatorUserId ?? 0
                       });

            var quotationlist = reg.ToList();
            return new ListResultDto<QuotationListDto>(quotationlist.MapTo<List<QuotationListDto>>());
        }
        public async Task<PagedResultDto<QuotationListDto>> GetRevisedQuotation(NullableIdDto input)
        {
            var RevisionId = (from r in _quotationRepository.GetAll() where r.Id == input.Id select r.RevisionId).FirstOrDefault();
            var query = _quotationRepository.GetAll().Where(r => r.Revised == true && r.RevisionId == RevisionId);
            var reg = (from r in query
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
                           CreationTime = r.LastModificationTime ?? r.CreationTime,
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
                           Optional = r.Optional,
                           Void = r.Void,
                           InquiryName = r.InquiryId > 0 ? r.Inquiry.Name : "",
                           Total = r.Total,
                           CreatedBy = "",
                           CreatorUserId = r.CreatorUserId ?? 0,
                           CompatitorName = r.CompatitorId > 0 ? r.Compatitors.Name : "",
                           ReasonName = r.ReasonId > 0 ? r.LostReason.LeadReasonName : "",
                           ReasonId = r.ReasonId,
                           CompatitorId = r.CompatitorId,
                           ReasonRemark = r.ReasonRemark,
                           PONumber = r.PONumber,
                           Vat = r.Vat,
                           VatAmount = r.VatAmount,
                           OrgDate = r.OrgDate,
                           Revised = r.Revised
                       });


            var quotationCount = await reg.CountAsync();

            var quotationlist = await reg
              .OrderByDescending(p => p.CreationTime)
              .ToListAsync();

            foreach (var data in quotationlist)
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
            var QuotationListDtos = quotationlist.MapTo<List<QuotationListDto>>();


            return new PagedResultDto<QuotationListDto>(quotationCount, QuotationListDtos);
        }
        public async Task<int> QuotationRevision(QuotationRevisionInput input)
        {
            int quotationId = 0;
            long userid = (long)AbpSession.UserId;
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_QuotationRevision", con);
                sqlComm.Parameters.AddWithValue("@QuotationId", input.Id);
                sqlComm.Parameters.AddWithValue("@UserId", userid);
                sqlComm.Parameters.AddWithValue("@NextActivity", input.NextActivity);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
                var tquotationId = ds.Rows[0]["Id"].ToString();
                quotationId = int.Parse(tquotationId);
            }
            return quotationId;
        }
        public async Task UpdateQuotationVatAmount(NullableIdDto input)
        {
            var quotationProductTotals = (from r in _quotationProductRepository.GetAll().Where(p => p.QuotationId == input.Id) select r.TotalAmount).Sum();

            var quotation = _quotationRepository.GetAll().Where(q => q.Id == input.Id).FirstOrDefault();

            quotation.Total = quotationProductTotals;

            if (quotation.Total > 0 && quotation.Vat > 0 && quotation.IsVat == true)
            {
                quotation.VatAmount = (quotation.Total * quotation.Vat) / 100;
            }

            await _quotationRepository.UpdateAsync(quotation);
        }
        public async Task SetDiscountForProducts(int QuotationId, Decimal NewDiscount)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_SetDiscountForQuotationProducts", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", QuotationId);
                sqlComm.Parameters.AddWithValue("@NewDiscount", NewDiscount);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public async Task<FileDto> GetQuotationToExcel()
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
                         where q.Revised == false && q.SalesPersonId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
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
                         where q.Revised == false && team.SalesManagerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Designer")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.DesignerId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else if (userrole.DisplayName == "Sales Coordinator")
            {
                query = (from q in _quotationRepository.GetAll()
                         join enq in _inquiryRepository.GetAll() on q.InquiryId equals enq.Id
                         join leadDetail in _LeadDetailRepository.GetAll() on enq.Id equals leadDetail.InquiryId
                         where q.Revised == false && leadDetail.CoordinatorId == userid && q.IsClosed != true && q.Archieved != true && q.Void != true
                         select q
                        );
            }
            else
            {
                query = _quotationRepository.GetAll().Where(p => p.Revised == false);

            }
            var reg = (from r in query
                       join ur in UserManager.Users on r.CreatorUserId equals ur.Id

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
                           CreationTime = r.LastModificationTime ?? r.CreationTime,
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
                           Optional = r.Optional,
                           Void = r.Void,
                           InquiryName = r.InquiryId > 0 ? r.Inquiry.Name : "",
                           Total = r.IsVat == true ? (r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total) + r.VatAmount : r.Negotiation == true ? r.Total - r.OverAllDiscountAmount : r.Total,
                           CreatedBy = ur.UserName ?? "",
                           CreatorUserId = r.CreatorUserId ?? 0,
                           CompatitorName = r.CompatitorId > 0 ? r.Compatitors.Name : "",
                           ReasonName = r.ReasonId > 0 ? r.LostReason.LeadReasonName : "",
                           ReasonId = r.ReasonId,
                           CompatitorId = r.CompatitorId,
                           ReasonRemark = r.ReasonRemark,
                           PONumber = r.PONumber,
                           Vat = r.Vat,
                           IsVat = r.IsVat,
                           VatAmount = r.VatAmount,
                           OrgDate = r.OrgDate,
                           Revised = r.Revised,
                           RefQNo = r.RefQNo,
                           RFQNo = r.RFQNo,
                           IsApproved = r.IsApproved,
                           OverAllDiscountAmount = r.OverAllDiscountAmount,
                           OverAllDiscountPercentage = r.OverAllDiscountPercentage,
                           Negotiation = r.Negotiation,
                           NegotiationDate = r.NegotiationDate
                       });
            var quotationCount = await reg.CountAsync();

             var quotationlist = await reg
               .OrderByDescending(p => p.CreationTime)
               .ToListAsync();

            var QuotationListDtos = quotationlist.MapTo<List<QuotationListDto>>();

            return _quotationListExcelExporter.ExportToFile(QuotationListDtos);

        }
        public async Task<PagedResultDto<QuotationInquiryFilter>> GetQuotationInquiryFilter(FilterByDto input)
        {

            string userid = input.UserIds;

            if (userid == null)
            {
                var userrole = (from c in UserManager.Users
                                join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                                join role in _roleManager.Roles on urole.RoleId equals role.Id
                                where urole.UserId == AbpSession.UserId
                                select role).FirstOrDefault();
                if (userrole.DisplayName == "Sales Executive")
                    userid = Convert.ToString(AbpSession.UserId);
            }

            var quotationCount = 0;
            try
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_GetQuotationInquiryFilter", con);
                    sqlComm.Parameters.AddWithValue("@Id", input.Id);
                    sqlComm.Parameters.AddWithValue("@QuotationCreateBy", input.QuotationCreateBy);
                    sqlComm.Parameters.AddWithValue("@QuotationStatus", input.StatusForQuotation);
                    sqlComm.Parameters.AddWithValue("@Salesman", input.Salesman);
                    sqlComm.Parameters.AddWithValue("@InquiryCreateBy", input.InquiryCreateBy);
                    sqlComm.Parameters.AddWithValue("@PotentialCustomer", input.PotentialCustomer);
                    sqlComm.Parameters.AddWithValue("@MileStoneName", input.MileStoneName);
                    sqlComm.Parameters.AddWithValue("@EnquiryStatus", input.Status);
                    sqlComm.Parameters.AddWithValue("@TeamName", input.TeamName);
                    sqlComm.Parameters.AddWithValue("@Coordinator", input.Coordinator);
                    sqlComm.Parameters.AddWithValue("@Designer", input.Designer);
                    sqlComm.Parameters.AddWithValue("@DesignationName", input.DesignationName);
                    sqlComm.Parameters.AddWithValue("@Emirates", input.Emirates);
                    sqlComm.Parameters.AddWithValue("@DepatmentName ", input.DepatmentName);
                    sqlComm.Parameters.AddWithValue("@Categories", input.Categories);
                    sqlComm.Parameters.AddWithValue("@Status", input.EnquiryStatus);
                    sqlComm.Parameters.AddWithValue("@WhyBafco", input.WhyBafco);
                    sqlComm.Parameters.AddWithValue("@Probability", input.Probability);
                    sqlComm.Parameters.AddWithValue("@InquiryCreation", input.InquiryCreation);
                    sqlComm.Parameters.AddWithValue("@QuotationCreation", input.QuotationCreation);
                    sqlComm.Parameters.AddWithValue("@ClosureDate", input.ClosureDate);
                    sqlComm.Parameters.AddWithValue("@LastActivityDate", input.LastActivity);
                    sqlComm.Parameters.AddWithValue("@QtnDateFilterId", input.QtnDateFilterId);
                    sqlComm.Parameters.AddWithValue("@ClsDateFilterId", input.ClsDateFilterId);
                    sqlComm.Parameters.AddWithValue("@LastActDateFilterId", input.LastActDateFilterId);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(dt);
                    }
                    // var c = dt.Rows.Count;
                    var Listdto = (from DataRow dr in dt.Rows
                                   select new QuotationInquiryFilter
                                   {
                                       QuotationRefNo = Convert.ToString(dr["REFNO"]),
                                       QuotationCreation = Convert.ToString(dr["QCreationTime"]),
                                       QuotationCreateBy = Convert.ToString(dr["QCreator"]),
                                       QuotationStatus = Convert.ToString(dr["QuotationStatus"]),
                                       Salesman = Convert.ToString(dr["Sales"]),
                                       QuotationValue = Convert.ToString(dr["Total"].ToString()),
                                       TitleOfInquiry = Convert.ToString(dr["Name"]),
                                       InquiryRefNo = Convert.ToString(dr["Submission"]),
                                       InquiryCreation = Convert.ToString(dr["ICreationTime"]),
                                       InquiryCreateBy = Convert.ToString(dr["Creator"]),
                                       PotentialCustomer = Convert.ToString(dr["Company"]),
                                       Email = Convert.ToString(dr["Email"]),
                                       MobileNumber = Convert.ToString(dr["MBNO"]),
                                       MileStoneName = Convert.ToString(dr["Milestone"]),
                                       EnquiryStatus = Convert.ToString(dr["Status"]),
                                       Total = Convert.ToString(dr["Estimation"].ToString()),
                                       TeamName = Convert.ToString(dr["Team"]),
                                       Coordinator = Convert.ToString(dr["Coordinator"]),
                                       Designer = Convert.ToString(dr["Designer"]),
                                       DesignationName = Convert.ToString(dr["Designation"]),
                                       Emirates = Convert.ToString(dr["Location"]),
                                       DepatmentName = Convert.ToString(dr["Depatment"]),
                                       ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                       LastActivity = Convert.ToString(dr["LastActivity"]),
                                       Probability = Convert.ToString(dr["Percentage"]),
                                       AreaName = Convert.ToString(dr["Area"]),
                                       BuildingName = Convert.ToString(dr["Building"]),
                                       Categories = Convert.ToString(dr["LeadType"]),
                                       Status = Convert.ToString(dr["Status"]),
                                       WhyBafco = Convert.ToString(dr["Whybafco"]),
                                       QuotationId = Convert.ToInt32(dr["Qid"])
                                   });

                    quotationCount = Listdto.Count();
                    Listdto = Listdto.Skip(input.SkipCount).Take(input.MaxResultCount);
                    var QuotationListDtos = Listdto.MapTo<List<QuotationInquiryFilter>>();

                    return new PagedResultDto<QuotationInquiryFilter>(quotationCount, QuotationListDtos);
                }
            }
            catch (Exception obj)
            {
                string dd = obj.Message.ToString();
                return new PagedResultDto<QuotationInquiryFilter>(0, null);
            }

        }
        public async Task SendDiscountMail(NullableIdDto input)
        {
            var qut = (from r in _quotationRepository.GetAll()
                       where r.Id == input.Id && r.Revised != true
                       select new DiscountEmailFirst
                       {
                           Id = r.Id,
                           SalesPersonId = r.SalesPersonId,
                           QuotationRefNo = r.RefNo,
                           FirstName = r.SalesPerson.Name,
                           LastName = r.SalesPerson.Surname
                       }).FirstOrDefault();
            var qutsal = _quotationRepository.GetAll().Where(p => p.Id == input.Id && p.Revised != true).Select(p => p.SalesPerson.UserName).FirstOrDefault();

            var salesmanager = (from a in _TeamDetailRepository.GetAll()
                                where a.SalesmanId == qut.SalesPersonId && a.IsDeleted == false
                                join b in _TeamRepository.GetAll().Where(p => p.IsDeleted == false) on a.TeamId equals b.Id
                                select b.SalesManager.EmailAddress).FirstOrDefault();

            try
            {
                await _userEmailer.DiscountEmailSendSalesManager(qut.Id, salesmanager, qut.QuotationRefNo, qut.FirstName + ' ' + qut.LastName);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task OverAllApproveQuote(NullableIdDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_OverAllQuotationApproval", conn);
                sqlComm.Parameters.AddWithValue("@QuotationId", input.Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public async Task<PagedResultDto<QuotationReportListDto>> GetTeamEnquiryReport(QuotationReportInput input)
        {
            var inquirycount = 0;
            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();
            var query = _enquiryDetailRepository.GetAll().Where(p => p.AssignedbyId == input.SalesId && p.ClosureDate >= currentmonth && p.ClosureDate < currentyear);
            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });
            try
            {
                var inqwdet = (from r in query
                               join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true && p.Junk != true && p.IsClosed != true) on r.InquiryId equals enq.Id
                               select new QuotationReportListDto
                               {
                                   Date = r.CreationTime.ToString("dd-MMM-yy"),
                                   InquiryName = r.InquiryId > 0 ? r.Inquirys.Name : "",
                                   Status = enq.LeadStatusId > 0 ? enq.LeadStatuss.LeadStatusName : "",
                                   CompanyName = enq.Companys.Name ?? "",
                                   QuotationId = 0,
                                   InquiryId = enq.Id,
                                   ClosureDate = r.ClosureDate ?? null,
                                   AccountManager = r.AssignedbyId > 0 ? r.AbpAccountManager.UserName : "",
                                   NewOrExisting = enq.Companys.NewCustomerTypes.Title ?? "",
                                   Location = enq.LocationId > 0 ? enq.Locations.LocationName : "",
                                   AEDValue = Math.Round(r.EstimationValue, 2),
                                   Stage = "New",
                                   Percentage = 0,
                                   WeightedAED = 0,
                                   Total1Value = 0,
                                   Total2Value = 0,
                                   Total3Value = 0,
                                   Total4Value = 0,
                                   Total5Value = 0,
                                   Total6Value = 0,
                                   Total7Value = 0,
                                   Total8Value = 0,
                                   Total9Value = 0,
                                   Total10Value = 0,
                                   Total11Value = 0,
                                   Total12Value = 0,
                                   Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                                   Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                                   Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                                   Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                                   Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                                   Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                                   Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                                   Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                                   Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                                   Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                                   Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                                   Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),
                                   ActionDate = r.LastActivity != null ? r.LastActivity.ToString() : "",
                                   Notes = r.Inquirys.Remarks ?? ""

                               });

                inquirycount = await inqwdet.CountAsync();

                var inquirylist = await inqwdet
                        .OrderBy(p => p.Date)
                        .PageBy(input)
                        .ToListAsync();

                inquirylistdto = inquirylist.MapTo<List<QuotationReportListDto>>();
            }
            catch (Exception ex)
            {

            }
            try
            {
                foreach (var item in inquirylistdto)
                {
                    var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                    if (data != null)
                    {
                        item.AEDValue = data.QuotationTotal;
                        item.WeightedAED = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                        item.Stage = data.EnqStage;
                        item.Percentage = data.StagePercent;

                        var InquiryDetailClosureDate = item.ClosureDate;
                        if (InquiryDetailClosureDate != null)
                        {
                            var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                            if (listmonths[0].ToString("MMM-yyyy") == Month)
                            {
                                item.Total1Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[1].ToString("MMM-yyyy") == Month)
                            {
                                item.Total2Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[2].ToString("MMM-yyyy") == Month)
                            {
                                item.Total3Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[3].ToString("MMM-yyyy") == Month)
                            {
                                item.Total4Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[4].ToString("MMM-yyyy") == Month)
                            {
                                item.Total5Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[5].ToString("MMM-yyyy") == Month)
                            {
                                item.Total6Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[6].ToString("MMM-yyyy") == Month)
                            {
                                item.Total7Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[7].ToString("MMM-yyyy") == Month)
                            {
                                item.Total8Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[8].ToString("MMM-yyyy") == Month)
                            {
                                item.Total9Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[9].ToString("MMM-yyyy") == Month)
                            {
                                item.Total10Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[10].ToString("MMM-yyyy") == Month)
                            {
                                item.Total11Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[11].ToString("MMM-yyyy") == Month)
                            {
                                item.Total12Value = Math.Round(item.WeightedAED, 2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return new PagedResultDto<QuotationReportListDto>(inquirycount, inquirylistdto);
        }
        public async Task<PagedResultDto<TeamReportListDto>> GetTeamReport(TeamReportInput input)
        {
            var query = _TeamDetailRepository.GetAll().Where(p => p.TeamId == input.TeamId);

            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();

            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });

            var reg = (from r in query
                       select new TeamReportListDto
                       {
                           TeamName = r.Team.Name ?? "",
                           AccountManager = r.Salesman.UserName ?? "",
                           AccountManagerId = r.SalesmanId,
                           TotalAEDValue = 0,
                           TeamId = (int)(r.TeamId ?? 0),
                           TotalWeightedAED = 0,
                           Total1Value = 0,
                           Total2Value = 0,
                           Total3Value = 0,
                           Total4Value = 0,
                           Total5Value = 0,
                           Total6Value = 0,
                           Total7Value = 0,
                           Total8Value = 0,
                           Total9Value = 0,
                           Total10Value = 0,
                           Total11Value = 0,
                           Total12Value = 0,
                           Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                           Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                           Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                           Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                           Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                           Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                           Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                           Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                           Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                           Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                           Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                           Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),
                       });

            var teamDetailCount = await reg.CountAsync();

            var teamDetaillist = await reg
                    .PageBy(input)
                    .ToListAsync();

            var TeamDetailListDtos = teamDetaillist.MapTo<List<TeamReportListDto>>();
            try
            {

                foreach (var salesman in TeamDetailListDtos)
                {
                    var SalesmanId = (from u in UserManager.Users where u.UserName == salesman.AccountManager select u.Id).FirstOrDefault();
                    var enqDetail = (from t in _enquiryDetailRepository.GetAll().Where(r => r.AssignedbyId == SalesmanId && r.ClosureDate >= currentmonth && r.ClosureDate < currentyear) join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true && p.Junk != true) on t.InquiryId equals enq.Id select t).ToArray();
                    decimal salesmanaedvalue = 0;
                    decimal salesmanweightvalue = 0;
                    if (enqDetail.Length > 0)
                    {
                        foreach (var item in enqDetail)
                        {
                            decimal qutsalesmanaedvalue = Math.Round(item.EstimationValue, 2);
                            decimal qutsalesmanweightvalue = 0;
                            var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                            if (data != null)
                            {
                                qutsalesmanaedvalue = data.QuotationTotal;
                                qutsalesmanweightvalue = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                                salesmanaedvalue = salesmanaedvalue + qutsalesmanaedvalue;
                                salesmanweightvalue = salesmanweightvalue + qutsalesmanweightvalue;
                                var InquiryDetailClosureDate = item.ClosureDate;
                                if (InquiryDetailClosureDate != null)
                                {
                                    var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                                    if (listmonths[0].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total1Value = Math.Round(Convert.ToDecimal(salesman.Total1Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[1].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total2Value = Math.Round(Convert.ToDecimal(salesman.Total2Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[2].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total3Value = Math.Round(Convert.ToDecimal(salesman.Total3Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[3].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total4Value = Math.Round(Convert.ToDecimal(salesman.Total4Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[4].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total5Value = Math.Round(Convert.ToDecimal(salesman.Total5Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[5].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total6Value = Math.Round(Convert.ToDecimal(salesman.Total6Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[6].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total7Value = Math.Round(Convert.ToDecimal(salesman.Total7Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[7].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total8Value = Math.Round(Convert.ToDecimal(salesman.Total8Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[8].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total9Value = Math.Round(Convert.ToDecimal(salesman.Total9Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[9].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total10Value = Math.Round(Convert.ToDecimal(salesman.Total10Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[10].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total11Value = Math.Round(Convert.ToDecimal(salesman.Total11Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[11].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total12Value = Math.Round(Convert.ToDecimal(salesman.Total12Value + qutsalesmanweightvalue), 2);
                                    }

                                }
                            }
                        }
                    }
                    salesman.TotalAEDValue = Math.Round(salesmanaedvalue, 2);
                    salesman.TotalWeightedAED = Math.Round(salesmanweightvalue, 2);

                }
            }
            catch (Exception ex)
            {

            }
            return new PagedResultDto<TeamReportListDto>(teamDetailCount, TeamDetailListDtos);
        }
        public async Task<PagedResultDto<TeamReportListDto>> GetAllTeamReport(TeamReportInput input)
        {
            var query = _TeamRepository.GetAll();
            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();

            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });

            if (input.TeamId > 0)
            {
                query = _TeamRepository.GetAll().Where(p => p.Id == input.TeamId);
            }

            query = query
                .WhereIf(
                !input.Filter.IsNullOrWhiteSpace(),
                u =>
                    u.Name.Contains(input.Filter)
                    );

            var reg = (from r in query
                       select new TeamReportListDto
                       {
                           TeamName = r.Name,
                           TeamId = r.Id,
                           AccountManager = "",
                           TotalAEDValue = 0,
                           TotalWeightedAED = 0,
                           Total1Value = 0,
                           Total2Value = 0,
                           Total3Value = 0,
                           Total4Value = 0,
                           Total5Value = 0,
                           Total6Value = 0,
                           Total7Value = 0,
                           Total8Value = 0,
                           Total9Value = 0,
                           Total10Value = 0,
                           Total11Value = 0,
                           Total12Value = 0,
                           Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                           Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                           Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                           Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                           Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                           Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                           Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                           Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                           Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                           Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                           Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                           Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),

                       });

            var teamCount = await reg.CountAsync();

            var teamlist = await reg
                    .PageBy(input)
                    .ToListAsync();

            var TeamListDtos = teamlist.MapTo<List<TeamReportListDto>>();

            foreach (var team in TeamListDtos)
            {

                var TeamId = (from p in _TeamRepository.GetAll() where p.Name == team.TeamName select p.Id).FirstOrDefault();
                var enqDetail = (from t in _enquiryDetailRepository.GetAll().Where(r => r.TeamId == TeamId && r.ClosureDate >= currentmonth && r.ClosureDate < currentyear) join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true &&  p.Junk != true) on t.InquiryId equals enq.Id select t).ToArray();
                decimal teamaedvalue = 0;
                decimal teamweightvalue = 0;
                if (enqDetail.Length > 0)
                {
                    foreach (var item in enqDetail)
                    {
                        decimal qutsalesmanaedvalue = Math.Round(item.EstimationValue, 2);
                        decimal qutsalesmanweightvalue = 0;
                        try
                        {
                            var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                            if (data != null)
                            {
                                qutsalesmanaedvalue = data.QuotationTotal;
                                qutsalesmanweightvalue = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                                teamaedvalue = teamaedvalue + qutsalesmanaedvalue;
                                teamweightvalue = teamweightvalue + qutsalesmanweightvalue;
                                var InquiryDetailClosureDate = item.ClosureDate;
                                if (InquiryDetailClosureDate != null)
                                {
                                    var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                                    if (listmonths[0].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total1Value = Math.Round(Convert.ToDecimal(team.Total1Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[1].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total2Value = Math.Round(Convert.ToDecimal(team.Total2Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[2].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total3Value = Math.Round(Convert.ToDecimal(team.Total3Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[3].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total4Value = Math.Round(Convert.ToDecimal(team.Total4Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[4].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total5Value = Math.Round(Convert.ToDecimal(team.Total5Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[5].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total6Value = Math.Round(Convert.ToDecimal(team.Total6Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[6].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total7Value = Math.Round(Convert.ToDecimal(team.Total7Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[7].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total8Value = Math.Round(Convert.ToDecimal(team.Total8Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[8].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total9Value = Math.Round(Convert.ToDecimal(team.Total9Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[9].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total10Value = Math.Round(Convert.ToDecimal(team.Total10Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[10].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total11Value = Math.Round(Convert.ToDecimal(team.Total11Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[11].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total12Value = Math.Round(Convert.ToDecimal(team.Total12Value + qutsalesmanweightvalue), 2);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                team.TotalAEDValue = Math.Round(teamaedvalue, 2);
                team.TotalWeightedAED = Math.Round(teamweightvalue, 2);
            }

            return new PagedResultDto<TeamReportListDto>(teamCount, TeamListDtos);

        }
        public async Task<FileDto> GetQuotationInquiryFilterToExcel(NullableIdDto input)
        {
            var View = (from v in _ViewRepository.GetAll() where v.Id == input.Id select v).FirstOrDefault();

            List<string> RemovedColumns = new List<string>();

            if (View.Query != null)
            {
                RemovedColumns = View.Query.Split(',').ToList<string>();
            }

            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_GetQuotationInquiryFilter", con);
                sqlComm.Parameters.AddWithValue("@Id", input.Id);
                sqlComm.Parameters.AddWithValue("@QuotationCreateBy", null);
                sqlComm.Parameters.AddWithValue("@QuotationStatus", null);
                sqlComm.Parameters.AddWithValue("@Salesman", null);
                sqlComm.Parameters.AddWithValue("@InquiryCreateBy", null);
                sqlComm.Parameters.AddWithValue("@PotentialCustomer", null);
                sqlComm.Parameters.AddWithValue("@MileStoneName", null);
                sqlComm.Parameters.AddWithValue("@EnquiryStatus", null);
                sqlComm.Parameters.AddWithValue("@TeamName", null);
                sqlComm.Parameters.AddWithValue("@Coordinator", null);
                sqlComm.Parameters.AddWithValue("@Designer", null);
                sqlComm.Parameters.AddWithValue("@DesignationName", null);
                sqlComm.Parameters.AddWithValue("@Emirates", null);
                sqlComm.Parameters.AddWithValue("@DepatmentName ", null);
                sqlComm.Parameters.AddWithValue("@Categories", null);
                sqlComm.Parameters.AddWithValue("@Status", null);
                sqlComm.Parameters.AddWithValue("@WhyBafco", null);
                sqlComm.Parameters.AddWithValue("@Probability", null);
                sqlComm.Parameters.AddWithValue("@InquiryCreation", null);
                sqlComm.Parameters.AddWithValue("@QuotationCreation", null);
                sqlComm.Parameters.AddWithValue("@ClosureDate", null);
                sqlComm.Parameters.AddWithValue("@LastActivityDate", null);
                sqlComm.Parameters.AddWithValue("@QtnDateFilterId", 0);
                sqlComm.Parameters.AddWithValue("@ClsDateFilterId", 0);
                sqlComm.Parameters.AddWithValue("@LastActDateFilterId", 0);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(dt);
                }

                var Listdto = (from DataRow dr in dt.Rows
                               select new QuotationInquiryFilter
                               {
                                   QuotationRefNo = Convert.ToString(dr["REFNO"]),
                                   QuotationCreation = Convert.ToString(dr["QCreationTime"]),
                                   QuotationCreateBy = Convert.ToString(dr["QCreator"]),
                                   QuotationStatus = Convert.ToString(dr["QuotationStatus"]),
                                   Salesman = Convert.ToString(dr["Sales"]),
                                   QuotationValue = Convert.ToString(dr["Total"].ToString()),
                                   TitleOfInquiry = Convert.ToString(dr["Name"]),
                                   InquiryRefNo = Convert.ToString(dr["Submission"]),
                                   InquiryCreation = Convert.ToString(dr["ICreationTime"]),
                                   InquiryCreateBy = Convert.ToString(dr["Creator"]),
                                   PotentialCustomer = Convert.ToString(dr["Company"]),
                                   Email = Convert.ToString(dr["Email"]),
                                   MobileNumber = Convert.ToString(dr["MBNO"]),
                                   MileStoneName = Convert.ToString(dr["Milestone"]),
                                   EnquiryStatus = Convert.ToString(dr["Status"]),
                                   Total = Convert.ToString(dr["Estimation"].ToString()),
                                   TeamName = Convert.ToString(dr["Team"]),
                                   Coordinator = Convert.ToString(dr["Coordinator"]),
                                   Designer = Convert.ToString(dr["Designer"]),
                                   DesignationName = Convert.ToString(dr["Designation"]),
                                   Emirates = Convert.ToString(dr["Location"]),
                                   DepatmentName = Convert.ToString(dr["Depatment"]),
                                   ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                   LastActivity = Convert.ToString(dr["LastActivity"]),
                                   Probability = Convert.ToString(dr["Percentage"]),
                                   AreaName = Convert.ToString(dr["Area"]),
                                   BuildingName = Convert.ToString(dr["Building"]),
                                   Categories = Convert.ToString(dr["LeadType"]),
                                   Status = Convert.ToString(dr["Status"]),
                                   WhyBafco = Convert.ToString(dr["Whybafco"])
                               });

                var QuotationListDtos = Listdto.MapTo<List<QuotationInquiryFilter>>();

                return _QuotationInquiryFilterListExcelExporter.ExportToFile(QuotationListDtos, View.Name, RemovedColumns);
            }

        }
        public async Task<FileDto> GetTeamEnquiryReportExcel(NullableIdDto input)
        {
            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();
            var query = _enquiryDetailRepository.GetAll().Where(p => p.AssignedbyId == input.Id && p.ClosureDate >= currentmonth && p.ClosureDate < currentyear);
            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });
            try
            {
                var inqwdet = (from r in query
                               join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true && p.Junk != true && p.IsClosed != true) on r.InquiryId equals enq.Id
                               select new QuotationReportListDto
                               {
                                   Date = r.CreationTime.ToString("dd-MMM-yy"),
                                   InquiryName = r.InquiryId > 0 ? r.Inquirys.Name : "",
                                   Status = enq.LeadStatusId > 0 ? enq.LeadStatuss.LeadStatusName : "",
                                   CompanyName = enq.Companys.Name ?? "",
                                   QuotationId = 0,
                                   InquiryId = enq.Id,
                                   ClosureDate = r.ClosureDate ?? null,
                                   AccountManager = r.AssignedbyId > 0 ? r.AbpAccountManager.UserName : "",
                                   NewOrExisting = enq.Companys.NewCustomerTypes.Title ?? "",
                                   Location = enq.LocationId > 0 ? enq.Locations.LocationName : "",
                                   AEDValue = Math.Round(r.EstimationValue, 2),
                                   Stage = "New",
                                   Percentage = 0,
                                   WeightedAED = 0,
                                   Total1Value = 0,
                                   Total2Value = 0,
                                   Total3Value = 0,
                                   Total4Value = 0,
                                   Total5Value = 0,
                                   Total6Value = 0,
                                   Total7Value = 0,
                                   Total8Value = 0,
                                   Total9Value = 0,
                                   Total10Value = 0,
                                   Total11Value = 0,
                                   Total12Value = 0,
                                   Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                                   Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                                   Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                                   Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                                   Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                                   Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                                   Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                                   Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                                   Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                                   Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                                   Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                                   Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),
                                   ActionDate = r.LastActivity != null ? r.LastActivity.ToString() : "",
                                   Notes = r.Inquirys.Remarks ?? ""

                               });

                inquirylistdto = inqwdet.MapTo<List<QuotationReportListDto>>();
            }
            catch (Exception ex)
            {

            }
            try
            {
                foreach (var item in inquirylistdto)
                {
                    var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                    if (data != null)
                    {
                        item.AEDValue = data.QuotationTotal;
                        item.WeightedAED = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                        item.Stage = data.EnqStage;
                        item.Percentage = data.StagePercent;

                        var InquiryDetailClosureDate = item.ClosureDate;
                        if (InquiryDetailClosureDate != null)
                        {
                            var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                            if (listmonths[0].ToString("MMM-yyyy") == Month)
                            {
                                item.Total1Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[1].ToString("MMM-yyyy") == Month)
                            {
                                item.Total2Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[2].ToString("MMM-yyyy") == Month)
                            {
                                item.Total3Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[3].ToString("MMM-yyyy") == Month)
                            {
                                item.Total4Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[4].ToString("MMM-yyyy") == Month)
                            {
                                item.Total5Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[5].ToString("MMM-yyyy") == Month)
                            {
                                item.Total6Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[6].ToString("MMM-yyyy") == Month)
                            {
                                item.Total7Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[7].ToString("MMM-yyyy") == Month)
                            {
                                item.Total8Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[8].ToString("MMM-yyyy") == Month)
                            {
                                item.Total9Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[9].ToString("MMM-yyyy") == Month)
                            {
                                item.Total10Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[10].ToString("MMM-yyyy") == Month)
                            {
                                item.Total11Value = Math.Round(item.WeightedAED, 2);
                            }
                            else if (listmonths[11].ToString("MMM-yyyy") == Month)
                            {
                                item.Total12Value = Math.Round(item.WeightedAED, 2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            QuotationReportListDto TotalReport = new QuotationReportListDto
            {
                Date = "Total",
                AEDValue = 0,
                WeightedAED = 0,
                Total1Value = 0,
                Total2Value = 0,
                Total3Value = 0,
                Total4Value = 0,
                Total5Value = 0,
                Total6Value = 0,
                Total7Value = 0,
                Total8Value = 0,
                Total9Value = 0,
                Total10Value = 0,
                Total11Value = 0,
                Total12Value = 0
            };

            foreach (var item in inquirylistdto)
            {
                TotalReport.AEDValue = Decimal.Add(TotalReport.AEDValue, item.AEDValue);
                TotalReport.WeightedAED = Decimal.Add(TotalReport.WeightedAED, item.WeightedAED);
                TotalReport.Total1Value = Decimal.Add(TotalReport.Total1Value, item.Total1Value);
                TotalReport.Total2Value = Decimal.Add(TotalReport.Total2Value, item.Total2Value);
                TotalReport.Total3Value = Decimal.Add(TotalReport.Total3Value, item.Total3Value);
                TotalReport.Total4Value = Decimal.Add(TotalReport.Total4Value, item.Total4Value);
                TotalReport.Total5Value = Decimal.Add(TotalReport.Total5Value, item.Total5Value);
                TotalReport.Total6Value = Decimal.Add(TotalReport.Total6Value, item.Total6Value);
                TotalReport.Total7Value = Decimal.Add(TotalReport.Total7Value, item.Total7Value);
                TotalReport.Total8Value = Decimal.Add(TotalReport.Total8Value, item.Total8Value);
                TotalReport.Total9Value = Decimal.Add(TotalReport.Total9Value, item.Total9Value);
                TotalReport.Total10Value = Decimal.Add(TotalReport.Total10Value, item.Total10Value);
                TotalReport.Total11Value = Decimal.Add(TotalReport.Total11Value, item.Total11Value);
                TotalReport.Total12Value = Decimal.Add(TotalReport.Total12Value, item.Total12Value);
            }
            return _TeamEnquiryReportExcelExporter.ExportToFile(inquirylistdto, TotalReport);

        }
        public async Task<FileDto> GetTeamReportExcel(NullableIdDto input)
        {
            var query = _TeamDetailRepository.GetAll().Where(p => p.TeamId == input.Id);

            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();

            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });

            var reg = (from r in query
                       select new TeamReportListDto
                       {
                           TeamName = r.Team.Name ?? "",
                           AccountManager = r.Salesman.UserName ?? "",
                           AccountManagerId = r.SalesmanId,
                           TotalAEDValue = 0,
                           TeamId = (int)(r.TeamId ?? 0),
                           TotalWeightedAED = 0,
                           Total1Value = 0,
                           Total2Value = 0,
                           Total3Value = 0,
                           Total4Value = 0,
                           Total5Value = 0,
                           Total6Value = 0,
                           Total7Value = 0,
                           Total8Value = 0,
                           Total9Value = 0,
                           Total10Value = 0,
                           Total11Value = 0,
                           Total12Value = 0,
                           Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                           Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                           Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                           Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                           Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                           Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                           Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                           Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                           Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                           Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                           Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                           Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),
                       });

            var TeamDetailListDtos = reg.MapTo<List<TeamReportListDto>>();

            try
            {

                foreach (var salesman in TeamDetailListDtos)
                {
                    var SalesmanId = (from u in UserManager.Users where u.UserName == salesman.AccountManager select u.Id).FirstOrDefault();
                    var enqDetail = (from t in _enquiryDetailRepository.GetAll().Where(r => r.AssignedbyId == SalesmanId && r.ClosureDate >= currentmonth && r.ClosureDate < currentyear) join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true && p.Junk != true) on t.InquiryId equals enq.Id select t).ToArray();
                    decimal salesmanaedvalue = 0;
                    decimal salesmanweightvalue = 0;
                    if (enqDetail.Length > 0)
                    {
                        foreach (var item in enqDetail)
                        {
                            decimal qutsalesmanaedvalue = Math.Round(item.EstimationValue, 2);
                            decimal qutsalesmanweightvalue = 0;
                            var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                            if (data != null)
                            {
                                qutsalesmanaedvalue = data.QuotationTotal;
                                qutsalesmanweightvalue = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                                salesmanaedvalue = salesmanaedvalue + qutsalesmanaedvalue;
                                salesmanweightvalue = salesmanweightvalue + qutsalesmanweightvalue;
                                var InquiryDetailClosureDate = item.ClosureDate;
                                if (InquiryDetailClosureDate != null)
                                {
                                    var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                                    if (listmonths[0].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total1Value = Math.Round(Convert.ToDecimal(salesman.Total1Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[1].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total2Value = Math.Round(Convert.ToDecimal(salesman.Total2Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[2].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total3Value = Math.Round(Convert.ToDecimal(salesman.Total3Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[3].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total4Value = Math.Round(Convert.ToDecimal(salesman.Total4Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[4].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total5Value = Math.Round(Convert.ToDecimal(salesman.Total5Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[5].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total6Value = Math.Round(Convert.ToDecimal(salesman.Total6Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[6].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total7Value = Math.Round(Convert.ToDecimal(salesman.Total7Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[7].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total8Value = Math.Round(Convert.ToDecimal(salesman.Total8Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[8].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total9Value = Math.Round(Convert.ToDecimal(salesman.Total9Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[9].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total10Value = Math.Round(Convert.ToDecimal(salesman.Total10Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[10].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total11Value = Math.Round(Convert.ToDecimal(salesman.Total11Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[11].ToString("MMM-yyyy") == Month)
                                    {
                                        salesman.Total12Value = Math.Round(Convert.ToDecimal(salesman.Total12Value + qutsalesmanweightvalue), 2);
                                    }

                                }
                            }
                        }
                    }
                    salesman.TotalAEDValue = Math.Round(salesmanaedvalue, 2);
                    salesman.TotalWeightedAED = Math.Round(salesmanweightvalue, 2);

                }
            }
            catch (Exception ex)
            {

            }
            TeamReportListDto TotalReport = new TeamReportListDto
            {
                AccountManager = "Total",
                TotalAEDValue = 0,
                TotalWeightedAED = 0,
                Total1Value = 0,
                Total2Value = 0,
                Total3Value = 0,
                Total4Value = 0,
                Total5Value = 0,
                Total6Value = 0,
                Total7Value = 0,
                Total8Value = 0,
                Total9Value = 0,
                Total10Value = 0,
                Total11Value = 0,
                Total12Value = 0
            };

            foreach (var item in TeamDetailListDtos)
            {
                TotalReport.TotalAEDValue = Decimal.Add(TotalReport.TotalAEDValue, item.TotalAEDValue);
                TotalReport.TotalWeightedAED = Decimal.Add(TotalReport.TotalWeightedAED, item.TotalWeightedAED);
                TotalReport.Total1Value = Decimal.Add(TotalReport.Total1Value, item.Total1Value);
                TotalReport.Total2Value = Decimal.Add(TotalReport.Total2Value, item.Total2Value);
                TotalReport.Total3Value = Decimal.Add(TotalReport.Total3Value, item.Total3Value);
                TotalReport.Total4Value = Decimal.Add(TotalReport.Total4Value, item.Total4Value);
                TotalReport.Total5Value = Decimal.Add(TotalReport.Total5Value, item.Total5Value);
                TotalReport.Total6Value = Decimal.Add(TotalReport.Total6Value, item.Total6Value);
                TotalReport.Total7Value = Decimal.Add(TotalReport.Total7Value, item.Total7Value);
                TotalReport.Total8Value = Decimal.Add(TotalReport.Total8Value, item.Total8Value);
                TotalReport.Total9Value = Decimal.Add(TotalReport.Total9Value, item.Total9Value);
                TotalReport.Total10Value = Decimal.Add(TotalReport.Total10Value, item.Total10Value);
                TotalReport.Total11Value = Decimal.Add(TotalReport.Total11Value, item.Total11Value);
                TotalReport.Total12Value = Decimal.Add(TotalReport.Total12Value, item.Total12Value);
            }
            return _TeamReportExcelExporter.ExportToFile(TeamDetailListDtos, TotalReport);
        }
        public async Task<FileDto> GetAllTeamReportExcel()
        {
            var query = _TeamRepository.GetAll();
            DateTime currentdate = DateTime.Now;
            var currentmonth = new DateTime(currentdate.Year, currentdate.Month, 1);
            var currentyear = currentmonth.AddYears(1);
            var listmonths = Enumerable.Range(0, 12).Select(m => currentmonth.AddMonths(m)).ToArray();

            string viewquery = "SELECT * FROM [dbo].[View_ForecastReport]";
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

            var viewdata = (from DataRow dr in viewtable.Rows
                            select new ForecastDataList
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                QuotationTotal = Convert.ToDecimal(dr["QuotationTotal"]),
                                StagePercent = Convert.ToDecimal(dr["StagePercent"]),
                                EnqStage = Convert.ToString(dr["EnqStage"])
                            });

            var reg = (from r in query
                       select new TeamReportListDto
                       {
                           TeamName = r.Name,
                           TeamId = r.Id,
                           AccountManager = "",
                           TotalAEDValue = 0,
                           TotalWeightedAED = 0,
                           Total1Value = 0,
                           Total2Value = 0,
                           Total3Value = 0,
                           Total4Value = 0,
                           Total5Value = 0,
                           Total6Value = 0,
                           Total7Value = 0,
                           Total8Value = 0,
                           Total9Value = 0,
                           Total10Value = 0,
                           Total11Value = 0,
                           Total12Value = 0,
                           Total1ValueFormat = listmonths[0].ToString("MMM-yyyy"),
                           Total2ValueFormat = listmonths[1].ToString("MMM-yyyy"),
                           Total3ValueFormat = listmonths[2].ToString("MMM-yyyy"),
                           Total4ValueFormat = listmonths[3].ToString("MMM-yyyy"),
                           Total5ValueFormat = listmonths[4].ToString("MMM-yyyy"),
                           Total6ValueFormat = listmonths[5].ToString("MMM-yyyy"),
                           Total7ValueFormat = listmonths[6].ToString("MMM-yyyy"),
                           Total8ValueFormat = listmonths[7].ToString("MMM-yyyy"),
                           Total9ValueFormat = listmonths[8].ToString("MMM-yyyy"),
                           Total10ValueFormat = listmonths[9].ToString("MMM-yyyy"),
                           Total11ValueFormat = listmonths[10].ToString("MMM-yyyy"),
                           Total12ValueFormat = listmonths[11].ToString("MMM-yyyy"),

                       });

            var TeamListDtos = reg.MapTo<List<TeamReportListDto>>();

            foreach (var team in TeamListDtos)
            {

                var TeamId = (from p in _TeamRepository.GetAll() where p.Name == team.TeamName select p.Id).FirstOrDefault();
                var enqDetail = (from t in _enquiryDetailRepository.GetAll().Where(r => r.TeamId == TeamId && r.ClosureDate >= currentmonth && r.ClosureDate < currentyear) join enq in _inquiryRepository.GetAll().Where(p => p.Lost != true && p.Won != true && p.Junk != true) on t.InquiryId equals enq.Id select t).ToArray();
                decimal teamaedvalue = 0;
                decimal teamweightvalue = 0;
                if (enqDetail.Length > 0)
                {
                    foreach (var item in enqDetail)
                    {
                        decimal qutsalesmanaedvalue = Math.Round(item.EstimationValue, 2);
                        decimal qutsalesmanweightvalue = 0;
                        try
                        {
                            var data = (from r in viewdata where r.Id == item.InquiryId select r).FirstOrDefault();
                            if (data != null)
                            {
                                qutsalesmanaedvalue = data.QuotationTotal;
                                qutsalesmanweightvalue = Math.Round(data.StagePercent * data.QuotationTotal / 100, 2);
                                teamaedvalue = teamaedvalue + qutsalesmanaedvalue;
                                teamweightvalue = teamweightvalue + qutsalesmanweightvalue;
                                var InquiryDetailClosureDate = item.ClosureDate;
                                if (InquiryDetailClosureDate != null)
                                {
                                    var Month = InquiryDetailClosureDate.GetValueOrDefault().ToString("MMM-yyyy");
                                    if (listmonths[0].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total1Value = Math.Round(Convert.ToDecimal(team.Total1Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[1].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total2Value = Math.Round(Convert.ToDecimal(team.Total2Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[2].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total3Value = Math.Round(Convert.ToDecimal(team.Total3Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[3].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total4Value = Math.Round(Convert.ToDecimal(team.Total4Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[4].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total5Value = Math.Round(Convert.ToDecimal(team.Total5Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[5].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total6Value = Math.Round(Convert.ToDecimal(team.Total6Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[6].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total7Value = Math.Round(Convert.ToDecimal(team.Total7Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[7].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total8Value = Math.Round(Convert.ToDecimal(team.Total8Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[8].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total9Value = Math.Round(Convert.ToDecimal(team.Total9Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[9].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total10Value = Math.Round(Convert.ToDecimal(team.Total10Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[10].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total11Value = Math.Round(Convert.ToDecimal(team.Total11Value + qutsalesmanweightvalue), 2);
                                    }
                                    else if (listmonths[11].ToString("MMM-yyyy") == Month)
                                    {
                                        team.Total12Value = Math.Round(Convert.ToDecimal(team.Total12Value + qutsalesmanweightvalue), 2);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
                team.TotalAEDValue = Math.Round(teamaedvalue, 2);
                team.TotalWeightedAED = Math.Round(teamweightvalue, 2);
            }

            TeamReportListDto TotalReport = new TeamReportListDto
            {
                TeamName = "Total",
                TotalAEDValue = 0,
                TotalWeightedAED = 0,
                Total1Value = 0,
                Total2Value = 0,
                Total3Value = 0,
                Total4Value = 0,
                Total5Value = 0,
                Total6Value = 0,
                Total7Value = 0,
                Total8Value = 0,
                Total9Value = 0,
                Total10Value = 0,
                Total11Value = 0,
                Total12Value = 0
            };

            foreach (var item in TeamListDtos)
            {
                TotalReport.TotalAEDValue = Decimal.Add(TotalReport.TotalAEDValue, item.TotalAEDValue);
                TotalReport.TotalWeightedAED = Decimal.Add(TotalReport.TotalWeightedAED, item.TotalWeightedAED);
                TotalReport.Total1Value = Decimal.Add(TotalReport.Total1Value, item.Total1Value);
                TotalReport.Total2Value = Decimal.Add(TotalReport.Total2Value, item.Total2Value);
                TotalReport.Total3Value = Decimal.Add(TotalReport.Total3Value, item.Total3Value);
                TotalReport.Total4Value = Decimal.Add(TotalReport.Total4Value, item.Total4Value);
                TotalReport.Total5Value = Decimal.Add(TotalReport.Total5Value, item.Total5Value);
                TotalReport.Total6Value = Decimal.Add(TotalReport.Total6Value, item.Total6Value);
                TotalReport.Total7Value = Decimal.Add(TotalReport.Total7Value, item.Total7Value);
                TotalReport.Total8Value = Decimal.Add(TotalReport.Total8Value, item.Total8Value);
                TotalReport.Total9Value = Decimal.Add(TotalReport.Total9Value, item.Total9Value);
                TotalReport.Total10Value = Decimal.Add(TotalReport.Total10Value, item.Total10Value);
                TotalReport.Total11Value = Decimal.Add(TotalReport.Total11Value, item.Total11Value);
                TotalReport.Total12Value = Decimal.Add(TotalReport.Total12Value, item.Total12Value);
            }

            return _AllTeamReportExcelExporter.ExportToFile(TeamListDtos, TotalReport);

        }

    }

    public class DiscountEmailFirst
    {
        public int Id { get; set; }
        public virtual string QuotationRefNo { get; set; }
        public virtual long? SalesPersonId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

    }
    public class ForecastDataList
    {
        public int Id { get; set; }
        public string QuotationRefno { get; set; }
        public decimal QuotationTotal { get; set; }
        public decimal StagePercent { get; set; }
        public string EnqStage { get; set; }
    }
    public class QuotationRevisionInput
    {
        public int Id { get; set; }
        public virtual DateTime NextActivity { get; set; }
    }
}
