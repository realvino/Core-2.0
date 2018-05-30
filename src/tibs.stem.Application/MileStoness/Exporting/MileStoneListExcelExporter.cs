using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.MileStoness.Dto;

namespace tibs.stem.MileStoness.Exporting
{
    public class MileStoneListExcelExporter : EpPlusExcelExporterBase, IMileStoneListExcelExporter
    {
        public FileDto ExportToFile(List<MileStoneList> MileStonelistoutput)
        {
            return CreateExcelPackage(
                "MileStoneList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("MileStone"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L(" MileStoneCode"),
                        L("MileStoneName"),
                        L("RottingPeriod"));

                    AddObjects(
                        sheet, 2, MileStonelistoutput,
                        _ => _.MileStoneCode,
                        _ => _.MileStoneName,
                        _ => _.RottingPeriod);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
