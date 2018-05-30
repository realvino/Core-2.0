using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributes;

namespace tibs.stem.ProductAttributess.Dto
{
    [AutoMapFrom(typeof(ProductAttribute))]
    public class ProductAttributeListDto
    {
        public int Id { get; set; }
        public virtual string AttributeName { get; set; }
        public virtual string AttributeCode { get; set; }
        public virtual string Imageurl { get; set; }
        public bool IsEdit { get; set; }

    }
}
