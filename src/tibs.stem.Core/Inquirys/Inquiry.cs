using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Companies;
using tibs.stem.Countrys;
using tibs.stem.Departments;
using tibs.stem.Designations;
using tibs.stem.EnquiryStatuss;
using tibs.stem.LeadReasons;
using tibs.stem.LeadStatuss;
using tibs.stem.LineTypes;
using tibs.stem.Locations;
using tibs.stem.Milestones;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.OpportunitySources;
using tibs.stem.Ybafcos;

namespace tibs.stem.Inquirys
{
    [Table("Inquiry")]
    public class Inquiry : FullAuditedEntity
    {
        [ForeignKey("DesignationId")]
        public virtual Designation Designations { get; set; }
        public virtual int? DesignationId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual NewCompany Companys { get; set; }
        public virtual int? CompanyId { get; set; }

        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int? MileStoneId { get; set; }

        [Required]
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string WebSite { get; set; }
        public virtual string Email { get; set; }
        public virtual string MbNo { get; set; }
        public virtual string LandlineNumber { get; set; }
        public virtual string CEmail { get; set; }
        public virtual string CMbNo { get; set; }
        public virtual string CLandlineNumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string SubMmissionId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string DesignationName { get; set; }
        //Extra Information
        public virtual string IpAddress { get; set; }
        public virtual string Browcerinfo { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public virtual int? DepartmentId { get; set; }

        public bool? Junk { get; set; }
        public DateTime? JunkDate { get; set; }

        [ForeignKey("StatusId")]
        public virtual EnquiryStatus EnqStatus { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey("WhyBafcoId")]
        public virtual Ybafco whyBafco { get; set; }
        public virtual int? WhyBafcoId { get; set; }

        [ForeignKey("OpportunitySourceId")]
        public virtual OpportunitySource opportunitySource { get; set; }
        public virtual int? OpportunitySourceId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Locations { get; set; }
        public virtual int? LocationId { get; set; }
        public bool? IsClosed { get; set; }
        public bool? Archieved { get; set; }

        [ForeignKey("LeadStatusId")]
        public virtual LeadStatus LeadStatuss { get; set; }
        public virtual int? LeadStatusId { get; set; }
        public virtual bool Won { get; set; }
        public virtual int Total { get; set; }
        public virtual bool DisableQuotation { get; set; }
        public virtual bool Lost { get; set; }

        public virtual bool DesignerApproval { get; set; }
        public virtual bool RevisionApproval { get; set; }
        public virtual bool Stared { get; set; }
        public virtual decimal Weightedvalue { get; set; }
    }
}
