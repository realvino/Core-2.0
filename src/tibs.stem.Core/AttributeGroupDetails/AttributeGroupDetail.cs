using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductAttributes;

namespace tibs.stem.AttributeGroupDetails
{
    [Table("AttributeGroupDetail")]
    public class AttributeGroupDetail: FullAuditedEntity
    {
        [ForeignKey("AttributeGroupId")]
        public virtual ProductAttributeGroup AttributeGroups { get; set; }
        public virtual int AttributeGroupId { get; set; }

        [ForeignKey("AttributeId")]
        public virtual ProductAttribute Attributes { get; set; }
        public virtual int AttributeId { get; set; }


    }
}
