using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class ClosureDateInquiry
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
        public string TotalValueformat { get; set; }
        public string WeightValueformat { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public InquiryClosureDateListDto[] GetTicketReservation { get; set; }

    }
}
