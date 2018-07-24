using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class GetLeadQuotationGraphList
    {
        public virtual int InquiryCount { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string STotal { get; set; }
    }
    public class GetLeadQuotationGraphListdto
    {
        public virtual int Id { get; set; }
        public virtual int StatusId { get; set; }
        public virtual decimal? Total { get; set; }
        public virtual bool? Submitted { get; set; }
        public virtual DateTime? SubmittedDate { get; set; }
        public virtual bool? Won { get; set; }
        public virtual bool? Lost { get; set; }
        public virtual string QuotationRefno { get; set; }
        public virtual bool? Stared { get; set; }
        public virtual bool? TenderProject { get; set; }
        public virtual decimal? Weighted { get; set; }

    }

    public class GetRaindto
    {
        public virtual int TotalCount { get; set; }
        public virtual int TenderCount { get; set; }
        public virtual int QTotalCount { get; set; }
        public virtual int QTenderCount { get; set; }
        public virtual int WTotalCount { get; set; }
        public virtual int WTenderCount { get; set; }
        public virtual int LTotalCount { get; set; }
        public virtual int LTenderCount { get; set; }
        public virtual int StrTotalCount { get; set; }
        public virtual int StrTenderCount { get; set; }
        public virtual int LiveTotalCount { get; set; }
        public virtual int LiveTenderCount { get; set; }

        public virtual decimal? Totalvalue { get; set; }
        public virtual decimal? Tendervalue { get; set; }
        public virtual decimal? QTotalvalue { get; set; }
        public virtual decimal? QTendervalue { get; set; }
        public virtual decimal? WTotalvalue { get; set; }
        public virtual decimal? WTendervalue { get; set; }
        public virtual decimal? LTotalvalue { get; set; }
        public virtual decimal? LTendervalue { get; set; }
        public virtual decimal? StrTotalvalue { get; set; }
        public virtual decimal? StrTendervalue { get; set; }
        public virtual decimal? LiveTotalvalue { get; set; }
        public virtual decimal? LiveTendervalue { get; set; }
        public virtual decimal ConversionRate { get; set; }

        //public virtual bool Submitted { get; set; }
        //public virtual DateTime? SubmittedDate { get; set; }
        //public virtual bool Won { get; set; }
        //public virtual bool Lost { get; set; }
        //public virtual string QuotationRefno { get; set; }
        //public virtual bool Stared { get; set; }
        //public virtual bool TenderProject { get; set; }

    }
}
