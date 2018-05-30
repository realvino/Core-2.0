﻿using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Inquirys.Dto
{
    public class GetoverallJobActivityInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int DesignerId { get; set; }
        public void Normalize()

        {

            if (string.IsNullOrEmpty(Sorting))

            {

                Sorting = "Title,DesignerName,InquiryName,Isopen,JobNumber";

            }

        }
    }
}
