using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LeadTypes.Dto
{
    [AutoMapFrom(typeof(LeadType))]
    public class LeadTypeList: FullAuditedEntity
    {
        public int Id { get; set; }
        public virtual string LeadTypeName { get; set; }
        public virtual string LeadTypeCode { get; set; }
    }
}
