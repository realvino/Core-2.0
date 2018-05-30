using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.LeadTypes.Dto.Exporting
{
   public interface ILeadTypeListExcelExporter
    {
        FileDto ExportToFile(List<LeadTypeList> LeadTypeListDtoss);
    }
}
