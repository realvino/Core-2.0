using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Region
{
    [Table("Region")]
    public class Regions : FullAuditedEntity
    {
        [Required]
        public virtual string RegionCode { get; set; }
        [Required]
        public virtual string RegionName { get; set; }
    }
}
