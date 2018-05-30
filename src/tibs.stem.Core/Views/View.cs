using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.Views
{
    [Table("View")]
    public class View : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public virtual bool IsEditable { get; set; }
        public virtual bool IsEnquiry { get; set; }

        [ForeignKey("QuotationStatusId")]
        public virtual QuotationStatus Quotationstatus { get; set; }
        public virtual int? QuotationStatusId { get; set; }

        [ForeignKey("AllPersonId")]
        public virtual User AllPerson { get; set; }
        public virtual long? AllPersonId { get; set; }

        public virtual int? GraterAmount { get; set; }
        public virtual int? LessAmount { get; set; }

        public virtual int? EnqStatusId { get; set; }
        public virtual string UserIds { get; set; }
        public virtual string QuotationCreateBy { get; set; }
        public virtual string QuotationStatus { get; set; }
        public virtual string Salesman { get; set; }
        public virtual string InquiryCreateBy { get; set; }
        public virtual string PotentialCustomer { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual string EnquiryStatus { get; set; }
        public virtual string TeamName { get; set; }
        public virtual string Coordinator { get; set; }
        public virtual string Designer { get; set; }
        public virtual string DesignationName { get; set; }
        public virtual string Emirates { get; set; }
        public virtual string DepatmentName { get; set; }
        public virtual string Categories { get; set; }
        public virtual string Status { get; set; }
        public virtual string WhyBafco { get; set; }
        public virtual string Probability { get; set; }
        public virtual string QuotationCreation { get; set; }
        public virtual string InquiryCreation { get; set; }
        public virtual string ClosureDate { get; set; }
        public virtual string LastActivity { get; set; }
        public virtual string StatusForQuotation { get; set; }



        [ForeignKey("DateFilterId")]
        public virtual DateFilter DateFilters { get; set; }
        public virtual int? DateFilterId { get; set; }

        [ForeignKey("ClosureDateFilterId")]
        public virtual DateFilter ClsDateFilters { get; set; }
        public virtual int? ClosureDateFilterId { get; set; }


        [ForeignKey("LastActivityDateFilterId")]
        public virtual DateFilter LADateFilters { get; set; }
        public virtual int? LastActivityDateFilterId { get; set; }

    }
}
