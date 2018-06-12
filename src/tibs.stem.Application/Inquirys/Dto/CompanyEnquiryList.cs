using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class CompanyEnquiryList
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string AccountManager { get; set; }
        public int EnquiryCount { get; set; }
        public int EnquiryWonCount { get; set; }
        public InquiryInformation[] Inquirys { get; set; }

    }

    public class InquiryInformation
    {
        public int EnquiryId { get; set; }
        public string EnquiryName { get; set; }
        public bool Won { get; set; }
    }
}
