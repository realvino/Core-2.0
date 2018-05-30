using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Quotationss.Dto
{
    public class TeamReportInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public int TeamId { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "";
            }

        }
    }
}
