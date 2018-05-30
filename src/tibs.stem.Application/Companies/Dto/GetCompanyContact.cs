using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Companies.Dto
{
    public class GetCompanyContact
    {
        public CreateContactInput Contact { get; set; }
        public CompanyDto[] Company { get; set; }
        public TitleDto[] Title { get; set; }
        public DesiginationDto[] Desigination { get; set; }


    }
    public class TitleDto
    {
        public int TitleId { get; set; }
        public string Title { get; set; }

    }
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

    }
    public class DesiginationDto
    {
        public int DesiginationId { get; set; }
        public string Desigination { get; set; }
    }
}
