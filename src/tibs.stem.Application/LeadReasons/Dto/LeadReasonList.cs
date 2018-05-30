using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadReasons.Dto
{
    [AutoMapFrom(typeof(LeadReason))]
    public class LeadReasonList : FullAuditedEntity
    {
        public int Id { get; set; }
        public virtual string LeadReasonName { get; set; }
        public virtual string LeadReasonCode { get; set; }
    }
}
