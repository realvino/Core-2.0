using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Region.Dto
{
    [AutoMapFrom(typeof(RegionCity))]
    public class RegionCityList
    {
        public virtual int Id { get; set; }
        public virtual int CityId { get; set; }
        public virtual int RegionId { get; set; }
        public virtual string CityName { get; set; }
    }
}
