using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collectionss.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Collectionss.Exporting
{
   public interface ICollectionListExcelExporter
    {
        FileDto ExportToFile(List<CollectionListDto> CollectionListDtos);
    }
}
