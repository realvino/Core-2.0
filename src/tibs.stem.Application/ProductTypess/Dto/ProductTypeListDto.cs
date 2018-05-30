using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductTypes.Dto
{
    [AutoMapFrom(typeof(ProductType))]
    public class ProductTypeListDto : FullAuditedEntityDto
    {
        public int Id { get; set; }
        public virtual string ProductTypeName { get; set; }
        public virtual string ProductTypeCode { get; set; }
    }

}
