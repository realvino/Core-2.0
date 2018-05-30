using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Milestones;

namespace tibs.stem.MileStoness.Dto
{
    [AutoMap(typeof(StageDetails))]
    public class MileStoneDetailInput
    {
        public virtual int MileStoneId { get; set; }
        public virtual int StageId { get; set; }

    }
}
