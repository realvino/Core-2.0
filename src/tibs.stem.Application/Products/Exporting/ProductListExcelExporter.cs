using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Products.Dto;

namespace tibs.stem.Products.Exporting
{
   public class ProductListExcelExporter : EpPlusExcelExporterBase, IProductListExcelExporter
    {
        public FileDto ExportToFile(List<ProductList> ProductListDtos)
        {
            return CreateExcelPackage(
                "Product.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Product"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductCode"),
                        L("ProductSpecification"),
                        L("SuspectCode"),
                        L("Gpcode"), 
                        L("Price"),
                        L("Created By"),
                        L("Modified By"),
                        L("CreateTime")
                       );

                    AddObjects(
                        sheet, 2, ProductListDtos,
                        _ => _.ProductCode,
                        _ => _.ProductSpecificationName,
                        _ => _.SuspectCode,
                        _ => _.Gpcode,
                        _ => _.Price,
                        _ => _.CreatedBy,
                        _ => _.LastModifiedBy,
                        _ => _.DCreationTime
                        );

                    var timeColumn = sheet.Column(8);
                    timeColumn.Style.Numberformat.Format = "MM/dd/yyyy hh:mm:ss tt";

                    for (var i = 1; i <= 6; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
