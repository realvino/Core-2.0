﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Sections;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMap(typeof(Section))]
    public class CreateSectionInput
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? QuotationId { get; set; }

    }
}
