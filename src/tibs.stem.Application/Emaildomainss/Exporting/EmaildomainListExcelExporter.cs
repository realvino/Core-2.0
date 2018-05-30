using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.DataExporting.Excel.EpPlus;
using tibs.stem.Dto;
using tibs.stem.Emaildomainss.Dto;

namespace tibs.stem.Emaildomainss.Exporting
{
     
    public class EmaildomainListExcelExporter : EpPlusExcelExporterBase, IEmaildomainListExcelExporter
    {
        public FileDto ExportToFile(List<EmaildomainList> EmaildomainListoutput)
        {
            return CreateExcelPackage(
                "EmaildomainList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Emaildomain"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,                        
                        L("EmaildomainName"));

                    AddObjects(
                        sheet, 1, EmaildomainListoutput,
                        _ => _.EmaildomainName);


                    for (var i = 1; i <= 1; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
