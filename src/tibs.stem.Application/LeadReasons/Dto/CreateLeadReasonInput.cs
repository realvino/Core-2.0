using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadReasons.Dto
{
    [AutoMap(typeof(LeadReason))]
    public class CreateLeadReasonInput
    {
        public int Id { get; set; }
        public virtual string LeadReasonName { get; set; }
        public virtual string LeadReasonCode { get; set; }
    }
}
