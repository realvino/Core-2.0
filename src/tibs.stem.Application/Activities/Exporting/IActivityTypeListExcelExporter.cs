using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Activities.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Activities.Exporting
{
   public interface IActivityTypeListExcelExporter
    {
        FileDto ExportToFile(List<ActivityListDto> activityListDtoss);
    }
}
