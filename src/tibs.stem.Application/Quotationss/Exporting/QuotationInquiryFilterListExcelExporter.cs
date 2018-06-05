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
    public class QuotationInquiryFilterListExcelExporter : EpPlusExcelExporterBase, IQuotationInquiryFilterListExcelExporter
    {
        public FileDto ExportToFile(List<QuotationInquiryFilter> QuotationInquiryFilterList, string ViewName, List<string> RemovedColumns)
        {
            return CreateExcelPackage(
                ViewName+".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L(ViewName));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Probability%"),
                        L("QuotationRefNo"),
                        L("OpportunityName"),
                        L("OpportunityRefNo"),
                        L("CompanyName"),
                        L("Designation"),
                        L("QuotationStatus"),
                        L("Email"),
                        L("MobileNumber"),
                        L("MileStone"),
                        L("Stages"), 
                        L("Team"),
                        L("Department"),
                        L("Salesperson"),
                        L("Designer"), 
                        L("Co-Ordinator"),
                        L("Categories"),
                        L("EstimationValue"),
                        L("QuotationValue"),
                        L("QuotationCreator"),
                        L("ClosureDate"),
                        L("NextActivityDate"),
                        L("CreationDate"),
                        L("Emirates"),
                        L("AreaName"),
                        L("BuildingName"),
                        L("WhyBafco")
                        );

                    AddObjects(
                        sheet, 2, QuotationInquiryFilterList,
                        _ => _.Probability,
                        _ => _.QuotationRefNo,
                        _ => _.TitleOfInquiry,
                        _ => _.InquiryRefNo,
                        _ => _.PotentialCustomer,
                        _ => _.DesignationName,
                        _ => _.QuotationStatus,
                        _ => _.Email,
                        _ => _.MobileNumber,
                        _ => _.MileStoneName,
                        _ => _.EnquiryStatus,
                        _ => _.TeamName,
                        _ => _.DepatmentName,
                        _ => _.Salesman,
                        _ => _.Designer,
                        _ => _.Coordinator,
                        _ => _.Categories,
                        _ => _.Total,
                        _ => _.QuotationValue,
                        _ => _.QuotationCreateBy,
                        _ => _.ClosureDate,
                        _ => _.LastActivity,
                        _ => _.QuotationCreation,
                        _ => _.Emirates,
                        _ => _.AreaName,
                        _ => _.BuildingName,
                        _ => _.WhyBafco);

                    for (var i = 1; i <= 27; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }

                    int totalCol = 27;
                    foreach (string hideCol in RemovedColumns)
                    {
                        for (var i = 1; i <= totalCol; i++)
                        {
                            string test = sheet.Cells[1, i].Value.ToString();

                            if (test == hideCol)
                            {
                                sheet.DeleteColumn(i);
                                totalCol = totalCol - 1;
                            }

                        }

                    }
                    
                });
        }

    }
}
