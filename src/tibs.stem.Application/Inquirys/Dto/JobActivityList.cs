using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
     
   [AutoMapFrom(typeof(JobActivity))]
    public class JobActivityList : FullAuditedEntityDto
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Remark { get; set; }
        public virtual long? DesignerId { get; set; }
        public virtual string DesignerName { get; set; }
        public virtual int? InquiryId { get; set; }
        public virtual string InquiryName { get; set; }
        public virtual bool Isopen { get; set; }
        public virtual DateTime? AllottedDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string JobNumber { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual string SallottedDate { get; set; }
        public virtual string SendDate { get; set; }
        public virtual string SstartDate { get; set; }
        public virtual string ScreationTime { get; set; }
        public virtual bool NotApproved { get; set; }

    }
}
