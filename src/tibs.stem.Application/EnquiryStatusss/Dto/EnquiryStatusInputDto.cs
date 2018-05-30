using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryStatuss;

namespace tibs.stem.EnquiryStatusss.Dto
{
    [AutoMapTo(typeof(EnquiryStatus))]
    public class EnquiryStatusInputDto
    {
        public int Id { get; set; }
        public virtual string EnqStatusCode { get; set; }
        public virtual string EnqStatusName { get; set; }
        public virtual string EnqStatusColor { get; set; }
        public decimal? Percentage { get; set; }
        public virtual int? StagestateId { get; set; }

    }
}
