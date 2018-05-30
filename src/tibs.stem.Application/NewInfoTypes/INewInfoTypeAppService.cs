﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.NewInfoTypes.Dto;

namespace tibs.stem.NewInfoTypes
{
    interface INewInfoTypeAppService : IApplicationService
    {
        Task<PagedResultDto<NewInfoTypeListDto>> GetNewInfoType(GetNewInfoTypeInput input);
        Task<GetNewInfoType> GetNewInfoTypeForEdit(NullableIdDto input);
        Task CreateOrUpdateNewInfoType(NewInfoTypeInputDto input);
        Task GetDeleteNewInfoType(EntityDto input);
    }
}
