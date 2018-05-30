using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductAttributeGroupss.Dto;

namespace tibs.stem.ProductAttributeGroupss.Exporting
{
    public interface IAttributeGroupListExcelExporter
    {
        FileDto ExportToFile(List<AttributeGroupListDto> AttributeGroupListDtos);
    }
}
