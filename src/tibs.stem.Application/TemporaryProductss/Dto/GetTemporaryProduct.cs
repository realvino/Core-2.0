using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.TemporaryProductss.Dto
{
    public class GetTemporaryProduct
    {
        public TemporaryProductList TemporaryProductLists { get; set; }
        public TemporaryProdImages[] TempProductImages { get; set; }
    }
    public class TemporaryProdImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
