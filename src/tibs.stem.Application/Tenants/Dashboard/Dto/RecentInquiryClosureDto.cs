using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class RecentInquiryInput
    {
        public virtual string TeamId { get; set; }
        public virtual string SalesId { get; set; }
    }
    public class RecentInquiryClosureList
    {
        public RecentInquiryClosureDto[] ThisMonthClosureInquiry { get; set; }
        public RecentInquiryClosureDto[] ThisWeekClosureInquiry { get; set; }
        public RecentInquiryClosureDto[] NextWeekClosureInquiry { get; set; }
        public string MonthValue { get; set; }
        public string ThisweekValue { get; set; }
        public string NextWeekValue { get; set; }
        public RecentInquiryClosureDto[] OverDueClosureInquiry { get; set; }
        public string OverDueValue { get; set; }

    }
    public class RecentInquiryActivityList
    {
        public RecentInquiryClosureDto[] ThisWeekActivityInquiry { get; set; }
        public RecentInquiryClosureDto[] NextWeekActivityInquiry { get; set; }
        public RecentInquiryClosureDto[] OverDueActivityInquiry { get; set; }
        public string OverDueValue { get; set; }
        public string ThisweekValue { get; set; }
        public string NextWeekValue { get; set; }
    }
    public class RecentInquiryClosureDto
    { 
        public virtual string Week { get; set; }
        public virtual string Month { get; set; }
        public virtual string ClosureDate { get; set; }
        public virtual string SubMmissionId { get; set; }
        public virtual string InquiryName { get; set; }
        public virtual int InquiryId { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string CreatorImage { get; set; }
        public virtual string SalesManImage { get; set; }
        public virtual string CoordinatorImage { get; set; }
        public virtual string DesignerImage { get; set; }
        public virtual DateTime LastActivity { get; set; }
        public virtual string Company { get; set; }
        public virtual string StageName { get; set; }
        public virtual string MileStone { get; set; }
        public virtual string Total { get; set; }
        public virtual string Stage { get; set; }
        public decimal Value { get; set; }
        public virtual bool Stared { get; set; }

    }

}
