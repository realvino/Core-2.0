using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Quotationss.Dto
{
    public class GetImportHistoryInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int QuotationId { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "FileName";
            }
        }
    }
}
