using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroups;

namespace tibs.stem.ProductGroups
{

    [Table("ProductGroupDetail")]
    public class ProductGroupDetail : FullAuditedEntity
    {
        [ForeignKey("AttributeGroupId")]
        public virtual ProductAttributeGroup ProductAttributeGroups { get; set; }
        public virtual int AttributeGroupId { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroups { get; set; }
        public virtual int ProductGroupId { get; set; }

        public virtual string Metedata { get; set; }
        public virtual int OrderBy { get; set; }
        public virtual int ReturnBy { get; set; }
    }
}
