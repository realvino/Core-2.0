using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Quotationss.Dto
{
    public class TeamReportListDto
    {
        public virtual string TeamName { get; set; }
        public virtual string AccountManager { get; set; }
        public virtual decimal TotalAEDValue { get; set; }
        public virtual decimal TotalWeightedAED { get; set; }
        public virtual decimal Total1Value { get; set; }
        public virtual decimal Total2Value { get; set; }
        public virtual decimal Total3Value { get; set; }
        public virtual decimal Total4Value { get; set; }
        public virtual decimal Total5Value { get; set; }
        public virtual decimal Total6Value { get; set; }
        public virtual decimal Total7Value { get; set; }
        public virtual decimal Total8Value { get; set; }
        public virtual decimal Total9Value { get; set; }
        public virtual decimal Total10Value { get; set; }
        public virtual decimal Total11Value { get; set; }
        public virtual decimal Total12Value { get; set; }
        public virtual int TeamId { get; set; }
        public virtual long AccountManagerId { get; set; }
        //public virtual string MonthOrder { get; set; }
        public virtual string TotalAEDValueFormat { get; set; }
        public virtual string TotalWeightedAEDFormat { get; set; }
        public virtual string Total1ValueFormat { get; set; }
        public virtual string Total2ValueFormat { get; set; }
        public virtual string Total3ValueFormat { get; set; }
        public virtual string Total4ValueFormat { get; set; }
        public virtual string Total5ValueFormat { get; set; }
        public virtual string Total6ValueFormat { get; set; }
        public virtual string Total7ValueFormat { get; set; }
        public virtual string Total8ValueFormat { get; set; }
        public virtual string Total9ValueFormat { get; set; }
        public virtual string Total10ValueFormat { get; set; }
        public virtual string Total11ValueFormat { get; set; }
        public virtual string Total12ValueFormat { get; set; }

    }
}
