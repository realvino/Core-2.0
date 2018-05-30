using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Companies.Dto
{
    public class GetContactInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int CompanyId { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CompanyName,Address,ContactPersonName,Desigination,Work_No,Mobile_No";
            }
        }
    }
}
