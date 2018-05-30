using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Inquirys.Dto
{
    public class GetInquiryInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy";
            }
        }
    }

    public class GetTicketInput
    {
        public string Filter { get; set; }
        public int SalesId { get; set; }

    }

    public class GetInquiryReportInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Name,CompanyName,DepartmentName,TeamName,MileStoneName,SalesMan,CreatedBy";
            }
        }
    }
}
