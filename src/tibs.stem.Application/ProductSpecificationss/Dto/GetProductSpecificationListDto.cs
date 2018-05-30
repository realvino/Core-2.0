using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroupss.Dto;

namespace tibs.stem.ProductSpecificationss.Dto
{
    public class GetProductSpecificationListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AttributeGroupDetailListDto[] AttributeGroups{ get; set; }
    }
}
