using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.ProductCategorys;
using tibs.stem.ProductFamilys;

namespace tibs.stem.ProductGroups
{
    [Table("ProductGroup")]
    public class ProductGroup : FullAuditedEntity
    {
        [Required]
        public virtual string ProductGroupName { get; set; }

        [ForeignKey("FamilyId")]
        public virtual ProductFamily prodFamily { get; set; }
        public virtual int? FamilyId { get; set; }

        public virtual string AttributeData { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategorys { get; set; }
        public virtual int? ProductCategoryId { get; set; }
    }
}
