using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductSpecificationss.Dto;

namespace tibs.stem.ProductSpecificationss.Exporting
{
   public class ProductSpecificationListExcelExporter : EpPlusExcelExporterBase, IProductSpecificationListExcelExporter
    {
        public FileDto ExportToFile(List<ProductSpecificationList> ProductSpecificationDtos)
        {
            return CreateExcelPackage(
                "ProductSpecification.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductSpecification"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name")
                        );

                    AddObjects(
                        sheet, 2, ProductSpecificationDtos,
                         _ => _.Name);


                    for (var i = 1; i <= 1; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
