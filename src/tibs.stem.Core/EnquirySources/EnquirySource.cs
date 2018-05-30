using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Inquirys;
using tibs.stem.Sources;

namespace tibs.stem.EnquirySources
{
    [Table("EnquirySource")]
    public class EnquirySource : FullAuditedEntity
    {
        [ForeignKey("InquiryId")]
        public virtual Inquiry Inquiry { get; set; }
        public virtual int? InquiryId { get; set; }

        [ForeignKey("SourceId")]
        public virtual Source Source { get; set; }
        public virtual int? SourceId { get; set; }
    }
}
