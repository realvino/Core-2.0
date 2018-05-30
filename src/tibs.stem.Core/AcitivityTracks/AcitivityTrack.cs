using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Activities;
using tibs.stem.Inquirys;
using tibs.stem.NewCustomerCompanys;

namespace tibs.stem.AcitivityTracks
{
    [Table("AcitivityTrack")]
    public class AcitivityTrack : FullAuditedEntity
    {

        public virtual string Title { get; set; }
        public virtual string Message { get; set; }
        public virtual string PreviousStatus { get; set; }
        public virtual string CurrentStatus { get; set; }

        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
        public virtual int ActivityId { get; set; }

        [ForeignKey("EnquiryId")]
        public virtual Inquiry Enquiry { get; set; }
        public virtual int EnquiryId { get; set; }

        [ForeignKey("ContactId")]
        public virtual NewContact NewContacts { get; set; }
        public virtual int? ContactId { get; set; }

    }
}
