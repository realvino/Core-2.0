using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Ybafcoss.Dto;


namespace tibs.stem.Ybafcoss.Exporting
{
    public class YbafcoListExcelExporter : EpPlusExcelExporterBase, IYbafcoListExcelExporter
    {
        public FileDto ExportToFile(List<YbafcoList> YbafcoListoutput)
        {
            return CreateExcelPackage(
                "YbafcoList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Ybafco"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("YbafcoCode"),
                        L("YbafcoName"));

                    AddObjects(
                        sheet, 2, YbafcoListoutput,
                        _ => _.YbafcoCode,
                        _ => _.YbafcoName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
