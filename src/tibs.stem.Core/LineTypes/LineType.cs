using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LineTypes
{
        [Table("LineType")]
        public class LineType : FullAuditedEntity
        {
            [Required]
            public virtual string LineTypeName { get; set; }

            [Required]
            public virtual string LineTypeCode { get; set; }
        }
}
