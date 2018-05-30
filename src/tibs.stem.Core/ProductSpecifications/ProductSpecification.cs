using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductFamilys;
using tibs.stem.ProductGroups;

namespace tibs.stem.ProductSpecifications
{
    [Table("ProductSpecification")]
    public class ProductSpecification: FullAuditedEntity
    {
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; } 

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroups { get; set; }
        public virtual int? ProductGroupId { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Reset { get; set; }
        public virtual bool BafcoMade { get; set; }

    }
}
