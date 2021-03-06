﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationProducts;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMap(typeof(QuotationProduct))]
    public class QuotationProductInput
    {
        public int Id { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual decimal UnitOfMeasurement { get; set; }
        public virtual decimal UnitOfPrice { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual int? QuotationId { get; set; }
        public virtual int? ProductId { get; set; }
        public bool Approval { get; set; }
        public virtual int? SectionId { get; set; }
        public bool? Discountable { get; set; }
        public bool Locked { get; set; }
        public virtual decimal OverAllPrice { get; set; }
        public virtual decimal OverAllDiscount { get; set; }
        public virtual string TemporaryCode { get; set; }
        public virtual int? TemporaryProductId { get; set; }

    }
}
