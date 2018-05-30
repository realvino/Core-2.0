using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.Cityss.Exporting
{
    public class CityListExcelExporter : EpPlusExcelExporterBase, ICityListExcelExporter
    {
       
        public FileDto ExportToFile(List<CityList> cityListDtos)
        {
            return CreateExcelPackage(
                "CityList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("City"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CityCode"),
                        L("CityName"),
                        L("CountryName") );

                    AddObjects(
                        sheet, 2, cityListDtos,
                        _ => _.CityCode,
                        _ => _.CityName,
                        _ => _.CountryName );

                   
                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }
}
