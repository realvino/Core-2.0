﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.OpportunitySources;

namespace tibs.stem.OpportunitySourcess.Dto
{
    [AutoMap(typeof(OpportunitySource))]
    public class OpportunitySourceInput
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
