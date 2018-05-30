using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Products.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductList>> GetProduct(GetProductInput input);
        Task<GetProduct> GetProductForEdit(NullableIdDto input);
        Task<int> CreateOrUpdateProduct(ProductInput input);
        Task CreateOrUpdateProductPriceLevel(ProductPriceLevelInput input);
        Task GetDeleteProductPriceLevel(EntityDto input);
        Array GetProductSpecificationDetails(EntityDto input);
        IEnumerable<string> SpecificationDetail(EntityDto input);
        Task<FileDto> GetProductToExcel();
        Task LinkProductToQuotation(ProductLinkInput input);
        Task<PagedResultDto<TempProductList>> GetAdvancedTempProducts(GetProductFilterInput input);
        Task<PagedResultDto<ProductList>> GetAdvancedProducts(GetProductFilterInput input);
        int CreateCustomProduct(NullableIdDto input);
    }
}
