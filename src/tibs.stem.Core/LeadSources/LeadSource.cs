using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadSources
{
    public class LeadSource : FullAuditedEntity
    {
        public string LeadSourceCode { get; set; }

        public string LeadSourceName { get; set; }

    }
}
