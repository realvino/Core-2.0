using Abp.Application.Services.Dto;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using tibs.stem.CompanyContacts;

namespace tibs.stem.Companies.Dto
{
    [AutoMapFrom(typeof(CompanyContact))]
    public class ContactViewDto:FullAuditedEntityDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPersonName { get; set; }
        public string Desigination { get; set; }
        public string Work_No { get; set; }
        public string Mobile_No { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int CreatorUserId { get; set; }

    }
}
