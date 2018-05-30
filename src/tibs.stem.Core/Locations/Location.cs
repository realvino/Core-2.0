using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys; 

namespace tibs.stem.Locations
{
    [Table("Location")]
    public class Location : FullAuditedEntity
    {
        [Required]
        public virtual string LocationName { get; set; }

        [Required]
        public virtual string LocationCode { get; set; }

        [ForeignKey("CityId")]
        public virtual City citys { get; set; }
        public virtual int CityId { get; set; }
 
    }
}
