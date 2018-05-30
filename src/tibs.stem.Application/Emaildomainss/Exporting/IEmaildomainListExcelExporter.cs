using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Emaildomainss.Dto;

namespace tibs.stem.Emaildomainss.Exporting
{
     
    public interface IEmaildomainListExcelExporter
    {
        FileDto ExportToFile(List<EmaildomainList> EmaildomainListoutput);
    }
}
