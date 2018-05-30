using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Products.Dto;

namespace tibs.stem.Products.Exporting
{
    public interface IProductListExcelExporter
    {
        FileDto ExportToFile(List<ProductList> ProductListDtos);
    }
}
