using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ColorGraphs
{
    [Table("ColorGraph")]
    public class ColorGraph : FullAuditedEntity
    {
        public virtual string Color { get; set; }
    }
}
