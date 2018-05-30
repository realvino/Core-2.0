using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.SourceTypes
{
[Table("SourceType")]
public class SourceType : FullAuditedEntity
{
    [Required]
    public virtual string SourceTypeName { get; set; }

    [Required]
    public virtual string SourceTypeCode { get; set; }

}
}


