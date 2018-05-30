using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.QuotationProductss.Dto
{
    public class QuotationProductOutput
    {
        public string name { get; set; }
        public decimal subtotal { get; set; }
        public string subtotalFormat { get; set; }
        public QuotationProductListDto[] GetQuotationProduct { get; set; }

    }
}
