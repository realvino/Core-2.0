using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadTypes
{
    [Table("LeadType")]
    public class LeadType : FullAuditedEntity
    {
        public virtual string LeadTypeCode { get; set; }
        public virtual string LeadTypeName { get; set; }
    }
}
