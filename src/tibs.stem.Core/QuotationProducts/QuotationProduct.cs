using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Products;
using tibs.stem.Quotations;
using tibs.stem.Sections;
using tibs.stem.TemporaryProducts;

namespace tibs.stem.QuotationProducts
{
    [Table("QuotationProduct")]
    public class QuotationProduct : FullAuditedEntity
    {
        public virtual string ProductCode { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual decimal UnitOfMeasurement { get; set; }
        public virtual decimal UnitOfPrice { get; set; }
        public virtual decimal TotalAmount { get; set; }

        [ForeignKey("QuotationId")]
        public virtual Quotation quotation { get; set; }
        public virtual int QuotationId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }
        public virtual int? ProductId { get; set; }

        [ForeignKey("TemporaryProductId")]
        public virtual TemporaryProduct TemporaryProducts { get; set; }
        public virtual int? TemporaryProductId { get; set; }

        [ForeignKey("SectionId")]
        public virtual Section section { get; set; }
        public virtual int? SectionId { get; set; }
        public bool? Discountable { get; set; }
        public bool Locked { get; set; }
        public virtual decimal OverAllPrice { get; set; }
        public virtual decimal OverAllDiscount { get; set; }
        public virtual string TemporaryCode { get; set; }
        public bool Approval { get; set; }

    }
}
