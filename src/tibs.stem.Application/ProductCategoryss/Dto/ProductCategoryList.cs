using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductCategorys;

namespace tibs.stem.ProductCategoryss.Dto
{
    [AutoMapFrom(typeof(ProductCategory))]
    public class ProductCategoryList
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
