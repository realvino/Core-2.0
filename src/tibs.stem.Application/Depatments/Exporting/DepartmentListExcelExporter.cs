using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Depatments.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Depatments.Exporting
{
  public class DepartmentListExcelExporter : EpPlusExcelExporterBase, IDepartmentListExcelExporter
    {
        public FileDto ExportToFile(List<DepartmentListDto> list)
        {
            return CreateExcelPackage(
                "Department.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Department"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DepartmentCode"),
                        L("DepatmentName"));

                    AddObjects(
                        sheet, 2, list,
                        _ => _.DepartmentCode,
                        _ => _.DepatmentName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
