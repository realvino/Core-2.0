using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.AttributeGroupDetails.Dto
{
    [AutoMapFrom(typeof(AttributeGroupDetail))]
    public class AttributeGroupDetailList : FullAuditedEntityDto
    {
        public new int Id { get; set; }
        public virtual int AttributeGroupId { get; set; }
        public virtual string AttributeGroupName { get; set; }
        public virtual int AttributeId { get; set; }
        public virtual string AttributeName { get; set; }

    }
}
