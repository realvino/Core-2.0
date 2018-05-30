using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Locations.Dto
{
    [AutoMapFrom(typeof(Location))]
    public class LocationListDto
    {
        public int Id { get; set; }
        public virtual string LocationName { get; set; }
        public virtual string LocationCode { get; set; }
        public virtual int CityId { get; set; }
        public virtual string CityName { get; set; } 

    }
}
