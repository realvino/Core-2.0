using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tibs.stem.Companies.Dto
{
    [AutoMap(typeof(Company))]  
    public class CreateCompanyInput
    {
        public int Id { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string Address { get; set; }      
        public virtual int CityId { get; set; }
        public virtual long? AccountManagerId { get; set; }     
        public virtual int? CustomerTypeId { get; set; }
        public virtual string TelNo { get; set; }
        public virtual string Email { get; set; }
        public virtual string Fax { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string Mob_No { get; set; }
        public int? CreatorUserId { get; set; }

    }
}
