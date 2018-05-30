using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users.Profile.Dto;
using tibs.stem.LeadDetails;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapFrom(typeof(LeadDetail))]
    public class LeadDetailListDto
    {
        public virtual int? LeadSourceId { get; set; }
        public string LeadSourceName { get; set; }
        public virtual int? LeadTypeId { get; set; }
        public string LeadTypeName { get; set; }
        public virtual long? SalesManagerId { get; set; }
        public string SalesManagerName { get; set; }
        public virtual long? CoordinatorId { get; set; }
        public string CoordinatorName { get; set; }
        public virtual long? DesignerId { get; set; }
        public virtual int? InquiryId { get; set; }
        public string DesignerName { get; set; }
        public float? EstimationValue { get; set; }
        public string Size { get; set; }
        public int Id { get; internal set; }
        public string CoordinatorImage { get; set; }
        public string DesignerImage { get; set; }

    }
}
