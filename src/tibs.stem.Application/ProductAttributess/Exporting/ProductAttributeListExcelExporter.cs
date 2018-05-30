using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductAttributess.Dto;

namespace tibs.stem.ProductAttributess.Exporting
{
   public class ProductAttributeListExcelExporter : EpPlusExcelExporterBase, IProductAttributeListExcelExporter
    {
        public FileDto ExportToFile(List<ProductAttributeListDto> ProductAttributeListDtos)
        {
            return CreateExcelPackage(
                "ProductAttribute.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductAttribute"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("AttributeCode"),
                        L("AttributeName")
                        );

                    AddObjects(
                        sheet, 2, ProductAttributeListDtos,
                        _ => _.AttributeCode,
                        _ => _.AttributeName
                        );


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
