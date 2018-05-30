using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Citys.Dto
{
    [AutoMapTo(typeof(City))]
    public class CreateCityInput
    {
        public int Id { get; set; }
        [Required]
        public string CityCode { get; set; }
        
        [Required]
        public string CityName { get; set; }
        public virtual int CountryId { get; set; }

    }
}
