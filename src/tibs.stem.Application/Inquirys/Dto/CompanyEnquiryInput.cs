﻿using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Inquirys.Dto
{
    public class CompanyEnquiryInput
    {
        public string Filter { get; set; }
    }
    public class CompanyInquiryInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CompanyName,EnquiryName,AccountManager,EnquiryCount,EnquiryWonCount";
            }
        }
    }
}
