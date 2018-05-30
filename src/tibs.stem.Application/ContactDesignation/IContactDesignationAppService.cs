using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ContactDesignation.Dto;
using tibs.stem.Dto;

namespace tibs.stem.ContactDesignation
{
    public interface IContactDesignationAppService : IApplicationService
    {
        ListResultDto<ContactDesignationInput> GetContactDesignation(GetContactDesignationInput input);
        Task<GetContactDesignation> GetContactDesignationForEdit(NullableIdDto input);
        Task ContactDesignationCreateOrUpdate(ContactDesignationInput input);
        Task GetDeleteContactDesignation(EntityDto input);
        Task<FileDto> GetContactDesignationToExcel();
    }
}
