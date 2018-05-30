using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Locations.Dto;

namespace tibs.stem.Locations.Exporting
{
    public class LocationListExcelExporter : EpPlusExcelExporterBase, ILocationListExcelExporter
    {
        public FileDto ExportToFile(List<LocationListDto> locationListDtos)
        {
            return CreateExcelPackage(
                "LocationList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Location"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("LocationCode"),
                        L("LocationName"),
                        L("CityName"));

                    AddObjects(
                        sheet, 2, locationListDtos,
                        _ => _.LocationCode,
                        _ => _.LocationName,
                        _ => _.CityName);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

    }
}

