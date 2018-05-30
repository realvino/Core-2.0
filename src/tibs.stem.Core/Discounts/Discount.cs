using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Discounts
{
    [Table("Discount")]
    public class Discount : FullAuditedEntity
    {
        public virtual decimal Discountable { get; set; }

        public virtual decimal UnDiscountable { get; set; }

        public virtual string QuotationDescription { get; set; }
        public virtual int Vat { get; set; }

    }

}
