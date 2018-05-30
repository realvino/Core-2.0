using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace tibs.stem.Orientations
{
    [Table("Orientation")]
    public  class Orientation : FullAuditedEntity
    {
        public virtual string OrientationCode { get; set; }
        public virtual string OrientationName { get; set; }
    }
}
