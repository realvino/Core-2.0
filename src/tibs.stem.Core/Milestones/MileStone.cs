using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.SourceTypes;

namespace tibs.stem.Milestones
{
    [Table("MileStone")]
    public class MileStone : FullAuditedEntity
    {
        public virtual string MileStoneCode { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual int? RottingPeriod { get; set; }

        [ForeignKey("SourceTypeId")]
        public virtual SourceType SourceTypes { get; set; }
        public virtual int SourceTypeId { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual bool ResetActivity { get; set; }

    }
}
