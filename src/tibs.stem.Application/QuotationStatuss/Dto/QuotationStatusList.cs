﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.QuotationStatuss.Dto
{
    [AutoMapFrom(typeof(QuotationStatus))]
    public class QuotationStatusList
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
