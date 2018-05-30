using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ContactDesignation.Dto;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;

namespace tibs.stem.ContactDesignation.Exporting
{
    public class ContactDesignationExcelExporter : EpPlusExcelExporterBase, IContactDesignationExcelExporter
    {
        public FileDto ExportToFile(List<ContactDesignationInput> ContactDesignationList)
        {
            return CreateExcelPackage(
                "ContactDesignation.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ContactDesignation"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DesignationCode"),
                        L("DesiginationName"));

                    AddObjects(
                        sheet, 2, ContactDesignationList,
                        _ => _.DesignationCode,
                        _ => _.DesiginationName);


                    for (var i = 1; i <= 2; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
