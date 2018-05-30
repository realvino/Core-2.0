using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.LeadReasons.Dto;

namespace tibs.stem.LeadReasons.Exporting
{
   public interface ILeadReasonListExcelExporter
    {
        FileDto ExportToFile(List<LeadReasonList> LeadReasonListDtoss);
    }
}
