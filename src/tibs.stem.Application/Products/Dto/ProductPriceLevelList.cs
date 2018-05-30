using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Products.Dto
{
   public class ProductPriceLevelList
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int PriceLevelId { get; set; }
        public virtual string PriceLeveName { get; set; }
        public virtual decimal Price{ get; set; }
    }
}
