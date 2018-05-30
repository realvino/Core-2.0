using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss.Exporting
{
    public class QuotationListExcelExporter : EpPlusExcelExporterBase, IQuotationListExcelExporter
    {
        public FileDto ExportToFile(List<QuotationListDto> QuotationListDtos)
        {
            return CreateExcelPackage(
                "Quotation.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Quotation"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("RefNo"),
                        L("InquiryName"),
                        L("CompanyName"),
                        L("AttentionName"),
                        L("Salesperson"),
                        L("QuotationStatus"),
                        L("Total"),
                        L("CreatedBy"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, QuotationListDtos,
                        _ => _.RefNo,
                        _ => _.InquiryName,
                        _ => _.CompanyName,
                        _ => _.AttentionName,
                        _ => _.SalesPersonName,
                        _ => _.StatusName,
                        _ => _.Total,
                         _ => _.CreatedBy,
                        _ => _.SCreationTime);

                   

                    for (var i = 1; i <= 9; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
