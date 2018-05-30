using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.AcitivityTracks;

namespace tibs.stem.ActivityTrackComments
{
    [Table("ActivityTrackComment")]
    public class ActivityTrackComment : FullAuditedEntity
    {
        public virtual string Message { get; set; }

        [ForeignKey("ActivityTrackId")]
        public virtual AcitivityTrack ActivityTrack { get; set; }
        public virtual int ActivityTrackId { get; set; }
    }
}
