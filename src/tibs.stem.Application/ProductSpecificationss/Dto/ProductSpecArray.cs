using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductSpecificationss.Dto
{
    public class ProductSpecArray
    {
        public int ProductSpecId { get; set; }
        public int ProductGroupId { get; set; }
        public AttributeArray[] ArributeGroupSelect { get; set; }
    }
    public class AttributeArray
    {
        public int AttributeGroupId { get; set; }
        public int[] AttributeId { get; set; }
    }
}
