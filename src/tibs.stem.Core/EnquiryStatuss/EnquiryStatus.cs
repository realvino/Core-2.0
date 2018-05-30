using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Stagestates;

namespace tibs.stem.EnquiryStatuss
{
    [Table("EnquiryStatus")]
    public class EnquiryStatus : FullAuditedEntity
    {
        [Required]
        public virtual string EnqStatusCode  { get; set; }
        [Required]
        public virtual string EnqStatusName { get; set; }
        public virtual string EnqStatusColor { get; set; }
        public decimal? Percentage { get; set; }

        [ForeignKey("StagestateId")]
        public virtual Stagestate Stagestatess { get; set; }
        public virtual int? StagestateId { get; set; }

    }
}
