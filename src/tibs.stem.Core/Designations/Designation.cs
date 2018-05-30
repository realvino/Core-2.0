using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Designations
{
    public class Designation : FullAuditedEntity
    { 
        public string DesignationCode { get; set; }
        public string DesiginationName { get; set; }

    }
}
