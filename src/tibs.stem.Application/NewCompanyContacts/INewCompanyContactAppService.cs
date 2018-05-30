using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.NewCompanyContacts.Dto;

namespace tibs.stem.NewCompanyContacts
{
    public interface INewCompanyContactAppService : IApplicationService
    {
        Task<int> CreateOrUpdateCompanyOrContact(CreateCompanyOrContact input);
        Task<Array> GetNewCompanyForEdit(NullableIdDto input);
        Task<Array> GetNewContactForEdit(NullableIdDto input);
        Task CreateOrUpdateAddressInfo(CreateAddressInfo input);
        Task CreateOrUpdateContactInfo(CreateContactInfo input);
        Task<GetNewAddressInfo> GetNewAddressInfoForEdit(NullableIdDto input);
        Task<GetNewContactInfo> GetNewContactInfoForEdit(NullableIdDto input);
        Task GetDeleteAddressInfo(EntityDto input);
        Task GetDeleteContactInfo(EntityDto input);
        Task GetDeleteCompany(EntityDto input);
        Task GetDeleteContact(EntityDto input);
        Task<PagedResultDto<NewCompanyListDto>> GetCompanys(GetCompanyInput input); 
        Task<PagedResultDto<NewContactListDto>> GetContacts(GetContactInput input);
        ListResultDto<NewContactListDto> GetCompanyContacts(NullableIdDto input);
        Task<int> ContactUpdate(CreateCompanyOrContact input);
        Task GetDeleteEnquiryContact(EntityDto input);
        bool CheckDuplicateContact(ContactInputDto input);
        bool CheckDuplicateCompany(CompanyInputDto input);
        Task ApprovedCompany(EntityDto input);
        Task<GetNewContacts> SearchContactInfo(EnquiryContactInput input);
        Task<int> SearchContactInfoId(EnquiryContactInput input);
    }
}
