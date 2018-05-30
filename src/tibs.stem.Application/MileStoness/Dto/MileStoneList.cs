using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Milestones;

namespace tibs.stem.MileStoness.Dto
{
    [AutoMapFrom(typeof(MileStone))]
    public class MileStoneList : FullAuditedEntityDto
    {
        public virtual int Id { get; set; }
        public virtual string MileStoneCode { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual string SourceTypeName { get; set; }
        public virtual int SourceTypeId { get; set; }
        public virtual int? RottingPeriod { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual bool ResetActivity { get; set; }

    }
}
