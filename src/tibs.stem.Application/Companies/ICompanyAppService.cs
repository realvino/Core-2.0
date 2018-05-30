using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Companies.Dto;

namespace tibs.stem.Companies
{
    public interface ICompanyAppService : IApplicationService
    {
        Task<PagedResultDto<CompanyViewDt>> GetCompanies(GetCompanyInput input);
        Task<GetCompany> GetCompanyForEdit(NullableIdDto input);
        Task CreateOrUpdateCompany(CreateCompanyInput input);
        Task DeleteCompany(EntityDto input);
        ListResultDto<ContactViewDto> GetContacts(NullableIdDto input);
        Task<GetCompanyContact> GetContactForInput(NullableIdDto input);
        Task CreateOrUpdateContact(CreateContactInput input);
        Task DeleteContact(EntityDto input);
        Task<PagedResultDto<ContactViewDto>> GetAllContacts(GetContactInput input);
        int CompanyCreate(CompanyCreateInput input);
        int NewCompanyCreate(CompanyCreateInput input);
    }
}
