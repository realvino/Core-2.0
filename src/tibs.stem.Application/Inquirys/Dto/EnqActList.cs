using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.AcitivityTracks;
using tibs.stem.ActivityTrackComments;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapFrom(typeof(AcitivityTrack))]
    public class EnqActList
    {
        public virtual int Id { get; set; }
        public virtual int? EnquiryId { get; set; }
        public virtual string EnquiryNo { get; set; }
        public virtual int? ActivityId { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual string ActivityName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Message { get; set; }
        public virtual string FrontMessage { get; set; }
        public virtual string Createdby { get; set; }
        public virtual long? CreatedId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public string ProfilePicture { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string PreviousStatus { get; set; }
        public virtual string CurrentStatus { get; set; }
        public virtual string ColorName { get; set; }
        public virtual string ClassName { get; set; }
    }
    [AutoMapFrom(typeof(ActivityTrackComment))]
    public class EnqActCommentList
    {
        public virtual int Id { get; set; }
        public virtual int ActivityTrackId { get; set; }
        public virtual string Message { get; set; }
        public virtual string Createdby { get; set; }
        public virtual long? CreatedId { get; set; }
        public virtual long? SessionId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class GetEActivity
    {
        public EnqActList Activities { get; set; }
    }
}
