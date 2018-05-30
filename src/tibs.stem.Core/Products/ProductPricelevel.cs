using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PriceLevels;

namespace tibs.stem.Products
{
    [Table("ProductPricelevel")]
    public class ProductPricelevel : FullAuditedEntity
    {
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
        public virtual int ProductId { get; set; }

        [ForeignKey("PriceLevelId")]
        public virtual PriceLevel PriceLevels { get; set; }
        public virtual int PriceLevelId { get; set; }

        public virtual Decimal  Price { get; set; }
    }
}
