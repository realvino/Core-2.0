using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Authorization.Roles;
using tibs.stem.Discounts;
using tibs.stem.EnquiryDetails;
using tibs.stem.Inquirys.Dto;
using tibs.stem.Select2;
using tibs.stem.Team;
using tibs.stem.TeamDetails;
using tibs.stem.Tenants.Dashboard.Dto;
using tibs.stem.Url;

namespace tibs.stem.Tenants.Dashboard
{
    //[DisableAuditing]
    //[AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : stemAppServiceBase, ITenantDashboardAppService
    {
        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<TeamDetail> _TeamDetailRepository;
        private readonly IRepository<Teams> _TeamRepository;
        private readonly IWebUrlService _webUrlService;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly RoleManager _roleManager;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;

        public TenantDashboardAppService(
            IRepository<Discount> discountRepository,
            IRepository<TeamDetail> TeamDetailRepository,
            IRepository<Teams> TeamRepository,
            IRepository<UserRole, long> userRoleRepository,
            RoleManager roleManager,
            IWebUrlService webUrlService,
            IRepository<EnquiryDetail> enquiryDetailRepository
            )
        {
            _discountRepository = discountRepository;
            _TeamDetailRepository = TeamDetailRepository;
            _TeamRepository = TeamRepository;
            _webUrlService = webUrlService;
            _userRoleRepository = userRoleRepository;
            _roleManager = roleManager;
            _enquiryDetailRepository = enquiryDetailRepository;
        }

        public GetMemberActivityOutput GetMemberActivity()
        {
            return new GetMemberActivityOutput
            (
                DashboardRandomDataGenerator.GenerateMemberActivities()
            );
        }

        public GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input)
        {
            var output = new GetDashboardDataOutput
            {
                TotalProfit = DashboardRandomDataGenerator.GetRandomInt(5000, 9000),
                NewFeedbacks = DashboardRandomDataGenerator.GetRandomInt(1000, 5000),
                NewOrders = DashboardRandomDataGenerator.GetRandomInt(100, 900),
                NewUsers = DashboardRandomDataGenerator.GetRandomInt(50, 500),
                SalesSummary = DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod),
                Expenses = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Growth = DashboardRandomDataGenerator.GetRandomInt(5000, 10000),
                Revenue = DashboardRandomDataGenerator.GetRandomInt(1000, 9000),
                TotalSales = DashboardRandomDataGenerator.GetRandomInt(10000, 90000),
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NetworkLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                CpuLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                LoadRate = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                TimeLineItems = DashboardRandomDataGenerator.GenerateTimeLineItems()
            };

            return output;
        }

        public GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input)
        {
            return new GetSalesSummaryOutput(DashboardRandomDataGenerator.GenerateSalesSummaryData(input.SalesSummaryDatePeriod));
        }

        public GetWorldMapOutput GetWorldMap(GetWorldMapInput input)
        {
            return new GetWorldMapOutput(DashboardRandomDataGenerator.GenerateWorldMapCountries());
        }

        public GetServerStatsOutput GetServerStats(GetServerStatsInput input)
        {
            return new GetServerStatsOutput
            {
                NetworkLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                CpuLoad = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15),
                LoadRate = DashboardRandomDataGenerator.GetRandomArray(20, 8, 15)
            };
        }

        public GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input)
        {
            return new GetGeneralStatsOutput
            {
                TransactionPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                NewVisitPercent = DashboardRandomDataGenerator.GetRandomInt(10, 100),
                BouncePercent = DashboardRandomDataGenerator.GetRandomInt(10, 100)
            };
        }

        public async Task<GetDiscount> GetDiscountForEdit(NullableIdDto input)
        {
            var output = new GetDiscount();
            var query = _discountRepository
               .GetAll().Where(p => p.Id == input.Id);


            var sections = (from a in query
                            select new CreateDiscountInput
                            {
                                Id = a.Id,
                                Discountable = a.Discountable,
                                UnDiscountable = a.UnDiscountable,
                                QuotationDescription = a.QuotationDescription,
                                Vat = a.Vat
                            }).FirstOrDefault();

            output.discount = sections.MapTo<CreateDiscountInput>();


            return output;
        }

        public async Task CreateOrUpdateDiscount(CreateDiscountInput input)
        {
            if (input.Id != 0)
            {
                await UpdateDiscount(input);
            }
            else
            {
                await CreateDiscount(input);
            }
        }

        public async Task CreateDiscount(CreateDiscountInput input)
        {
            var discount = input.MapTo<Discount>();

            await _discountRepository.InsertAsync(discount);

        }

        public async Task UpdateDiscount(CreateDiscountInput input)
        {
            var discount = input.MapTo<Discount>();

            await _discountRepository.UpdateAsync(discount);

        }

        public async Task<Array> GetLostReasonGraph(GraphInput input)
        {

            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("spGraph_LostReasonGraph", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new LostReasonGraphList
                            {
                                Reason = Convert.ToString(dr["Reason"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Color = Convert.ToString(dr["Color"])
                            });
                return data.ToArray();
            }
        }
        public async Task<Array> GetLeadSummaryGraph(GraphInput input)
        {

            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("spGraph_LeadSummaryGraph", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new GetLeadSummaryGraphList
                            {
                                StageId = Convert.ToInt32(dr["EnqId"]),
                                StageName = Convert.ToString(dr["EnqName"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Color = Convert.ToString(dr["EnqColor"])
                            });
                return data.ToArray();
            }
        }
        public async Task<GetConvertionratio> GetConversionRatioGraph(GraphInput input)
        {
            List<ConversionRatio> cl = new List<ConversionRatio>();
            GetConvertionratio cc = new GetConvertionratio();
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("spGraph_ConversionRatioGraph", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new GetConversionRatioListDto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = Convert.ToString(dr["Name"]),
                                STotal = Convert.ToDecimal(dr["SubmittedTotal"]),
                                WTotal = Convert.ToDecimal(dr["WonTotal"])

                            });

                var datas = data.ToArray();

                int k = 0;
                decimal[] Submitted = new decimal[datas.Select(p => p.Name).ToList().Count()];
                decimal[] Won = new decimal[datas.Select(p => p.Name).ToList().Count()];
                string[] Catageries = new string[datas.Select(p => p.Name).ToList().Count()];

                foreach (var st in datas)
                {
                    Submitted[k] = st.STotal;
                    Won[k] = st.WTotal;
                    Catageries[k] = st.Name;
                    k++;
                }

                cl.Add(new ConversionRatio { Data = Submitted, Name = "Submitted" });
                cl.Add(new ConversionRatio { Data = Won, Name = "Won" });
                cc.Catagries = Catageries;
                cc.ConversionRatio = cl.ToArray();
                return cc;
            }
        }
        public async Task<RecentInquiryClosureList> GetInquiryRecentClosure(RecentInquiryInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_RecentClosureInquiry", con);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@SalesId", input.SalesId);

                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(dt);
                }

                var Listdto = (from DataRow dr in dt.Rows
                               select new RecentInquiryClosureDto
                               {
                                   Week = Convert.ToString(dr["Week"].ToString()),
                                   Month = Convert.ToString(dr["Month"].ToString()),
                                   ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                   SubMmissionId = Convert.ToString(dr["SubMmissionId"]),
                                   InquiryName = Convert.ToString(dr["InquiryName"]),
                                   InquiryId = Convert.ToInt32(dr["InquiryId"]),
                                   Remarks = Convert.ToString(dr["Remarks"]),
                                   CreatorImage = Convert.ToString(dr["CreatorImage"]),
                                   SalesManImage = Convert.ToString(dr["SalesManImage"]),
                                   CoordinatorImage = Convert.ToString(dr["CoordinatorImage"]),
                                   DesignerImage = Convert.ToString(dr["DesignerImage"]),
                                   LastActivity = Convert.ToDateTime(dr["LastActivity"]),
                                   Company = Convert.ToString(dr["Company"]),
                                   Stage = Convert.ToString(dr["Stage"]),
                                   Total = Convert.ToString(dr["Total"]),
                                   StageName = Convert.ToString(dr["StageName"]),
                                   MileStone = Convert.ToString(dr["Milestone"]),
                                   Value = Convert.ToDecimal(dr["Total"]),
                                   Stared = Convert.ToBoolean(dr["Stared"])
                               }).ToList();

                var SubListout = new RecentInquiryClosureList
                {
                    MonthValue = (from r in Listdto where r.Month == "Y" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisweekValue = (from r in Listdto where r.Week == "This Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    NextWeekValue = (from r in Listdto where r.Week == "Next Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisMonthClosureInquiry = (from r in Listdto where r.Month == "Y" select r).ToArray(),
                    ThisWeekClosureInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekClosureInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray(),
                    OverDueValue = (from r in Listdto where r.Week == "Over Due" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    OverDueClosureInquiry = (from r in Listdto where r.Week == "Over Due" select r).ToArray(),
                };

                return SubListout;
            }
        }
        public async Task<RecentInquiryActivityList> GetInquiryRecentActivity(RecentInquiryInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_RecentActivityInquiry", con);
                sqlComm.Parameters.AddWithValue("@SalesId", input.SalesId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(dt);
                }

                var Listdto = (from DataRow dr in dt.Rows
                               select new RecentInquiryClosureDto
                               {
                                   Week = Convert.ToString(dr["Week"].ToString()),
                                   ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                   SubMmissionId = Convert.ToString(dr["SubMmissionId"]),
                                   InquiryName = Convert.ToString(dr["InquiryName"]),
                                   InquiryId = Convert.ToInt32(dr["InquiryId"]),
                                   Remarks = Convert.ToString(dr["Remarks"]),
                                   CreatorImage = Convert.ToString(dr["CreatorImage"]),
                                   SalesManImage = Convert.ToString(dr["SalesManImage"]),
                                   CoordinatorImage = Convert.ToString(dr["CoordinatorImage"]),
                                   DesignerImage = Convert.ToString(dr["DesignerImage"]),
                                   LastActivity = Convert.ToDateTime(dr["LastActivity"]),
                                   Company = Convert.ToString(dr["Company"]),
                                   Stage = Convert.ToString(dr["Stage"]),
                                   Total = Convert.ToString(dr["Total"]),
                                   StageName = Convert.ToString(dr["StageName"]),
                                   MileStone = Convert.ToString(dr["Milestone"]),
                                   Value = Convert.ToDecimal(dr["Value"])
                               });

                var SubListout = new RecentInquiryActivityList
                {
                    OverDueValue = (from r in Listdto where r.Week == "Over Due" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisweekValue = (from r in Listdto where r.Week == "This Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    NextWeekValue = (from r in Listdto where r.Week == "Next Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisWeekActivityInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekActivityInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray(),
                    OverDueActivityInquiry = (from r in Listdto where r.Week == "Over Due" select r).ToArray()
                };

                return SubListout;
            }
        }
        public async Task<GetSalesLeadList> GetSalesLeadSummary()
        {

            List<SalesLeads> cl = new List<SalesLeads>();
            GetSalesLeadList cc = new GetSalesLeadList();
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("PrintCustomers_Cursor", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new SalesLeadListDto
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = Convert.ToString(dr["Name"]),
                                Total = Convert.ToInt32(dr["Total"]),
                                Counts = Convert.ToInt32(dr["Counts"]),
                                SourceName = Convert.ToString(dr["SourceName"])
                            });

                var datas = data.ToArray();

                int j = 0;
                foreach (var st in datas.Select(p => p.Name).ToList().Distinct())
                {
                    double[] arr = new double[datas.Where(p => p.Name == st).Select(p => p.SourceName).ToList().Count()];
                    string[] arrn = new string[datas.Where(p => p.Name == st).Select(p => p.SourceName).ToList().Count()];

                    var status = st;

                    int k = 0;

                    foreach (var d in datas.Where(p => p.Name == st).Select(p => p.SourceName).ToList())
                    {

                        var da = datas.Where(p => p.Name == st && p.SourceName == d).Select(p => p.Counts).FirstOrDefault();
                        if (da > 0)
                        {
                            arr[k] = (int)da;
                        }
                        else
                        {
                            arr[k] = 0;
                        }

                        if (j == 0)
                            arrn[k] = d;

                        k++;
                    }
                    cl.Add(new SalesLeads { Data = arr.ToArray(), Name = st });
                    if (j == 0)
                        cc.Catagries = arrn;

                    j++;
                }
                cc.LeadDevelop = cl.ToArray();
            }
            return cc;
        }
        public List<SliderDataList> GetSalesExecutive(String datainput, bool IsSales, DateTime StartDate,DateTime EndDate)
        {
            var Datas = new List<SliderDataList>();

            string viewquery = "SELECT * FROM [dbo].[View_SliderUser]";

            if (string.IsNullOrEmpty(datainput) == false && IsSales == false)
            {
                viewquery = viewquery + "  WHERE TeamId in(0," + datainput + ")";
            }
            if (string.IsNullOrEmpty(datainput) == false && IsSales == true)
            {
                viewquery = viewquery + "  WHERE Id in(" + datainput + ")";
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
                            Phone = Convert.ToString(dr["PhoneNumber"]),
                            ConversionCount = 0,
                            TConversionCount = 0,
                            Conversionratio = 0,
                            TConversionratio = 0
                        }).ToList();
            int i = 0;
            foreach (var d in data)
            {
                DataTable viewtable2 = new DataTable();
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("spGraph_QuotationRainflow_90", con);
                    sqlComm.Parameters.AddWithValue("@UserId", d.Id);
                    sqlComm.Parameters.AddWithValue("@TeamId", datainput);
                    sqlComm.Parameters.AddWithValue("@StartDate",StartDate);
                    sqlComm.Parameters.AddWithValue("@EndDate",EndDate);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(viewtable2);
                    }
                    con.Close();
                    var datas = (from DataRow dr in viewtable2.Rows
                                 select new GetLeadQuotationGraphListdto
                                 {
                                     Id = Convert.ToInt32(dr["Id"]),
                                     Total = Convert.ToDecimal(dr["Total"]),
                                     Won = Convert.ToBoolean(dr["Won"]),
                                     Submitted = Convert.ToBoolean(dr["Submitted"]),
                                     Lost = Convert.ToBoolean(dr["Lost"]),
                                     Stared = Convert.ToBoolean(dr["Stared"]),
                                     TenderProject = Convert.ToBoolean(dr["TenderProject"]),
                                     //SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]),
                                     QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                     StatusId = Convert.ToInt32(dr["StatusId"]),
                                     Weighted = Convert.ToDecimal(dr["W"])

                                 }).ToList();

                    var WTotalvalue = (from r in datas where r.TenderProject != true && r.Won == true select r.Total).Sum();
                    var WTendervalue = (from r in datas where r.TenderProject == true && r.Won == true select r.Total).Sum();

                    var LTotalvalue = (from r in datas where r.TenderProject != true && r.Lost == true select r.Total).Sum();
                    var LTendervalue = (from r in datas where r.TenderProject == true && r.Lost == true select r.Total).Sum();


                    var WTotalCount = (from r in datas where r.TenderProject != true && r.Won == true select r).Count();
                    var WTenderCount = (from r in datas where r.TenderProject == true && r.Won == true select r).Count();

                    var LTotalCount = (from r in datas where r.TenderProject != true && r.Lost == true select r).Count();
                    var LTenderCount = (from r in datas where r.TenderProject == true && r.Lost == true select r).Count();


                    if ((WTotalvalue + LTotalvalue) > 0)
                    {
                        data[i].Conversionratio = Math.Round((decimal)(WTotalvalue / (WTotalvalue+ LTotalvalue)) * 100, 2);
                    }
                    if ((WTendervalue + LTendervalue) > 0)
                    {
                        data[i].TConversionratio = Math.Round((decimal)(WTendervalue / (WTendervalue + LTendervalue)) * 100, 2);
                    }


                    if ((LTotalCount + WTotalCount) > 0)
                    {
                        data[i].ConversionCount = Math.Round((decimal)(WTotalCount * 100) / (LTotalCount+WTotalCount),2);
                    }


                    if ((WTenderCount + LTenderCount) > 0)
                    {
                        data[i].TConversionCount = Math.Round((decimal)(WTenderCount * 100) / (WTenderCount + LTenderCount),2);
                    }

                    i++;

                }
  
        }
            var Outdata = data.MapTo<List<SliderDataList>>();
            try
            {
                return Outdata;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<SelectDResult> GetDashboardTeam()
        {
            SelectDResult sr = new SelectDResult();
            var team = (from r in _TeamRepository.GetAll() select r).ToArray();

            long userid = (int)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userid
                            select role).FirstOrDefault();

            if (userrole.DisplayName == "Sales Manager" || userrole.DisplayName == "Sales Manager / Sales Executive")
            {
                team = (from r in _TeamRepository.GetAll() where r.SalesManagerId == userid select r).ToArray();
                if (team.Length > 0)
                {
                    var teamlist = (from r in team
                                    join e in UserManager.Users on r.SalesManagerId equals e.Id
                                    select new datadtoes
                                    {
                                        Id = r.Id,
                                        Name = e.FullName + " " + "(" + r.Name + ")",
                                        Photo = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + e.ProfilePictureUrl,
                                        IsSales = false
                                    }).ToList();
                    sr.selectDdata = teamlist.ToArray();
                }
            }
            if (userrole.DisplayName == "Sales Executive")
            {
                team = (from r in _TeamRepository.GetAll()
                        join c in _TeamDetailRepository.GetAll() on r.Id equals c.TeamId
                        into cJoined
                        from c in cJoined.DefaultIfEmpty()
                        where c.SalesmanId == userid
                        select r).ToArray();
                if (team.Length > 0)
                {
                    var teamlist = (from r in team
                                    join e in UserManager.Users on r.SalesManagerId equals e.Id
                                    select new datadtoes
                                    {
                                        Id = (int)userid,
                                        Name = e.FullName + " " + "(" + r.Name + ")",
                                        Photo = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + e.ProfilePictureUrl,
                                        IsSales = true
                                    }).ToList();
                    sr.selectDdata = teamlist.ToArray();
                }
            }
            else
            {
                if (team.Length > 0)
                {
                    var teamlist = (from r in team
                                    join e in UserManager.Users on r.SalesManagerId equals e.Id
                                    select new datadtoes
                                    {
                                        Id = r.Id,
                                        Name = e.FullName + " " + "(" + r.Name + ")",
                                        Photo = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + e.ProfilePictureUrl,
                                        IsSales = false
                                    }).ToList();
                    sr.selectDdata = teamlist.ToArray();
                }
            }

            return sr;
        }
        public async Task<Array> GetSalesLeadBreakdown()
        {

            List<SalesLeads> cl = new List<SalesLeads>();
            GetSalesLeadList cc = new GetSalesLeadList();
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("LeadBreakDown", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new LeadsBreakdown
                            {
                                Count = Convert.ToInt32(dr["Counts"]),
                                Name = Convert.ToString(dr["Source"]),
                            });
                return data.ToArray();
            }
        }
        public async Task<Array> GetLeadQuotationGraph(GraphInput input)
        {

            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("spGraph_QuotationRainflow", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new GetLeadQuotationGraphList
                            {
                                InquiryCount = Convert.ToInt32(dr["InquiryCount"]),
                                Total = Convert.ToDecimal(dr["Total"])
                            }).ToArray();

                foreach(var da in data) {
                    da.STotal = da.Total.ToString("N", new CultureInfo("en-US"));
                }
                return data;
            }
        }
        public List<SliderDataList> GetUserDesignerSlider()
        {
            var Datas = new List<SliderDataList>();

            string selectQuery = "SELECT * FROM [dbo].[View_UserDesignerSlider]";

            long userId = (long)AbpSession.UserId;
            var userrole = (from c in UserManager.Users
                            join urole in _userRoleRepository.GetAll() on c.Id equals urole.UserId
                            join role in _roleManager.Roles on urole.RoleId equals role.Id
                            where urole.UserId == userId
                            select role).FirstOrDefault();

            if (userrole.DisplayName == "Designer")
            {
                selectQuery = "SELECT * FROM [dbo].[View_UserDesignerSlider] WHERE DesignerId= " + userId + " AND UserId= 2";
            }
            else
            {
                selectQuery = "SELECT * FROM [dbo].[View_UserDesignerSlider] WHERE DesignerId= 1 OR UserId=" + userId;
            }

            DataTable viewtable = new DataTable();
            ConnectionAppService db = new ConnectionAppService();
            SqlConnection con = new SqlConnection(db.ConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand(selectQuery, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(viewtable);
            }
            con.Close();
            var data = (from DataRow dr in viewtable.Rows
                        select new SliderDataList
                        {
                            Id = Convert.ToInt32(dr["DesignerId"]),
                            TeamId = Convert.ToInt32(dr["UserId"]),
                            Name = Convert.ToString(dr["DesignerName"]),
                            ProfilePicture = _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + Convert.ToString(dr["ProfilePictureUrl"]),
                            Email = Convert.ToString(dr["EmailAddress"]),
                            Phone = Convert.ToString(dr["PhoneNumber"])
                        });
            var Outdata = data.MapTo<List<SliderDataList>>();
            return Outdata;
        }
        public async Task<RecentInquiryClosureList> GetDesignerRecentClosure(NullableIdDto input)
        {
            long userId = (int)AbpSession.UserId;
            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DesignerRecentClosureInquiry", con);
                sqlComm.Parameters.AddWithValue("@DesignerId", input.Id);
                sqlComm.Parameters.AddWithValue("@UserId", userId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(dt);
                }

                var Listdto = (from DataRow dr in dt.Rows
                               select new RecentInquiryClosureDto
                               {
                                   Week = Convert.ToString(dr["Week"].ToString()),
                                   ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                   SubMmissionId = Convert.ToString(dr["SubMmissionId"]),
                                   InquiryName = Convert.ToString(dr["InquiryName"]),
                                   InquiryId = Convert.ToInt32(dr["InquiryId"]),
                                   Remarks = Convert.ToString(dr["Remarks"]),
                                   CreatorImage = Convert.ToString(dr["CreatorImage"]),
                                   SalesManImage = Convert.ToString(dr["SalesManImage"]),
                                   CoordinatorImage = Convert.ToString(dr["CoordinatorImage"]),
                                   DesignerImage = Convert.ToString(dr["DesignerImage"]),
                                   LastActivity = Convert.ToDateTime(dr["LastActivity"]),
                                   Company = Convert.ToString(dr["Company"]),
                                   Stage = Convert.ToString(dr["Stage"]),
                                   Total = Convert.ToString(dr["Total"]),
                                   StageName = Convert.ToString(dr["StageName"]),
                                   MileStone = Convert.ToString(dr["Milestone"]),
                                   Stared = Convert.ToBoolean(dr["Stared"]),
                                   Month = Convert.ToString(dr["Month"].ToString()),
                                   Value = Convert.ToDecimal(dr["Total"])
                               }).ToList();

                var SubListout = new RecentInquiryClosureList
                {
                    OverDueValue = (from r in Listdto where r.Week == "Over Due" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    MonthValue = (from r in Listdto where r.Month == "Y" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisweekValue = (from r in Listdto where r.Week == "This Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    NextWeekValue = (from r in Listdto where r.Week == "Next Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    OverDueClosureInquiry = (from r in Listdto where r.Week == "Over Due" select r).ToArray(),
                    ThisMonthClosureInquiry = (from r in Listdto where r.Month == "Y" select r).ToArray(),
                    ThisWeekClosureInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekClosureInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray()

                };

                return SubListout;
            }
        }
        public async Task<RecentInquiryActivityList> GetDesignerRecentActivity(NullableIdDto input)
        {
            long userId = (int)AbpSession.UserId;
            ConnectionAppService db = new ConnectionAppService();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DesignerRecentActivityInquiry", con);
                sqlComm.Parameters.AddWithValue("@DesignerId", input.Id);
                sqlComm.Parameters.AddWithValue("@UserId", userId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(dt);
                }

                var Listdto = (from DataRow dr in dt.Rows
                               select new RecentInquiryClosureDto
                               {
                                   Week = Convert.ToString(dr["Week"].ToString()),
                                   ClosureDate = Convert.ToString(dr["ClosureDate"]),
                                   SubMmissionId = Convert.ToString(dr["SubMmissionId"]),
                                   InquiryName = Convert.ToString(dr["InquiryName"]),
                                   InquiryId = Convert.ToInt32(dr["InquiryId"]),
                                   Remarks = Convert.ToString(dr["Remarks"]),
                                   CreatorImage = Convert.ToString(dr["CreatorImage"]),
                                   SalesManImage = Convert.ToString(dr["SalesManImage"]),
                                   CoordinatorImage = Convert.ToString(dr["CoordinatorImage"]),
                                   DesignerImage = Convert.ToString(dr["DesignerImage"]),
                                   LastActivity = Convert.ToDateTime(dr["LastActivity"]),
                                   Company = Convert.ToString(dr["Company"]),
                                   Stage = Convert.ToString(dr["Stage"]),
                                   Total = Convert.ToString(dr["Total"]),
                                   StageName = Convert.ToString(dr["StageName"]),
                                   MileStone = Convert.ToString(dr["Milestone"]),
                                   Value = Convert.ToDecimal(dr["Value"])
                               });

                var SubListout = new RecentInquiryActivityList
                {
                    ThisWeekActivityInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekActivityInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray(),
                    OverDueActivityInquiry = (from r in Listdto where r.Week == "Over Due" select r).ToArray(),
                    OverDueValue = (from r in Listdto where r.Week == "Over Due" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    ThisweekValue = (from r in Listdto where r.Week == "This Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US")),
                    NextWeekValue = (from r in Listdto where r.Week == "Next Week" select r.Value).Sum().ToString("N", new CultureInfo("en-US"))
                };

                return SubListout;
            }
        }
        public async Task<Array> GetDesignerLostReasonGraph(GraphInput input)
        {
            long userId = (int)AbpSession.UserId;
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DesignerLostReasonGraph", con);
                sqlComm.Parameters.AddWithValue("@DesignerId", input.UserId);
                sqlComm.Parameters.AddWithValue("@UserId", userId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new LostReasonGraphList
                            {
                                Reason = Convert.ToString(dr["Reason"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Color = Convert.ToString(dr["Color"])
                            });
                return data.ToArray();
            }
        }
        public async Task<Array> GetDesignerLeadSummaryGraph(GraphInput input)
        {
            long userId = (int)AbpSession.UserId;
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_DesignerLeadSummaryGraph", con);
                sqlComm.Parameters.AddWithValue("@DesignerId", input.UserId);
                sqlComm.Parameters.AddWithValue("@UserId", userId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new GetLeadSummaryGraphList
                            {
                                StageId = Convert.ToInt32(dr["EnqId"]),
                                StageName = Convert.ToString(dr["EnqName"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Color = Convert.ToString(dr["EnqColor"])
                            });
                return data.ToArray();
            }
        }
        public async Task<GetRaindto> GetRainflowGraph(GraphInput input)
        {
            GetRaindto dts = new GetRaindto();
            try
            {
                ConnectionAppService db = new ConnectionAppService();
                DataTable viewtable = new DataTable();
                using (SqlConnection con = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("spGraph_QuotationRainflow_90", con);
                    sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                    sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                    sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                    sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(viewtable);
                    }
                    con.Close();
                    var data = (from DataRow dr in viewtable.Rows
                                select new GetLeadQuotationGraphListdto
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Total = Convert.ToDecimal(dr["Total"]),
                                    Won = Convert.ToBoolean(dr["Won"]),
                                    Submitted = Convert.ToBoolean(dr["Submitted"]),
                                    Lost = Convert.ToBoolean(dr["Lost"]),
                                    Stared = Convert.ToBoolean(dr["Stared"]),
                                    TenderProject = Convert.ToBoolean(dr["TenderProject"]),
                                    //SubmittedDate = Convert.ToDateTime(dr["SubmittedDate"]),
                                    QuotationRefno = Convert.ToString(dr["QuotationRefno"]),
                                    StatusId = Convert.ToInt32(dr["StatusId"]),
                                    Weighted = Convert.ToDecimal(dr["W"])

                                }).ToList();


                    //Count
                    dts.TotalCount = (from r in data where r.TenderProject != true select r).Count();
                    dts.TenderCount = (from r in data where r.TenderProject == true select r).Count();

                    dts.QTotalCount = (from r in data where r.TenderProject != true && r.StatusId > 1 select r).Count();
                    dts.QTenderCount = (from r in data where r.TenderProject == true && r.StatusId > 1 select r).Count();

                    dts.WTotalCount = (from r in data where r.TenderProject != true && r.Won == true select r).Count();
                    dts.WTenderCount = (from r in data where r.TenderProject == true && r.Won == true select r).Count();

                    dts.LTotalCount = (from r in data where r.TenderProject != true && r.Lost == true select r).Count();
                    dts.LTenderCount = (from r in data where r.TenderProject == true && r.Lost == true select r).Count();

                    dts.StrTotalCount = (from r in data where r.TenderProject != true && r.Stared == true select r).Count();
                    dts.StrTenderCount = (from r in data where r.TenderProject == true && r.Stared == true select r).Count();

                    dts.LiveTotalCount = (from r in data where r.TenderProject != true && r.StatusId > 1 && r.Won != true && r.Lost != true select r).Count();
                    dts.LiveTenderCount = (from r in data where r.TenderProject == true && r.StatusId > 1 && r.Won != true && r.Lost != true select r).Count();

                    //Total
                    dts.Totalvalue = (from r in data where r.TenderProject != true select r.Total).Sum();
                    dts.Tendervalue = (from r in data where r.TenderProject == true select r.Total).Sum();

                    dts.QTotalvalue = (from r in data where r.TenderProject != true && r.StatusId > 1 select r.Total).Sum();
                    dts.QTendervalue = (from r in data where r.TenderProject == true && r.StatusId > 1 select r.Total).Sum();

                    dts.WTotalvalue = (from r in data where r.TenderProject != true && r.Won == true select r.Total).Sum();
                    dts.WTendervalue = (from r in data where r.TenderProject == true && r.Won == true select r.Total).Sum();

                    dts.LTotalvalue = (from r in data where r.TenderProject != true && r.Lost == true select r.Total).Sum();
                    dts.LTendervalue = (from r in data where r.TenderProject == true && r.Lost == true select r.Total).Sum();

                    dts.StrTotalvalue = (from r in data where r.TenderProject != true && r.Stared == true select (r.Total * r.Weighted/100)).Sum();
                    dts.StrTendervalue = (from r in data where r.TenderProject == true && r.Stared == true select (r.Total * r.Weighted / 100)).Sum();

                    dts.LiveTotalvalue = (from r in data where r.TenderProject != true && r.StatusId > 1 && r.Won != true && r.Lost != true select r.Total).Sum();
                    dts.LiveTendervalue = (from r in data where r.TenderProject == true && r.StatusId > 1 && r.Won != true && r.Lost != true select r.Total).Sum();

                    decimal? closedvalue = dts.WTotalvalue + dts.WTendervalue + dts.LTotalvalue + dts.LTendervalue;
                    decimal? wonvalue = dts.WTotalvalue + dts.WTendervalue;
                    dts.ConversionRate = 0;
                    if (closedvalue > 0)
                    {
                        dts.ConversionRate = Math.Round((decimal)(wonvalue / closedvalue) * 100,1);
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return dts;

        }
        public async Task<Array> GetMIleStoneTotalGraph(GraphInput input)
        {

            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_MileStoneBasedEnquiryTotal", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new LostReasonGraphList
                            {
                                Reason = Convert.ToString(dr["MileStoneName"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Color = Convert.ToString(dr["MileStoneColor"])
                            });
                return data.ToArray();
            }
        }
        public async Task<GetSalesLeadList> GetSalesPersonOpportunitiesStatus(GraphInput input)
        {

            List<SalesLeads> cl = new List<SalesLeads>();
            GetSalesLeadList cc = new GetSalesLeadList();
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_SalesPersonOpportunitiesGraph", con);
                //SqlCommand sqlComm = new SqlCommand("Sp_SalesPersonOpportunitiesStatus", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new SalesLeadListDto
                            {
                                Id = Convert.ToInt32(dr["SalesPersonId"]),
                                Name = Convert.ToString(dr["SalesPersonName"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Counts = Convert.ToInt32(dr["Counts"]),
                                SourceName = Convert.ToString(dr["StatusName"])
                            });

                var datas = data.ToArray();

                int j = 0;
                foreach (var st in datas.Select(p => p.SourceName).ToList().Distinct())
                {
                    decimal[] arr = new decimal[datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList().Count()];
                    string[] arrn = new string[datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList().Count()];

                    var status = st;

                    int k = 0;

                    foreach (var d in datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList())
                    {

                        var da = datas.Where(p => p.SourceName == st && p.Name == d).Select(p => p.Total).FirstOrDefault();
                        if (da > 0)
                        {
                            arr[k] = da;
                        }
                        else
                        {
                            arr[k] = 0;
                        }

                        if (j == 0)
                            arrn[k] = d;

                        k++;
                    }
                    cl.Add(new SalesLeads { Data = arr.ToArray(), Name = st });
                    if (j == 0)
                        cc.Catagries = arrn;

                    j++;
                }
                cc.LeadDevelop = cl.ToArray();
            }
            return cc;
        }
        public async Task<GetSalesLeadList> GetSalesPersonOpportunitiesStatusAllUser(GraphInput input)
        {
            long SessionUserId = (int)AbpSession.UserId;
            List<SalesLeads> cl = new List<SalesLeads>();
            GetSalesLeadList cc = new GetSalesLeadList();
            ConnectionAppService db = new ConnectionAppService();
            DataTable viewtable = new DataTable();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_SalesPersonOpportunitiesGraphforAllUser", con);
                //SqlCommand sqlComm = new SqlCommand("Sp_SalesPersonOpportunitiesStatus", con);
                sqlComm.Parameters.AddWithValue("@UserId", input.UserId);
                sqlComm.Parameters.AddWithValue("@TeamId", input.TeamId);
                sqlComm.Parameters.AddWithValue("@StartDate", input.StartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", input.EndDate);
                sqlComm.Parameters.AddWithValue("@SessionUserId", SessionUserId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(viewtable);
                }
                con.Close();
                var data = (from DataRow dr in viewtable.Rows
                            select new SalesLeadListDto
                            {
                                Id = Convert.ToInt32(dr["SalesPersonId"]),
                                Name = Convert.ToString(dr["SalesPersonName"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                Counts = Convert.ToInt32(dr["Counts"]),
                                SourceName = Convert.ToString(dr["StatusName"])
                            });

                var datas = data.ToArray();

                int j = 0;
                foreach (var st in datas.Select(p => p.SourceName).ToList().Distinct())
                {
                    decimal[] arr = new decimal[datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList().Count()];
                    string[] arrn = new string[datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList().Count()];

                    var status = st;

                    int k = 0;

                    foreach (var d in datas.Where(p => p.SourceName == st).Select(p => p.Name).ToList())
                    {

                        var da = datas.Where(p => p.SourceName == st && p.Name == d).Select(p => p.Total).FirstOrDefault();
                        if (da > 0)
                        {
                            arr[k] = da;
                        }
                        else
                        {
                            arr[k] = 0;
                        }

                        if (j == 0)
                            arrn[k] = d;

                        k++;
                    }
                    cl.Add(new SalesLeads { Data = arr.ToArray(), Name = st });
                    if (j == 0)
                        cc.Catagries = arrn;

                    j++;
                }
                cc.LeadDevelop = cl.ToArray();
            }
            return cc;
        }

    }
}