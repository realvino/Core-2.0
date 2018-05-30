using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Ybafcoss.Dto;

namespace tibs.stem.Ybafcoss.Exporting
{
    public interface IYbafcoListExcelExporter
    {
        FileDto ExportToFile(List<YbafcoList> YbafcoListoutput);
    }
}
