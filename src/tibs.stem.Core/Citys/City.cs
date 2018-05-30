using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Countrys; 

namespace tibs.stem.Citys 
{
    [Table("City")] 
    public class City : FullAuditedEntity
    {
        [Required]
        public virtual string CityName { get; set; }

        [Required]
        public virtual string CityCode { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public virtual int CountryId { get; set; }

    }
}
