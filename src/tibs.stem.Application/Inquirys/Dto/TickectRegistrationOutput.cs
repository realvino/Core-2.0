using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class TickectRegistrationOutput
    {
        public string Name { get; set; }
        public string Total { get; set; }
        public string Class { get; set; }
        public bool IsQuotation { get; set; }
        public int StatusId { get; set; }
        public InquiryListDto[] GetTicketReservation { get; set; }

    }
    public class QuotationTicketOutput
    {
        public InquiryListDto[] GetTicketReservation { get; set; }

    }
}
