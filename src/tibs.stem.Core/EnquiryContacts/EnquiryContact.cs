using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Inquirys;
using tibs.stem.NewCustomerCompanys;

namespace tibs.stem.EnquiryContacts
{
    [Table("EnquiryContacts")]
    public class EnquiryContact : FullAuditedEntity
    {
        [ForeignKey("InquiryId")]
        public virtual Inquiry Inquiry { get; set; }
        public virtual int? InquiryId { get; set; }

        [ForeignKey("ContactId")]
        public virtual NewContact Contacts { get; set; }
        public virtual int? ContactId { get; set; }
    }
}
