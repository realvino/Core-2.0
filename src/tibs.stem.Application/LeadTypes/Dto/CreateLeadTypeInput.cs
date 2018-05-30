using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadTypes.Dto
{
    [AutoMap(typeof(LeadType))]
    public class CreateLeadTypeInput
    {
        public int Id { get; set; }
        public virtual string LeadTypeName { get; set; }
        public virtual string LeadTypeCode { get; set; }
    }
}
