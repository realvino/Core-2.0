using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TemporaryProducts;

namespace tibs.stem.TemporaryProductss.Dto
{
    [AutoMap(typeof(TemporaryProduct))]
    public class TemporaryProductList : FullAuditedEntityDto
    {
        public  new int Id { get; set; }
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
        public string ProductSpecificationName { get; internal set; }
        public string ScreationTime { get; internal set; }
        public string Dimention { get; set; }
        public bool IsQuotation { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string LastModifiedBy { get; set; }
        public DateTime DcreationTime { get; set; }
    }

}
