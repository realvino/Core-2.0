using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Countrys.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Countrys.Exporting
{
    public interface ICountryListExcelExporter
    {
        FileDto ExportToFile(List<CountryListDto> countryListDtos);  
    }
}
