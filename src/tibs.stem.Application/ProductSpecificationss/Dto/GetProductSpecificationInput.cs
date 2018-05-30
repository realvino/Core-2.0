using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductSpecificationss.Dto
{
    public class GetProductSpecificationInput
    {
        public string Filter { get; set; }

    }
    public class GetProductSpecificationDetailInput
    {
        public int ProductSpecificationId { get; set; }
    }

}
