using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryStatuss;

namespace tibs.stem.EnquiryStatusss.Dto
{
    [AutoMap(typeof(EnquiryStatus))]
    public class EnquiryStatusListDto : FullAuditedEntityDto
    {
        public new int Id { get; set; }
        public virtual string EnqStatusCode { get; set; }
        public virtual string EnqStatusName { get; set; }
        public virtual string EnqStatusColor { get; set; }
        public decimal? Percentage { get; set; }
        public virtual int? StagestateId { get; set; }
        public virtual string StagestateName { get; set; }


    }
}
