using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Inquirys.Dto;

namespace tibs.stem.Inquirys.Exporting
{
    public class LeadInquiryListExcelExporter : EpPlusExcelExporterBase , ILeadInquiryListExcelExporter
    {
        public FileDto ExportToFile(List<InquiryListDto> leadInquiryListDtos)
        {
            return CreateExcelPackage(
                "LeadInquiryList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Inquiry"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        //L("Address"),
                        //L("WebSite"),
                        //L("MobileNo"),
                        //L("LandlineNumber"),
                        //L("Browcerinfo"),
                        L("CompanyName"),
                        L("Milestone"),
                        L("DepartmentName"),
                        L("TeamName"),
                        L("CreatedBy"),
                        L("CreationTime")
                        );

                    AddObjects(
                        sheet, 2, leadInquiryListDtos,
                        _ => _.Name,
                        //_ => _.Address,
                        //_ => _.WebSite,
                        //_ => _.MbNo,
                        //_ => _.LandlineNumber,
                        //_ => _.Browcerinfo,
                         _ => _.CompanyName,
                        _ => _.MileStoneName,
                        _ => _.DepartmentName,
                         _ => _.TeamName,
                        _ => _.CreatedBy,
                         _ => _.CreationTime

                        );


                    for (var i = 1; i <= 7; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
