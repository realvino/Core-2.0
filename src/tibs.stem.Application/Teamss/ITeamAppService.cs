using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Teamss.Dto;

namespace tibs.stem.Teamss
{
    public interface ITeamAppService : IApplicationService
    {
        ListResultDto<TeamListDto> GetTeam(GetTeamInput input);
        Task<GetTeam> GetTeamForEdit(NullableIdDto input);
        Task CreateOrUpdateTeam(CreateTeamInput input);
        Task DeleteTeam(EntityDto input);
    }
}
