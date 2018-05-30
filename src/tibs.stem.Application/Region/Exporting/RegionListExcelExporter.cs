using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Locations.Dto;
using tibs.stem.Region.Dto;

namespace tibs.stem.Region.Exporting
{
    public class RegionListExcelExporter : EpPlusExcelExporterBase, IRegionListExcelExporter
    {
        public FileDto ExportToFile(List<RegionList> regionListDtos)
        {
            return CreateExcelPackage(
                "RegionList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Region"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("RegionCode"),
                        L("RegionName"));
                    //L("CityName"));

                    AddObjects(
                        sheet, 2, regionListDtos,
                        _ => _.RegionCode,
                        _ => _.RegionName);
                       // _ => _.CityName);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
