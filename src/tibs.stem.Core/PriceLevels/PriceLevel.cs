using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.PriceLevels
{
    [Table("PriceLevel")]
    public class PriceLevel : FullAuditedEntity
    {
        public virtual string PriceLevelCode { get; set; }
        public virtual string PriceLevelName { get; set; }
        public virtual bool DiscountAllowed { get; set; }
    }
}
