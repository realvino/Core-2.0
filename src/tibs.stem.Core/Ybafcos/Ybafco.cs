using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Ybafcos
{   
    [Table("Ybafco")]
    public class Ybafco : FullAuditedEntity
    {
        public virtual string YbafcoCode { get; set; }
        public virtual string YbafcoName { get; set; }
    }
}
