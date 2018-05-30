using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collectionss.Dto;

namespace tibs.stem.Collectionss
{
   public interface ICollectionAppService : IApplicationService
    {
        ListResultDto<CollectionListDto> GetCollection(GetCollectionInput input);
        Task<GetCollection> GetCollectionForEdit(NullableIdDto input);
        Task CreateOrUpdateCollection(CreateCollectionInput input);
        Task GetDeleteCollection(EntityDto input);
    }
    
}
