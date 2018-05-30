using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ColorCodes
{
    [Table("ColorCode")]
    public class ColorCode : FullAuditedEntity
    {
        public virtual string Component { get; set; }

        public virtual string Code { get; set; }
        public virtual string Color { get; set; }
    }
   
}
