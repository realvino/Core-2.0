using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.UserDesignationss.Dto;

namespace tibs.stem.UserDesignationss
{
    public interface IUserDesignationAppService : IApplicationService
    {
        ListResultDto<UserDesignationListDto> GetUserDesignation(GetUserDesignationInput input);
        Task<GetUserDesignation> GetUserDesignationForEdit(NullableIdDto input);
        Task CreateOrUpdateUserDesignation(UserDesignationInputDto input);
        Task DeleteUserDesignation(EntityDto input);
    }
}
