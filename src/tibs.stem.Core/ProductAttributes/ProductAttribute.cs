using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductAttributes
{
    public class ProductAttribute : FullAuditedEntity
    {
        public virtual string AttributeName { get; set; }
        public virtual string AttributeCode { get; set; }
        public virtual string Imageurl { get; set; }
        public virtual string Description { get; set; }
    }
}