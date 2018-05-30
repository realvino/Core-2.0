using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.LeadTypes.Dto.Exporting
{
   public class LeadTypeListExcelExporter : EpPlusExcelExporterBase, ILeadTypeListExcelExporter
    {
        public FileDto ExportToFile(List<LeadTypeList> LeadTypeListDtoss)
        {
            return CreateExcelPackage(
                "LeadType.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LeadType"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L(" LeadTypeCode"),
                        L("LeadTypeName"));

                    AddObjects(
                        sheet, 2, LeadTypeListDtoss,
                        _ => _.LeadTypeCode,
                        _ => _.LeadTypeName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
