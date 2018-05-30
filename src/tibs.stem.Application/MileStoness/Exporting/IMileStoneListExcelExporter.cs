using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.MileStoness.Dto;

namespace tibs.stem.MileStoness.Exporting
{
   public interface IMileStoneListExcelExporter
    {
        FileDto ExportToFile(List<MileStoneList> MileStonelistoutput);
    }
}
