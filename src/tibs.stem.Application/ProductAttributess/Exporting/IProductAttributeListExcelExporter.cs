using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductAttributess.Dto;

namespace tibs.stem.ProductAttributess.Exporting
{
    public interface IProductAttributeListExcelExporter
    {
        FileDto ExportToFile(List<ProductAttributeListDto> ProductAttributeListDtos);
    }
}
