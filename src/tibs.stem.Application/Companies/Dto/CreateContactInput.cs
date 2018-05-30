using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using tibs.stem.CompanyContacts;

namespace tibs.stem.Companies.Dto
{
    [AutoMap(typeof(CompanyContact))]
    public class CreateContactInput
    {
        public int Id { get; set; }
        public virtual int CompanyId { get; set; }
        public virtual string ContactPersonName { get; set; }
        
        public virtual int? DesiginationId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Work_No { get; set; }
        public virtual string Mobile_No { get; set; }

        public virtual int TitleId { get; set; }
        public virtual string Address { get; set; }
        public virtual string Description { get; set; }
        public int CreatorUserId { get; set; }

    }
}
