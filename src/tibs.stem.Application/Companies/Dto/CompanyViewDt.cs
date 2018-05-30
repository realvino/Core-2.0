using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace tibs.stem.Companies.Dto
{
    [AutoMapFrom(typeof(Company))]
    public class CompanyViewDt:FullAuditedEntityDto
    {
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string City { get; set; }
        public string CustomerType { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string TelNo { get; set; }
        public string Country{get;set;}
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CreatorUserId { get; set; }
        public string AccountManager { get; set; }

    }
}
