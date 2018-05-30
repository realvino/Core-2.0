using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups.Exporting
{
    public class ProductGroupListExcelExporter : EpPlusExcelExporterBase, IProductGroupListExcelExporter
    {
        public FileDto ExportToFile(List<ProductGroupListDto> ProductGroupListDtos)
        {
            return CreateExcelPackage(
                "ProductGroup.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductGroup"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductGroupName")
                        );

                    AddObjects(
                        sheet, 2, ProductGroupListDtos,
                         _ => _.ProductGroupName
                          );


                    for (var i = 1; i <= 1; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
