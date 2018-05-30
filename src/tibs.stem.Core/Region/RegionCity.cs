using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys;

namespace tibs.stem.Region
{
    [Table("RegionCity")]
    public class RegionCity : FullAuditedEntity
    {
        [ForeignKey("RegionId")]
        public virtual Regions Regions { get; set; }
        public virtual int RegionId { get; set; }

        [ForeignKey("CityId")]
        public virtual City Citys { get; set; } 
        public virtual int CityId { get; set; }

    }
}
