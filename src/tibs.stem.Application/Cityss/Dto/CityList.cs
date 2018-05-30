using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Citys.Dto
{
    [AutoMapFrom(typeof(City))]
    public class CityList : FullAuditedEntityDto
    {
        public virtual int Id { get; set; } 
        public virtual string CityCode { get; set; }
        public virtual string CityName { get; set; }
        public virtual string CountryName { get; set; }
        public virtual int CountryId { get; set; }  

    }
}
