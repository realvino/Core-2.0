using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Quotationss.Dto
{
   public class ImportQuotationInput
    {
        public int QuotationId { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
    }
}
