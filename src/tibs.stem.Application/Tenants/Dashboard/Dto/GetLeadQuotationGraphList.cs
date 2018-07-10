using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class GetLeadQuotationGraphList
    {
        public virtual int SalesmanId { get; set; }
        public virtual string Salesman { get; set; }
        public virtual int InquiryCount { get; set; }
        public virtual int QuotationCount { get; set; }
        public virtual int WonQuotationCount { get; set; }
        public virtual int LostQuotationCount { get; set; }
        public virtual int SubmittedQuotationCount { get; set; }

    }
}
