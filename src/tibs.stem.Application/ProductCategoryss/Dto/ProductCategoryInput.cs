using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductCategorys;

namespace tibs.stem.ProductCategoryss.Dto
{
    [AutoMap(typeof(ProductCategory))]
    public class ProductCategoryInput
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
    }
}
