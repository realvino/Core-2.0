using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss.Exporting
{
    public interface IQuotationListExcelExporter
    {
        FileDto ExportToFile(List<QuotationListDto> QuotationListDtos);
    }
}
