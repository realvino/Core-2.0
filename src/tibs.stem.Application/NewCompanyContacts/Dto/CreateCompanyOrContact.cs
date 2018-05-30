using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.NewCustomerCompanys;

namespace tibs.stem.NewCompanyContacts.Dto
{
    public class CreateCompanyOrContact
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual int Discountable { get; set; }
        public virtual int UnDiscountable { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string LastName { get; set; }
        public virtual int? IndustryId { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual long? ApprovedById { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual string TradeLicense { get; set; }
        public virtual string TRNnumber { get; set; }
        public virtual int? DesignationId { get; set; }

    }

    [AutoMap(typeof(NewCompany))]
    public class CreateCompany
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual long? ApprovedById { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual string TradeLicense { get; set; }
        public virtual string TRNnumber { get; set; }
        public virtual int? IndustryId { get; set; }
        public virtual int Discountable { get; set; }
        public virtual int UnDiscountable { get; set; }
    }

    [AutoMapFrom(typeof(NewCompany))]
    public class NewCompanyListDto
    {
        public virtual int Id { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual string CustomerTypeName { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual string ManagedBy { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string TradeLicense { get; set; }
        public virtual string TRNnumber { get; set; }
        public virtual long? ApprovedById { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual long CreatedUserId { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        public virtual int? IndustryId { get; set; }
        public virtual string IndustryName { get; set; }
        public virtual int Discountable { get; set; } 
        public virtual int UnDiscountable { get; set; }
    }

    [AutoMap(typeof(NewContact))]
    public class CreateContact
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string LastName { get; set; }
        public virtual int? DesignationId { get; set; }

    }


    [AutoMapFrom(typeof(NewContact))]
    public class NewContactListDto
    {
        public virtual int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual string CustomerTypeName { get; set; }
        public virtual string ContactTypeName { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string Title { get; set; }
        public virtual string LastName { get; set; }
        public string IndustryName { get; set; }
        public virtual int? DesignationId { get; set; }
        public string DesignationName { get; set; }
    }

    [AutoMapTo(typeof(NewAddressInfo))]
    public class CreateAddressInfo
    {
        public virtual int Id { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual int? NewContacId { get; set; }
        public virtual int? NewInfoTypeId { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual int? CityId { get; set; }
        public virtual string CountryName { get; set; }

    }

    [AutoMapTo(typeof(NewContactInfo))]
    public class CreateContactInfo
    {
        public virtual int Id { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual int? NewContacId { get; set; }
        public virtual int? NewInfoTypeId { get; set; }
        public virtual string InfoData { get; set; }
    }

    public class GetNewContact
    {
        public CreateContact Contact { get; set; }
    }
    public class GetNewAddressInfo
    {
        public CreateAddressInfo CreateAddressInfos { get; set; }
    }
    public class GetNewContactInfo
    {
        public CreateContactInfo CreateContactInfos { get; set; }
    }
}
