using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Activities.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.Activities.Exporting
{
   public class ActivityTypeListExcelExporter : EpPlusExcelExporterBase, IActivityTypeListExcelExporter
    {
        public FileDto ExportToFile(List<ActivityListDto> activityListDtoss)
        {
            return CreateExcelPackage(
                "ActivityType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ActivityType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L(" ActivityCode"),
                        L("ActivityName"),
                        L("ColorCode"));

                    AddObjects(
                        sheet, 2, activityListDtoss,
                        _ => _.ActivityCode,
                        _ => _.ActivityName,
                        _ => _.ColorCode);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
