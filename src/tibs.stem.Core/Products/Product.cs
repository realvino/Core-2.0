using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductSpecifications;
using tibs.stem.ProductSubGroups;

namespace tibs.stem.Products
{

    [Table("Product")]
    public class Product : FullAuditedEntity
    {
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string SuspectCode { get; set; }
        public virtual string Gpcode { get; set; }
        public virtual string Description { get; set; }

        [ForeignKey("ProductSpecificationId")]
        public virtual ProductSpecification ProductSpecifications { get; set; }
        public virtual int? ProductSpecificationId { get; set; }
        public decimal? Price { get; set; }
        public virtual int Width { get; set; }
        public virtual int Depth { get; set; }
        public virtual int Height { get; set; }

        [ForeignKey("ProductStateId")]
        public virtual ProductStates ProductState { get; set; }
        public virtual int? ProductStateId { get; set; }

    }
}
