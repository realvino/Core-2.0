using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadReasons
{
    [Table("LeadReason")]
    public class LeadReason : FullAuditedEntity
    {
        public virtual string LeadReasonCode { get; set; }
        public virtual string LeadReasonName { get; set; }
    }
}
