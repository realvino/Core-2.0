using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Discounts;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    [AutoMap(typeof(Discount))]
    public class CreateDiscountInput
    {
        public int Id { get; set; }
        public virtual decimal Discountable { get; set; }
        public virtual decimal UnDiscountable { get; set; }
        public virtual string QuotationDescription { get; set; }
        public virtual int Vat { get; set; }
    }
}
