using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Industrys;

namespace tibs.stem.Industries.Dto
{
    [AutoMap(typeof(Industry))]
    public class IndustryListDto : FullAuditedEntityDto
    {
        public new int Id { get; set; }
        public virtual string IndustryCode { get; set; }
        public virtual string IndustryName { get; set; }
    }
}
