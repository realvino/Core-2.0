using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Inquirys.Dto;

namespace tibs.stem.Inquirys.Exporting
{
    public class SalesInquiryListExcelExporter : EpPlusExcelExporterBase  , ISalesInquiryListExcelExporter
    {
        public FileDto ExportToFile(List<InquiryListDto> salesInquiryListDtos)
        {
            return CreateExcelPackage(
                "SalesInquiryList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Inquiry"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("RefNo"),
                        L("TitleOfEnquiry"),
                        L("CompanyName"),
                        L("Milestone"),
                        L("DepartmentName"),
                        L("TeamName"),
                        L("AssignTo"),
                        L("ClosureDate"),
                        L("NextActivity"),
                        L("CreatedBy"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, salesInquiryListDtos,
                       _ => _.SubMmissionId,
                        _ => _.Name,
                        _ => _.CompanyName,
                        _ => _.MileStoneName,
                        _ => _.DepartmentName,
                        _ => _.TeamName,
                        _ => _.SalesMan,
                        _ => _.SclosureDate,
                        _ => _.SlastActivity,
                        _ => _.CreatedBy,
                        _ => _.SCreationTime

                        );


                    for (var i = 1; i <= 11; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
