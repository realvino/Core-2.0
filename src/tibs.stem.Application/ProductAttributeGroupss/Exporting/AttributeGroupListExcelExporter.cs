using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.ProductAttributeGroupss.Dto;

namespace tibs.stem.ProductAttributeGroupss.Exporting
{
    public class AttributeGroupListExcelExporter : EpPlusExcelExporterBase, IAttributeGroupListExcelExporter
    {
        public FileDto ExportToFile(List<AttributeGroupListDto> AttributeGroupListDtos)
        {
            return CreateExcelPackage(
                "AttributeGroup.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AttributeGroup"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("AttributeCode"),
                        L("AttributeName"));

                    AddObjects(
                        sheet, 2, AttributeGroupListDtos,
                        _ => _.AttributeGroupCode,
                        _ => _.AttributeGroupName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
