using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSpecifications;

namespace tibs.stem.ProductSpecificationDetails
{
    [Table("ProductSpecificationDetail")]
    public class ProductSpecificationDetail: FullAuditedEntity
    {
        [ForeignKey("AttributeGroupId")]
        public virtual ProductAttributeGroup ProductAttributeGroups { get; set; }
        public virtual int AttributeGroupId { get; set; }

        [ForeignKey("ProductSpecificationId")]
        public virtual ProductSpecification ProductSpecifications { get; set; }
        public virtual int ProductSpecificationId { get; set; }

        //public virtual int OrderBy { get; set; }
        //public virtual int ReturnBy { get; set; }

    }
}
