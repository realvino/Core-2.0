using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.LeadDetails;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapTo(typeof(LeadDetail))]
    public class LeadDetailInputDto
    {
        public int Id { get; set; }
        public virtual int? LeadSourceId { get; set; }
        public virtual int? LeadTypeId { get; set; }
        public virtual long? SalesManagerId { get; set; }
        public virtual long? CoordinatorId { get; set; }
        public virtual long? DesignerId { get; set; }
        public virtual int? InquiryId { get; set; }
        public float? EstimationValue { get; set; }
        public string Size { get; set; }
    }
}
