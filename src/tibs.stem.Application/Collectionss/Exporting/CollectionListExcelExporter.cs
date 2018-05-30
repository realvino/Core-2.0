using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collectionss.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.Collectionss.Exporting
{
    public class CollectionListExcelExporter : EpPlusExcelExporterBase, ICollectionListExcelExporter
    {
        public FileDto ExportToFile(List<CollectionListDto> CollectionListDtos)
        {
            return CreateExcelPackage(
                "Collection.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Collection"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CollectionCode"),
                        L("CollectionName"));

                    AddObjects(
                        sheet, 2, CollectionListDtos,
                        _ => _.CollectionCode,
                        _ => _.CollectionName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
