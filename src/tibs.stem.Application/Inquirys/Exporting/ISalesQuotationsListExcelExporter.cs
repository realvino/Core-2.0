using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Inquirys.Exporting
{
    public interface ISalesQuotationsListExcelExporter
    {
        FileDto ExportToFile(List<QuotationListDto> salesquotationListDtos);
    }
}
