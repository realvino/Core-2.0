using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Industries.Dto;

namespace tibs.stem.Industries.Exporting
{
    public class IndustryListExcelExporter : EpPlusExcelExporterBase, IIndustryListExcelExporter
    {
        public FileDto ExportToFile(List<IndustryListDto> IndustryListDtos)
        {
            return CreateExcelPackage(
                "IndustryList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Industry"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("IndustryCode"),
                        L("IndustryName"));

                    AddObjects(
                        sheet, 2, IndustryListDtos,
                        _ => _.IndustryCode,
                        _ => _.IndustryName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }

}
