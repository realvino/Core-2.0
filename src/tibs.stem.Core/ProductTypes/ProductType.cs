using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductTypes
{
    [Table("ProductType")]
    public class ProductType : FullAuditedEntity
    {
        public virtual string ProductTypeName { get; set; }

        public virtual string ProductTypeCode { get; set; }
    }
}
