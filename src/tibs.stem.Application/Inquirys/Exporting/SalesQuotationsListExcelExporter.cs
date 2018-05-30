using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Inquirys.Exporting
{
    public class SalesQuotationsListExcelExporter : EpPlusExcelExporterBase, ISalesQuotationsListExcelExporter
    {
        public FileDto ExportToFile(List<QuotationListDto> salesquotationListDtos)
        {
            return CreateExcelPackage(
                "SalesQuotationsList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Inquiry"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("RefNo"),
                        L("TitleOfEnquiry"),
                        L("CompanyName"),
                        L("AttentionName"),
                        L("Salesperson"),
                        L("QuotationStatus"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, salesquotationListDtos,
                       _ => _.RefNo,
                        _ => _.InquiryName,
                        _ => _.CompanyName,
                        _ => _.AttentionName,
                        _ => _.SalesPersonName,
                         _ => _.StatusName,
                         _ => _.SCreationTime

                        );


                    for (var i = 1; i <= 7; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
