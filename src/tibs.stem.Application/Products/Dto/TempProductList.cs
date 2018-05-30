using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Products.Dto
{
    
    //[AutoMap(typeof(TemporaryProduct))]
    public class TempProductList : FullAuditedEntityDto
    {

        public new int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string SuspectCode { get; set; }
        public virtual string Gpcode { get; set; }
        public virtual string Description { get; set; }
        public decimal? Price { get; set; }
        public bool Updated { get; set; }
        public virtual int? Width { get; set; }
        public virtual int? Depth { get; set; }
        public virtual int? Height { get; set; }

        public bool IsSelect { get; set; }
        public virtual int? ProductSpecificationId { get; set; }
        public virtual string ProductSpecificationName { get; set; }
        public DateTime? GMTCreationTime { get; set; }
        public virtual string Dimention { get; set; }
        public virtual bool BafcoMade { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual int SalesCount { get; set; }
        public bool IsQuotation { get; set; }
        public string ProductImage { get; set; }
        public string ScreationTime { get; set; }

    }
}
