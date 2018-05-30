using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductSpecificationss.Dto;

namespace tibs.stem.ProductSpecificationss.Exporting
{
    public interface IProductSpecificationListExcelExporter
    {
        FileDto ExportToFile(List<ProductSpecificationList> ProductSpecificationDtos);
    }
}
