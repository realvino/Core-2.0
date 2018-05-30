using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Locations.Dto
{
    [AutoMapTo(typeof(Location))]
    public class LocationInputDto
    {
        public int Id { get; set; }
        public virtual string LocationName { get; set; }
        public virtual string LocationCode { get; set; }
        public virtual int CityId { get; set; } 

    }
}
