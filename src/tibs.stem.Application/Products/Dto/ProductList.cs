using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Products;

namespace tibs.stem.Products.Dto
{
    [AutoMapFrom(typeof(Product))]
    public class ProductList : FullAuditedEntityDto
    {
        public new int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string SuspectCode { get; set; }
        public virtual string Gpcode { get; set; }
        public virtual string Description { get; set; }
        public virtual int? ProductSpecificationId { get; set; }
        public virtual string ProductSpecificationName { get; set; }
        public decimal? Price { get; set; }
        public string ScreationTime { get; set; }
        public bool InQuotationProduct { get; internal set; }
        public bool IsQuotation { get; set; }
        public virtual int Width { get; set; }
        public virtual int Depth { get; set; }
        public virtual int Height { get; set; }
        public virtual string Dimention { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual bool BafcoMade { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDiscountable { get; set; }
        public virtual int? ProductStateId { get; set; }
        public virtual string ProductState { get; set; }
        public virtual string ProductImage { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string LastModifiedBy { get; set; }
        public DateTime DCreationTime { get; set; }
        public int? RefId { get; set; }
    }
}
