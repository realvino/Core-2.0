using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.AttributeGroupDetails.Dto
{
    [AutoMapTo(typeof(AttributeGroupDetail))]
    public class AttributeGroupDetailInput
    {
        public virtual int Id { get; set; }
        public virtual int AttributeGroupId { get; set; }
        public virtual int AttributeId { get; set; }
        
    }
}
