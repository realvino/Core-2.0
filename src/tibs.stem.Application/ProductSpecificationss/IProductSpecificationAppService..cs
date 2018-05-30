using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Products.Dto;
using tibs.stem.ProductSpecificationss.Dto;

namespace tibs.stem.ProductSpecificationss
{
   public interface IProductSpecificationAppService : IApplicationService
    {
        ListResultDto<ProductSpecificationList> GetProductSpecification(GetProductSpecificationInput input);
        Task<GetProductSpecification> GetProductSpecificationForEdit(EntityDto input);
        Task CreateOrUpdateProductSpecification(CreateProductSpecification input);
        Task GetDeleteProductSpecification(EntityDto input);
        Task GetDeleteProductSpecificationDetail(EntityDto input);
        Task CreateProductSpecificationDetail(CreateProductSpecificationInput input);
        Array GetProductGroupDetails(EntityDto input);
        Task<FileDto> GetProductSpecificationToExcel();
        Task<PagedResultDto<ProductList>> GetProducts(GetProductSpecInput input);
        Task CreateOrDeleteProductGroupDetails(ProductSpecArray input);
        Task GenerateProduct(EntityDto input);
        Task ConfirmDeleteProductSpecification(EntityDto input);
    }
}
