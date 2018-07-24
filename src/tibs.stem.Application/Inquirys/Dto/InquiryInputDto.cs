using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapTo(typeof(Inquiry))]
    public class InquiryInputDto 
    {
        public int Id { get; set; }
        public virtual int? DesignationId { get; set; }

        public virtual int? CompanyId { get; set; }

        public virtual int? MileStoneId { get; set; }
        public virtual int? LeadStatusId { get; set; }
        public virtual int? TeamId { get; set; }

        [Required]
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

        public virtual string CompanyName { get; set; }

        public virtual string DesignationName { get; set; }

        public virtual long? AssignedbyId { get; set; }

        public virtual int? ContactId { get; set; }

        public bool? Junk { get; set; }

        public DateTime? JunkDate { get; set; }

        public int? StatusId { get; set; }

        public virtual int? CompatitorsId { get; set; }

        public string Summary { get; set; }

        public decimal EstimationValue { get; set; }

        public string Size { get; set; }

        public virtual int? LeadTypeId { get; set; }

        public int[] SourceId { get; set; }
        public DateTime? ClosureDate { get; set; }
        public DateTime? LastActivity { get; set; }
        public virtual int? WhyBafcoId { get; set; }
        public virtual int? OpportunitySourceId { get; set; }
        public virtual int? LocationId { get; set; }
        public virtual string CEmail { get; set; }
        public virtual string CMbNo { get; set; }
        public virtual string CLandlineNumber { get; set; }
        public virtual bool Won { get; set; }
        public virtual int Total { get; set; }
        public virtual bool DisableQuotation { get; set; }
        public virtual bool Lost { get; set; }
        public virtual bool DesignerApproval { get; set; }
        public virtual bool RevisionApproval { get; set; }
        public virtual bool Stared { get; set; }
        public virtual decimal Weightedvalue { get; set; }
        public virtual bool TenderProject { get; set; }
        public virtual string LCNumber { get; set; }

    }
}
