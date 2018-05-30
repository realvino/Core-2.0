using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Quotationss.Dto
{
    public class UpdateQuotationInput
    {
        public int Id { get; set; }
        public virtual int? MileStoneId { get; set; }
        public int? StageId { get; set; }
        public virtual string PONumber { get; set; }
        public virtual string ReasonRemark { get; set; }
        public virtual int? CompatitorId { get; set; }
        public int? ReasonId { get; set; }
        public virtual bool Won { get; set; }
        public virtual bool Lost { get; set; }
        public virtual DateTime? PaymentDate { get; set; }

    }
}
