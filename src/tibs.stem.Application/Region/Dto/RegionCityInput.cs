using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Region.Dto
{
    [AutoMapTo(typeof(RegionCity))]
    public class RegionCityInput
    {
        public int Id { get; set; }
        public virtual int RegionId { get; set; }
        public virtual int CityId { get; set; }
    }
}
