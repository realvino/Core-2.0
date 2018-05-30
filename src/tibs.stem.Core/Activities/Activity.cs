using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Activities
{
    [Table("Activity")]
    public class Activity : FullAuditedEntity
    {
        [Required]
        public virtual string ActivityName { get; set; }

        [Required]
        public virtual string ActivityCode { get; set; }
        public virtual string ColorCode { get; set; }
    }
}
