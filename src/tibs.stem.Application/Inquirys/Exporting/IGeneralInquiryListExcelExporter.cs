using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Inquirys.Dto;

namespace tibs.stem.Inquirys.Exporting
{
   public interface IGeneralInquiryListExcelExporter
    {
        FileDto ExportToFile(List<InquiryListDto> inquiryListDtos);
    }
}
