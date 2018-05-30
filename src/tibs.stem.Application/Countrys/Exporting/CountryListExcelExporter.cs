using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Countrys.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.Countrys.Exporting
{
    public class CountryListExcelExporter : EpPlusExcelExporterBase, ICountryListExcelExporter
    {
        
        public FileDto ExportToFile(List<CountryListDto> countryListDtos)
        {
            return CreateExcelPackage(
                "CountryList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Country"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CountryCode"),
                        L("CountryName"),
                        L("ISDCode")
                        );

                    AddObjects(
                        sheet, 2, countryListDtos,
                        _ => _.CountryCode,
                        _ => _.CountryName,
                        _ => _.ISDCode
                        );
                    

                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }
}

