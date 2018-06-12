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
    public class TeamEnquiryReportExcelExporter : EpPlusExcelExporterBase, ITeamEnquiryReportExcelExporter
    {
        public FileDto ExportToFile(List<QuotationReportListDto> QuotationReportList, QuotationReportListDto TotalReport)
        {
            return CreateExcelPackage(
                QuotationReportList[0].AccountManager+".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L(QuotationReportList[0].AccountManager));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CreationDate"),
                        L("OpportunityName"),
                        L("CompanyName"), 
                        L("Status"),
                        L("AccountManager"),
                        L("New/Existing"),
                        L("Location"),
                        L("AEDValue"),
                        L("Stage"),
                        L("Percentage"),
                        L("WeightedAED"),
                        L(QuotationReportList[0].Total1ValueFormat),
                        L(QuotationReportList[0].Total2ValueFormat),
                        L(QuotationReportList[0].Total3ValueFormat),
                        L(QuotationReportList[0].Total4ValueFormat),
                        L(QuotationReportList[0].Total5ValueFormat),
                        L(QuotationReportList[0].Total6ValueFormat),
                        L(QuotationReportList[0].Total7ValueFormat),
                        L(QuotationReportList[0].Total8ValueFormat),
                        L(QuotationReportList[0].Total9ValueFormat),
                        L(QuotationReportList[0].Total10ValueFormat),
                        L(QuotationReportList[0].Total11ValueFormat),
                        L(QuotationReportList[0].Total12ValueFormat),
                        L("ActionDate"),
                        L("Notes")
                        );

                    AddObjects(
                        sheet, 2, QuotationReportList,
                        _ => _.Date,
                        _ => _.InquiryName,
                        _ => _.CompanyName,
                        _ => _.Status,
                        _ => _.AccountManager,
                        _ => _.NewOrExisting,
                        _ => _.Location,
                        _ => _.AEDValue,
                        _ => _.Stage,
                        _ => _.Percentage,
                        _ => _.WeightedAED,
                        _ => _.Total1Value,
                        _ => _.Total2Value,
                        _ => _.Total3Value,
                        _ => _.Total4Value,
                        _ => _.Total5Value,
                        _ => _.Total6Value,
                        _ => _.Total7Value,
                        _ => _.Total8Value,
                        _ => _.Total9Value,
                        _ => _.Total10Value,
                        _ => _.Total11Value,
                        _ => _.Total12Value,
                        _ => _.ActionDate,
                        _ => _.Notes
                        );

                    for (var i = 1; i <= 25; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }

                    sheet.InsertRow(QuotationReportList.Count + 1, 2);
                    sheet.Cells[QuotationReportList.Count + 3, 1].Value = TotalReport.Date;
                    sheet.Cells[QuotationReportList.Count + 3, 8].Value = TotalReport.AEDValue;
                    sheet.Cells[QuotationReportList.Count + 3, 11].Value = TotalReport.WeightedAED;
                    sheet.Cells[QuotationReportList.Count + 3, 12].Value = TotalReport.Total1Value;
                    sheet.Cells[QuotationReportList.Count + 3, 13].Value = TotalReport.Total2Value;
                    sheet.Cells[QuotationReportList.Count + 3, 14].Value = TotalReport.Total3Value;
                    sheet.Cells[QuotationReportList.Count + 3, 15].Value = TotalReport.Total4Value;
                    sheet.Cells[QuotationReportList.Count + 3, 16].Value = TotalReport.Total5Value;
                    sheet.Cells[QuotationReportList.Count + 3, 17].Value = TotalReport.Total6Value;
                    sheet.Cells[QuotationReportList.Count + 3, 18].Value = TotalReport.Total7Value;
                    sheet.Cells[QuotationReportList.Count + 3, 19].Value = TotalReport.Total8Value;
                    sheet.Cells[QuotationReportList.Count + 3, 20].Value = TotalReport.Total9Value;
                    sheet.Cells[QuotationReportList.Count + 3, 21].Value = TotalReport.Total10Value;
                    sheet.Cells[QuotationReportList.Count + 3, 22].Value = TotalReport.Total11Value;
                    sheet.Cells[QuotationReportList.Count + 3, 23].Value = TotalReport.Total12Value;

                    int totalCol = 23;
                    for (var i = 12; i <= totalCol; i++)
                    {
                        decimal test = (decimal)sheet.Cells[QuotationReportList.Count + 3, i].Value;

                        if (test <= 0.00M)
                        {
                            sheet.DeleteColumn(i);
                            totalCol = totalCol - 1;
                        }

                    }
                });
        }
    }
}
