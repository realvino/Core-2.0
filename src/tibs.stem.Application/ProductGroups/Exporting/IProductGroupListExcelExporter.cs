using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups.Exporting
{
   public interface IProductGroupListExcelExporter
    {
        FileDto ExportToFile(List<ProductGroupListDto> ProductGroupListDtos);
    }
}
