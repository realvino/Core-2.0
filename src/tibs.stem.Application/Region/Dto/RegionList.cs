using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Region.Dto
{
    [AutoMapFrom(typeof(Regions))]
    public class RegionList
    {
        public int Id { get; set; }
        public virtual string RegionCode { get; set; }
        public virtual string RegionName { get; set; }
    }
}
