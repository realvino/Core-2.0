using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.EnquiryStatusss.Dto;

namespace tibs.stem.EnquiryStatusss.Exporting
{
    public class EnquiryStatusListExcelExporter : EpPlusExcelExporterBase, IEnquiryStatusListExcelExporter
    {
        public FileDto ExportToFile(List<EnquiryStatusListDto> enquiryStatusListDtos)
        {
            return CreateExcelPackage(
                "EnquiryStatusList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("EnqStatus"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("EnqStatusCode"),
                        L("EnqStatusName"),
                        L("EnqStatusColor"));

                    AddObjects(
                        sheet, 2, enquiryStatusListDtos,
                        _ => _.EnqStatusCode,
                        _ => _.EnqStatusName,
                        _ => _.EnqStatusColor);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
