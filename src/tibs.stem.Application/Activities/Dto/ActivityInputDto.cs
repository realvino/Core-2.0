using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Activities.Dto
{
    [AutoMapTo(typeof(Activity))]
    public class ActivityInputDto
    {
        public int Id { get; set; }
        public virtual string ActivityName { get; set; }
        public virtual string ActivityCode { get; set; }

        public virtual string ColorCode { get; set; }
    }
}
