using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Sources.Dto;

namespace tibs.stem.Sources.Exporting
{
   public interface ISourceListExcelExporter
    {
        FileDto ExportToFile(List<SourceListDto> SourceListDtos);

    }
}
