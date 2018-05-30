using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Activities;

namespace tibs.stem.Activities.Dto
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityListDto
    {
        public int Id { get; set; } 
        public virtual string ActivityName { get; set; }
        public virtual string ActivityCode { get; set; }

        public virtual string ColorCode { get; set; }
    }
}
