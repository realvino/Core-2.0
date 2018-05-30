using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductImageUrls;

namespace tibs.stem.Products.Dto
{
    [AutoMapTo(typeof(ProductImageUrl))]
    public class ProductImagesInput
    {
        public int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string ImageUrl { get; set; }
    }

}
