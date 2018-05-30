using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductSpecificationss.Dto
{
   public class GetProductSpecification
    {
        public int Available { get; set; }
        public int Created { get; set; }
        public bool DataMapped { get; set; }
        public ProductSpecificationList ProductSpecification { get; set; }
        public Array productSpecificationDetails { get; set; }
    }
}
