using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Departments;
using tibs.stem.Designations;
using tibs.stem.Inquirys;
using tibs.stem.LeadTypes;
using tibs.stem.NewCustomerCompanys;
using tibs.stem.Team;

namespace tibs.stem.EnquiryDetails
{
    [Table("EnquiryDetail")]
    public class EnquiryDetail : FullAuditedEntity
    {
        [ForeignKey("InquiryId")]
        public virtual Inquiry Inquirys { get; set; }
        public virtual int? InquiryId { get; set; }

        [ForeignKey("DesignationId")]
        public virtual Designation Designations { get; set; }
        public virtual int? DesignationId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual NewCompany Companys { get; set; }
        public virtual int? CompanyId { get; set; }

        [ForeignKey("ContactId")]
        public virtual NewContact Contacts { get; set; }
        public virtual int? ContactId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public virtual int? DepartmentId { get; set; } 

        [ForeignKey("AssignedbyId")]
        public virtual User AbpAccountManager { get; set; }
        public virtual long? AssignedbyId { get; set; }

        public DateTime? AssignedbyDate { get; set; }
        public string Summary { get; set; }
        public decimal EstimationValue { get; set; }
        public string Size { get; set; }

        [ForeignKey("LeadTypeId")]
        public virtual LeadType LeadTypes { get; set; }
        public virtual int? LeadTypeId { get; set; }

        [ForeignKey("CompatitorsId")]
        public virtual NewCompany Compatitor { get; set; }
        public virtual int? CompatitorsId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Teams Team { get; set; }
        public virtual int? TeamId { get; set; }
        public DateTime? ClosureDate { get; set; }
        public DateTime? LastActivity { get; set; }

    }
}
