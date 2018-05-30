using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Companies.Dto
{
    public class GetCompany
    {
        public CreateCompanyInput Company { get; set; }
        public CityDto[] City { get; set; }
        public CustomerTypeDto[] CustomerType { get; set; }
        public AccountManagerDto[] AccountManager { get; set; }
    }
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
    public class CustomerTypeDto
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }
    public class AccountManagerDto
    {
        public long AccountManagerId { get; set; }
        public string AccountManagerName { get; set; }
    }
}
