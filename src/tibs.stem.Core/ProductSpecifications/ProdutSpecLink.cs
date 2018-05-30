using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSpecifications;

namespace tibs.stem.ProdutSpecLinks
{
    [Table("ProdutSpecLink")]
    public class ProdutSpecLink: FullAuditedEntity
    {
        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroups { get; set; }
        public virtual int ProductGroupId { get; set; }

        [ForeignKey("ProductSpecificationId")]
        public virtual ProductSpecification ProductSpecifications { get; set; }
        public virtual int? ProductSpecificationId { get; set; }

        [ForeignKey("AttributeGroupId")]
        public virtual ProductAttributeGroup AttributeGroups { get; set; }
        public virtual int AttributeGroupId { get; set; }

        [ForeignKey("AttributeId")]
        public virtual ProductAttribute Attributes { get; set; }
        public virtual int AttributeId { get; set; }
    }
}
