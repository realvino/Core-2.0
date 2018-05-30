using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductTypes.Dto
{
    [AutoMapTo(typeof(ProductType))]
    public class ProductTypeInputDto
    {
        public int Id { get; set; }
        public virtual string ProductTypeName { get; set; }
        public virtual string ProductTypeCode { get; set; }
    }
}
