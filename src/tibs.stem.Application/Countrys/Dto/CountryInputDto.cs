using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Countrys.Dto
{
    [AutoMapTo(typeof(Country))]
    public class CountryInputDto 
    {
        public int Id { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string ISDCode { get; set; }

    }
}
