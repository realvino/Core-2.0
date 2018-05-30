using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.AcitivityTracks;
using tibs.stem.ActivityTrackComments;

namespace tibs.stem.Inquirys.Dto
{

    [AutoMapTo(typeof(AcitivityTrack))]
    public class EnqActCreate
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Message { get; set; }
        public virtual int ActivityId { get; set; }
        public virtual int EnquiryId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string PreviousStatus { get; set; }
        public virtual string CurrentStatus { get; set; }

    }

    [AutoMapTo(typeof(ActivityTrackComment))]
    public class EnqActCommentCreate
    {
        public int Id { get; set; }
        public virtual int ActivityTrackId { get; set; }
        public virtual string Message { get; set; }

    }
    public class CompanyUpdateInput
    {
        public int EnquiryId { get; set; }
        public int? CompanyId { get; set; }
        public int? DesignationId { get; set; }


    }
}
