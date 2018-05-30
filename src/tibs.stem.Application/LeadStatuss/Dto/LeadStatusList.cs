using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.LeadStatuss.Dto
{
    [AutoMapFrom(typeof(LeadStatus))]
    public class LeadStatusList
    {
        public int Id { get; set; }
        public virtual string LeadStatusCode { get; set; }
        public virtual string LeadStatusName { get; set; }
    }
}
