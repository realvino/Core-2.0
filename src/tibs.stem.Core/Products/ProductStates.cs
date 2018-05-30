using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Products
{
    [Table("ProductStates")]
    public class ProductStates : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
