using Abp.AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Products.Dto
{
    [AutoMapTo(typeof(ProductPricelevel))]
    public  class ProductPriceLevelInput 
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int PriceLevelId { get; set; }

        public virtual Decimal Price { get; set; }
    }
}
