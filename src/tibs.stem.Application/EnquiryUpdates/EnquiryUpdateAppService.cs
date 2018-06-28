using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.AcitivityTracks;
using tibs.stem.EnquiryDetails;
using tibs.stem.EnquiryStatuss;
using tibs.stem.Inquirys;
using tibs.stem.Inquirys.Dto;
using tibs.stem.Milestones;
using tibs.stem.NewCompanyContacts.Dto;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.Quotations;
using tibs.stem.Team;
using tibs.stem.TeamDetails;

namespace tibs.stem.EnquiryUpdates
{
    public class EnquiryUpdateAppService : stemAppServiceBase, IEnquiryUpdateAppService
    {
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<MileStone> _milestoneRepository;
        private readonly IRepository<NewCompany> _NewCompanyRepository;
        private readonly IRepository<EnquiryDetail> _enquiryDetailRepository;
        private readonly IRepository<AcitivityTrack> _acitivityTrackRepository;
        private readonly IRepository<NewContact> _NewContactRepository;
        private readonly IRepository<Quotation> _quotationRepository;
        private readonly IRepository<Teams> _teamsRepository;
        private readonly IRepository<StageDetails> _StageDetailRepository;
        private readonly IRepository<EnquiryStatus> _EnquiryStatusRepository;
        private readonly IRepository<TeamDetail> _teamDetailRepository;

        public EnquiryUpdateAppService(
            IRepository<Inquiry> inquiryRepository,                                       
            IRepository<MileStone> milestoneRepository,
            IRepository<NewCompany> NewCompanyRepository, 
            IRepository<EnquiryDetail> enquiryDetailRepository,
            IRepository<AcitivityTrack> acitivityTrackRepository,
            IRepository<NewContact> NewContactRepository,
            IRepository<StageDetails> StageDetailRepository,
            IRepository<EnquiryStatus> EnquiryStatusRepository,
            IRepository<Quotation> quotationRepository,
            IRepository<TeamDetail> teamDetailRepository,
        IRepository<Teams> teamsRepository)
        {
            _inquiryRepository = inquiryRepository;
            _milestoneRepository = milestoneRepository;
            _NewCompanyRepository = NewCompanyRepository;
            _enquiryDetailRepository = enquiryDetailRepository;
            _acitivityTrackRepository = acitivityTrackRepository;
            _NewContactRepository = NewContactRepository;
            _quotationRepository = quotationRepository;
            _teamsRepository = teamsRepository;
            _StageDetailRepository = StageDetailRepository;
            _EnquiryStatusRepository = EnquiryStatusRepository;
            _teamDetailRepository = teamDetailRepository; 
        }


        public int createORupdateInquiry(EnquiryUpdateInputDto input)
        {
            int id = 0;
            var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.Id).FirstOrDefault();

            var title = (from c in _EnquiryStatusRepository.GetAll()
                         join d in _StageDetailRepository.GetAll() on c.Id equals d.StageId
                         where d.MileStones.MileStoneName == input.UpdateStatusName
                         select c).ToArray();


            if (input.UpdateStatusName == "Lead" && enquiryDetail.DepartmentId == null)
            {
                if (enquiryDetail.CompanyId > 0)
                {
                    var comp = _NewCompanyRepository.GetAll().Where(p => p.Id == enquiryDetail.CompanyId).FirstOrDefault();
                    if(comp.AccountManagerId > 0)
                    {
                        long AccountManagerId = (long)comp.AccountManagerId;
                        var teamDetail = _teamDetailRepository.GetAll().Where(p => p.SalesmanId == AccountManagerId).FirstOrDefault();
                        var teams = _teamDetailRepository.GetAll().Where(p => p.SalesmanId == AccountManagerId).Select(p =>p.Team).FirstOrDefault();
                        if (teamDetail != null && teams != null)
                        {
                            var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                            enqdetail.TeamId = teamDetail.TeamId;
                            enqdetail.DepartmentId = teams.DepartmentId;
                            _enquiryDetailRepository.UpdateAsync(enqdetail);

                            UpdateEnquiry(input);
                        }
                        else
                        {
                            var team = _teamsRepository.GetAll().Where(p => p.SalesManagerId == AccountManagerId).FirstOrDefault();
                            if (team != null)
                            {
                                var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                                enqdetail.TeamId = team.Id;
                                enqdetail.DepartmentId = team.DepartmentId;
                                _enquiryDetailRepository.UpdateAsync(enqdetail);

                                UpdateEnquiry(input);
                            }
                        }
                    }
                    else
                    {
                        id = 1;
                    }

                }

            }
            else if(title.Count() >1 && input.UpdateStatusName != "Assigned")
            {
                    id = 2;
            }
            else if(title.Count() > 0 && input.UpdateStatusName == "Assigned")
            {
                if(input.StageId > 0)
                {
                }
                else
                {
                    input.StageId = title[0].Id;
                }
                UpdateEnquiry(input);
            }
            else
            {
                UpdateEnquiry(input);
            }
            return id;
        }


        public void CreateActivityDefault(EnquiryUpdateInputDto input)
        {
            EnqActCreate actinput = new EnqActCreate()
            {
                EnquiryId = input.Id,
                PreviousStatus = input.CurrentStatusName,
                CurrentStatus = input.UpdateStatusName,
                ActivityId = 7,
                Title = "Status Update",
                Message = "Ticket Status Updated Successfully"
            };
            var Activity = actinput.MapTo<AcitivityTrack>();
            _acitivityTrackRepository.InsertAsync(Activity);
        }
       public async Task createORupdateInquiryJunk(EnquiryJunkUpdateInputDto input)
       {
            var inquiry = (from r in _inquiryRepository.GetAll() where r.Id == input.Id select r).FirstOrDefault();
            var inquirys = inquiry.MapTo<Inquiry>();
            inquirys.Junk = input.Junk;
            inquirys.JunkDate = DateTime.Now;
           await _inquiryRepository.UpdateAsync(inquirys);

        }
        public async Task ReverseJunk(EnquiryJunkUpdateInputDto input)
        {
            var inquiry = (from r in _inquiryRepository.GetAll() where r.Id == input.Id select r).FirstOrDefault();
            var inquirys = inquiry.MapTo<Inquiry>();
            inquirys.Junk = null;
            inquirys.JunkDate = null;
            await _inquiryRepository.UpdateAsync(inquirys);

        }

        public void UpdateEnquiry(EnquiryUpdateInputDto input)
        {
            //string[] values = input.UpdateStatusName.Split('(').Select(sValue => sValue).ToArray();
           
            var Status = (from r in _milestoneRepository.GetAll() where r.MileStoneName == input.UpdateStatusName select r).FirstOrDefault();
            var inquiry = (from r in _inquiryRepository.GetAll() where r.Id == input.Id select r).FirstOrDefault();
            var inquirys = inquiry.MapTo<Inquiry>();
            inquirys.MileStoneId = Status.Id;
            
            inquirys.StatusId = input.StageId;
            if (input.UpdateStatusName == "Assigned")
            {
                inquirys.OpportunitySourceId = 1;
            }
            _inquiryRepository.UpdateAsync(inquirys);

            if (Status.ResetActivity == true)
            {
                var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.Id).FirstOrDefault();

                var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                enqdetail.LastActivity = null;
                _enquiryDetailRepository.UpdateAsync(enqdetail);
            }
        }

        public int CheckEnquiryStages(EnquiryStatusUpdateInput input)
        {
            var title = (from c in _EnquiryStatusRepository.GetAll()
                         join d in _StageDetailRepository.GetAll() on c.Id equals d.StageId
                         where d.MileStones.Id == input.StatusId
                         select c).ToArray();

            return title.Count();
        }
        public void EnquiryStatusUpdate(EnquiryStatusUpdateInput input)
        {
            var inquiry = (from r in _inquiryRepository.GetAll() where r.Id == input.EnquiryId select r).FirstOrDefault();
            var CurrentStatusId = inquiry.MileStoneId;
            var UpdateStatusName = (from r in _milestoneRepository.GetAll() where r.Id == input.StatusId select r).FirstOrDefault();

            var inquirys = inquiry.MapTo<Inquiry>();
            inquirys.MileStoneId = input.StatusId;
            inquirys.StatusId = input.StageId;
            _inquiryRepository.UpdateAsync(inquirys);

            if (input.LastActivity != null)
            {
                var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == input.EnquiryId).FirstOrDefault();
                var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                 enqdetail.LastActivity = input.LastActivity;
                _enquiryDetailRepository.UpdateAsync(enqdetail);
            }
        }

        public void QuotationStatusUpdate(QuotationStatusUpdateInput input)
        {
            var quotation = (from r in _quotationRepository.GetAll() where r.Id == input.QuotationId select r).FirstOrDefault();
            var quotations = quotation.MapTo<Quotation>();
                quotations.MileStoneId = input.StatusId;
                quotations.StageId = input.StageId;
            if(input.StatusId == 9)
            {
                quotations.Negotiation = true;
                quotations.NegotiationDate = DateTime.Now;
            }
               _quotationRepository.UpdateAsync(quotations);

            if (input.LastActivity != null && quotation != null)
            {
                var enquiryDetail = _enquiryDetailRepository.GetAll().Where(p => p.InquiryId == quotation.InquiryId).FirstOrDefault();
                var enqdetail = enquiryDetail.MapTo<EnquiryDetail>();
                 enqdetail.LastActivity = input.LastActivity;
                _enquiryDetailRepository.UpdateAsync(enqdetail);
            }
        }
        public void ContactUpdate(ContactUpdateInputDto input)
        {
            var contacts = (from r in _NewContactRepository.GetAll() where r.Id == input.ContactId select r).FirstOrDefault();
            var contact = contacts.MapTo<NewContact>();
                contact.NewCompanyId = input.CompanyId;
               _NewContactRepository.UpdateAsync(contact);
        }

        public void GetUpdateQuotation(UpdateQuotationInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_UpdateQuotationMileStone", conn);
                sqlComm.Parameters.AddWithValue("@InquiryId", input.EnquiryId);
                sqlComm.Parameters.AddWithValue("@MileStoneId", input.MilestoneId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }
        public void UpdateEnquiryClosureDate(ClosureUpdateDateInput input)
        {
            var inquiryDetail = (from r in _enquiryDetailRepository.GetAll() where r.InquiryId == input.InquiryId select r).FirstOrDefault();
            var inquirys = inquiryDetail.MapTo<EnquiryDetail>();
            if (inquiryDetail != null)
            {
                DateTime myDate = DateTime.Parse(input.UpdateDate);
                inquirys.ClosureDate = myDate;
                _enquiryDetailRepository.UpdateAsync(inquirys);
            }
        }
        public void ReverseClosed(NullableIdDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_ClosedQuotation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InquiryId", SqlDbType.VarChar).Value = input.Id;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = 2;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        public void InquiryClosed(int Id)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_ClosedQuotation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InquiryId", SqlDbType.VarChar).Value = Id;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = 1;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public async Task UpdateNegotiationQuotation(NullableIdDto input)
        {
            var quotation = (from r in _quotationRepository.GetAll() where r.Id == input.Id select r).FirstOrDefault();
            var quotations = quotation.MapTo<Quotation>();
            quotations.Negotiation = true;
            quotations.NegotiationDate = DateTime.Now;
            await _quotationRepository.UpdateAsync(quotations);
        }
    }
    public class EnquiryUpdateInputDto
    {
        public int Id { get; set; }
        public string UpdateStatusName { get; set; }
        public string CurrentStatusName { get; set; }
        public int? StageId { get; set; }

    }
    public class ClosureUpdateDateInput
    {
        public int InquiryId { get; set; }
        public string UpdateDate { get; set; }
    }
    public class EnquiryStatusUpdateInput
    {
        public int EnquiryId { get; set; }
        public int StatusId { get; set; }
        public int StageId { get; set; }
        public DateTime? LastActivity { get; set; }

    }
    public class QuotationStatusUpdateInput
    {
        public int QuotationId { get; set; }
        public int StatusId { get; set; }
        public int StageId { get; set; }
        public DateTime? LastActivity { get; set; }

    }
    public class ContactUpdateInputDto
    {
        public int CompanyId { get; set; }
        public int ContactId { get; set; }

    }
    public class EnquiryJunkUpdateInputDto
    {
        public int Id { get; set; }
        public bool? Junk { get; set; }
    }
    public class UpdateQuotationInput
    {
        public int EnquiryId { get; set; }
        public int MilestoneId { get; set; }
    }
}
