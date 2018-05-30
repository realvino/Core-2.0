using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Locations.Dto;
using tibs.stem.Region.Dto;

namespace tibs.stem.Region.Exporting
{
  public  interface IRegionListExcelExporter
    {
        FileDto ExportToFile(List<RegionList> regionListDtos);
    }

}
