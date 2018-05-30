using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Locations.Dto
{
    public class GetLocationInput : PagedAndSortedInputDto, IShouldNormalize  
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting)) 
            {
                Sorting = "LocationCode,LocationName";
            }
        }
    }
}
