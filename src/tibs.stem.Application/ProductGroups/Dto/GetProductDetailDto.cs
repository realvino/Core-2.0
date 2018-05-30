using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    public class GetProductDetailDto
    {
        public int Id { get; set; }
        public int rowId { get; set; }
        public string Name { get; set; }
        public ProductDetailDto[] ProductDetails { get; set; }
    }
}
