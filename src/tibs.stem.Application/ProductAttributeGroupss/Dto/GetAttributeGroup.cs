using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductAttributeGroupss.Dto
{
    public class GetAttributeGroup
    {
        public CreateAttributeGroupInput attributeGroup { get; set; }
        public AttributeGroupDetailListDto[] attributeGroupDetails { get; set; }
    }
}
