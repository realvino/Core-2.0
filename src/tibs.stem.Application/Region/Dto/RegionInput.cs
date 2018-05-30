using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Region.Dto
{
    [AutoMapTo(typeof(Regions))]
    public class RegionInput
    {
        public int Id { get; set; }
        public virtual string RegionCode { get; set; }
        public virtual string RegionName { get; set; }
    }
}
