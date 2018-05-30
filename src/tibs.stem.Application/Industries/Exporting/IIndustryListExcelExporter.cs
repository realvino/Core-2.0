using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Industries.Dto;

namespace tibs.stem.Industries.Exporting
{
    public interface IIndustryListExcelExporter
    {
        FileDto ExportToFile(List<IndustryListDto> IndustryListDtos);   
    }
}
