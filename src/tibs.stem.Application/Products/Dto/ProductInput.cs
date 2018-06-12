using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Products;

namespace tibs.stem.Products.Dto
{
    [AutoMapTo(typeof(Product))]
    public class ProductInput
    {
        public int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string SuspectCode { get; set; }
        public virtual string Gpcode { get; set; }
        public virtual string Description { get; set; }
        public virtual int? ProductSpecificationId { get; set; }
        public decimal? Price { get; set; }
        public virtual int Width { get; set; }
        public virtual int Depth { get; set; }
        public virtual int Height { get; set; }
        public virtual int? ProductStateId { get; set; }
        public virtual int? RefId { get; set; }

    }
}
