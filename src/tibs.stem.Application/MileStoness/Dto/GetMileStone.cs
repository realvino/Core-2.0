using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.MileStoness.Dto
{
    public class GetMileStone
    {
        public MileStoneList MileStones { get; set; }
        public SourceTypees[] SourceTyped { get; set; }
        public StageDetailListDto[] Stages { get; set; }
    }
    public class SourceTypees
    {
        public virtual int SourceTypeId { get; set; }
        public virtual string SourceTypeName { get; set; }
    }
}
