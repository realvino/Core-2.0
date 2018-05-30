using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class GetInquirys
    {
        public InquiryListDto Inquirys { get; set; }
        public LeadDetailListDto InquiryDetails { get; set; }
        public Sourcelist[] Sourcelists { get; set; }
        public Sourcelist[] SelectedSource { get; set; }
        public Array ContactEdit { get; set; }
        public InquiryLockedQuotation InquiryLock { get; set; }
    }
    public class Sourcelist
    {
        public bool IsAssigned { get; internal set; }
        public virtual int? SourceId { get; set; }
        public virtual string SourceName { get; set; }
    }
    public class ContactInput
    {
        public virtual int? Id { get; set; }
    }
    public class InquiryLockedQuotation
    {
        public virtual string QuotationRefno { get; set; }
        public virtual decimal QuotationTotal { get; set; }
    }
}
