﻿using Abp.Application.Services.Dto;
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
            ){
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
                                Discountable=a.Discountable,
                                UnDiscountable=a.UnDiscountable,
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
                                   MileStone = Convert.ToString(dr["Milestone"])
                               }).ToList();

                var SubListout = new RecentInquiryClosureList
                {
                    ThisWeekClosureInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekClosureInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray()
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
                                   MileStone = Convert.ToString(dr["Milestone"])
                               });

                var SubListout = new RecentInquiryActivityList
                {
                    ThisWeekActivityInquiry = (from r in Listdto where r.Week == "This Week" select r).ToArray(),
                    NextWeekActivityInquiry = (from r in Listdto where r.Week == "Next Week" select r).ToArray(),
                    OverDueActivityInquiry = (from r in Listdto where r.Week == "Over Due" select r).ToArray()
                };

                return SubListout;
            }
        }
        public List<SliderDataList> GetSalesExecutive(String datainput,bool IsSales)
        {
            var Datas = new List<SliderDataList>();

            string viewquery = "SELECT * FROM [dbo].[View_SliderUser]";

            if (string.IsNullOrEmpty(datainput) == false && IsSales == false)
            {
                    viewquery = viewquery + "  WHERE TeamId in(0,"+ datainput + ")";
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
                            Phone = Convert.ToString(dr["PhoneNumber"])
                        });
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
                        into cJoined from c in cJoined.DefaultIfEmpty()
                        where c.SalesmanId == userid select r).ToArray();
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

    }

}