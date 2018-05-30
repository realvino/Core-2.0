using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Sources.Dto;

namespace tibs.stem.Sources.Exporting
{
   public class SourceListExcelExporter : EpPlusExcelExporterBase, ISourceListExcelExporter
    {
        public FileDto ExportToFile(List<SourceListDto> SourceListDtos)
        {
            return CreateExcelPackage(
                "SourceList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Source"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("SourceCode"),
                        L("SourceName"),
                        L("ColorCode"),
                        L("SourceTypeName"));

                    AddObjects(
                        sheet, 2, SourceListDtos,
                        _ => _.SourceCode,
                        _ => _.SourceName,
                        _ =>_.ColorCode,
                        _ =>_.TypeName);


                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}

