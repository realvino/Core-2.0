using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.LeadStatuss
{
    [Table("LeadStatus")]
   public class LeadStatus : FullAuditedEntity
    {
        public virtual string LeadStatusCode { get; set; }
        public virtual string LeadStatusName { get; set; }
    }
}
