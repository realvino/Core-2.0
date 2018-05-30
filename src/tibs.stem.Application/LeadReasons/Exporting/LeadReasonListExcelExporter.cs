using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.LeadReasons.Dto;

namespace tibs.stem.LeadReasons.Exporting
{
    public class LeadReasonListExcelExporter : EpPlusExcelExporterBase, ILeadReasonListExcelExporter
    {
        public FileDto ExportToFile(List<LeadReasonList> LeadReasonListDtoss)
        {
            return CreateExcelPackage(
                "LeadReason.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LeadReason"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L(" LeadReasonCode"),
                        L("LeadReasonName"));

                    AddObjects(
                        sheet, 2, LeadReasonListDtoss,
                        _ => _.LeadReasonCode,
                        _ => _.LeadReasonName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
