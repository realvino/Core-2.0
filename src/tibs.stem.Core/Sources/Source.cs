using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.SourceTypes;

namespace tibs.stem.Sources
{ 
[Table("Source")]
public class Source : FullAuditedEntity
{
    [Required]
    public virtual string SourceName { get; set; }

    [Required]
    public virtual string SourceCode { get; set; }

    [ForeignKey("TypeId")]
    public virtual SourceType SourceTypes { get; set; }
    public virtual int TypeId { get; set; }

    public virtual string ColorCode { get; set; }

    }
}