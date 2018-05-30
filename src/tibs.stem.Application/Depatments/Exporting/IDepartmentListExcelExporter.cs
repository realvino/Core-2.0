using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Depatments.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Depatments.Exporting
{
    public interface IDepartmentListExcelExporter
    {
        FileDto ExportToFile(List<DepartmentListDto> list);
    }
}
