using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.ProductFamilyss.Exporting
{
   public class ProductFamilyListExcelExporter : EpPlusExcelExporterBase, IProductFamilyListExcelExporter
    {
        public FileDto ExportToFile(List<ProductFamilyListDto> ProductFamilyListDtos)
        {
            return CreateExcelPackage(
                "ProductFamily.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductFamily"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ProductFamilyCode"),
                        L("ProductFamilyName"),
                        L("CollectionName"));

                    AddObjects(
                        sheet, 2, ProductFamilyListDtos,
                        _ => _.ProductFamilyCode,
                        _ => _.ProductFamilyName,
                        _ => _.CollectionName);


                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
