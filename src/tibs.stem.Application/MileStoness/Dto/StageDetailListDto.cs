using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.MileStoness.Dto
{
    public class StageDetailListDto
    {
        public virtual int Id { get; set; }
        public virtual int StageId { get; set; }
        public virtual string StageName { get; set; }
        public virtual decimal? Value { get; set; }
    }
}
