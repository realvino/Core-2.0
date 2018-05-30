using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributes;

namespace tibs.stem.ProductAttributeGroups
{
    [Table("AttributeGroup")]

    public class ProductAttributeGroup : FullAuditedEntity
    {
        public virtual string AttributeGroupName { get; set; }
        public virtual string AttributeGroupCode { get; set; }

    }
}
