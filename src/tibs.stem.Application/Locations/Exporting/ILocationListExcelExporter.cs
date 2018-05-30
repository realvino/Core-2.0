using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Locations.Dto;

namespace tibs.stem.Locations.Exporting
{
    public interface ILocationListExcelExporter
    {
        FileDto ExportToFile(List<LocationListDto> locationListDtos);
    }
}
