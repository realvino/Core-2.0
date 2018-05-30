using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewCompanyContacts.Dto
{
    public class GetNewCompany
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual string TradeLicense { get; set; }
        public virtual string TRNnumber { get; set; }
        public CreateAddressInfo[] AddressInfo { get; set; }
        public CreateContactInfo[] Contactinfo { get; set; }
        public long? AccountManagerId { get; set; }
        public virtual long ApprovedById { get; set; }
        public virtual long CreatorUserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ApprovedName { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual int? IndustryId { get; set; }
        public virtual int Discountable { get; set; } 
        public virtual int UnDiscountable { get; set; }

    }
    public class GetNewContacts
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual int? TitleId { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Country { get; set; }
        public CreateAddressInfo[] AddressInfo { get; set; }
        public CreateContactInfo[] Contactinfo { get; set; }
        public virtual int? DesignationId { get; set; }

    }
    public class GetAddressInfoDto
    {
        public virtual int Id { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual int? NewContacId { get; set; }
        public virtual int? NewInfoTypeId { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual int? CityId { get; set; }
    }
}
