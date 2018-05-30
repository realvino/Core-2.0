using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Industrys
{
    [Table("Industry")]
    public class Industry : FullAuditedEntity
    {
        [Required]
        public virtual string IndustryCode { get; set; }

        [Required]
        public virtual string IndustryName { get; set; } 

    }
}

