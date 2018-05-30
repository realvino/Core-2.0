using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Products.Dto
{
    public class GetProduct
    {
        public ProductList ProductLists { get; set; }
        public ProductImages[] Images { get; set; }
        public ProductPriceLevelList[] ProductPriceLevelLists { get; set; }
    }

   
}
