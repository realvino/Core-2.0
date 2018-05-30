using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.ProductFamilyss.Exporting
{
   public interface IProductFamilyListExcelExporter
    {
        FileDto ExportToFile(List<ProductFamilyListDto> ProductFamilyListDtos);
    }
}
