using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
     
   [AutoMapFrom(typeof(Inquiry))]
    public class InquiryClosureDateListDto
    {
        public virtual int? DesignationId { get; set; }

        public virtual int? CompanyId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual int? MileStoneId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual string WebSite { get; set; }

        public virtual string Email { get; set; }

        public virtual string MbNo { get; set; }

        public virtual string LandlineNumber { get; set; }

        public virtual string Remarks { get; set; }

        public virtual string SubMmissionId { get; set; }

        public virtual string IpAddress { get; set; }

        public virtual string Browcerinfo { get; set; }

        public virtual int? DepartmentId { get; set; }
        public int[] SourceId { get; set; }
        public string MileStoneName { get; set; }
        public string DesignationName { get; set; }
        public string CompanyName { get; set; }
        public int Id { get; set; }
        public string DepartmentName { get; internal set; }
        public long CreatorUserId { get; internal set; }
        public DateTime CreationTime { get; set; }
        public string SCreationTime { get; set; }
        public DateTime CreationOrModification { get; set; }
        public string ProfilePicture { get; internal set; }
        public string UserName { get; set; }
        public virtual long? AssignedbyId { get; set; }
        public string AssignedTime { get; set; }
        public bool? Junk { get; set; }
        public int? EnqStageId { get; set; }
        public string EnqStageColor { get; set; }
        public string EnqStageName { get; set; }
        public decimal StagePercent { get; set; }
        public virtual int? CompatitorsId { get; set; }
        public virtual string CompatitorName { get; set; }
        public string Summary { get; set; }
        public int? SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public decimal EnqTotalValue { get; set; }
        public decimal EnqWeightValue { get; set; }
        public string Size { get; set; }
        public virtual int? LeadTypeId { get; set; }
        public virtual string LeadTypeName { get; set; }
        public DateTime? JunkDate { get; set; }
        public ActivityColor[] ActivityColors { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual int? QuotationId { get; set; }
        public QuotationLists[] Quotations { get; set; }
        public virtual int? QuotationCount { get; set; }
        public virtual int? TeamId { get; set; }
        public string Team { get; set; }
        public virtual bool? IsOptional { get; set; }
        public virtual string InquiryName { get; set; }
        public string CreatedBy { get; internal set; }
        public virtual string ContactName { get; set; }
        public virtual bool Approved { get; set; }
        public DateTime? LastActivity { get; set; }
        public DateTime ClosureDate { get; set; }
        public virtual bool? IsExpire { get; set; }
        public int? Percentage { get; set; }
        public virtual int? WhyBafcoId { get; set; }
        public virtual string WhyBafcoName { get; set; }
        public virtual int? OpportunitySourceId { get; set; }
        public virtual string OpportunitySourceName { get; set; }
        public string SalesPersonImage { get; set; }
        public long? SalesManagerId { get; set; }
        public long? DesignerId { get; set; }
        public string Designer { get; set; }
        public string DesignerImage { get; set; }
        public long? CoordinatorId { get; set; }
        public string Coordinator { get; set; }
        public string CoordinatorImage { get; set; }
        public string Revision { get; set; }
        public string Quotationshtml { get; set; }
        public int Starred { get; set; }
        public decimal Weightedvalue { get; set; }
        public int Tender { get; set; }
        public string Amount { get; set; }
        public string WeigntedAmount { get; set; }

    }
}
