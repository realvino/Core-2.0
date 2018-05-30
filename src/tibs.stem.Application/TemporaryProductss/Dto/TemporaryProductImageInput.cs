using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TemporaryProducts;

namespace tibs.stem.TemporaryProductss.Dto
{
    [AutoMapTo(typeof(TemporaryProductImage))]
    public class TemporaryProductImageInput
    {
        public int Id { get; set; }
        public virtual int TemporaryProductId { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}
