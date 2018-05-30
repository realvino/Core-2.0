using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EnquiryStatuss;

namespace tibs.stem.Milestones
{
    [Table("MileStoneDetails")]
    public class StageDetails : FullAuditedEntity
    {
        [ForeignKey("MileStoneId")]
        public virtual MileStone MileStones { get; set; }
        public virtual int MileStoneId { get; set; }

        [ForeignKey("StageId")]
        public virtual EnquiryStatus EnquiryStatuss { get; set; }
        public virtual int StageId { get; set; }

    }
}
