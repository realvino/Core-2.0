using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Cityss.Exporting
{
   public  interface ICityListExcelExporter
    {
        FileDto ExportToFile(List<CityList> cityListDtos);
    }
}
