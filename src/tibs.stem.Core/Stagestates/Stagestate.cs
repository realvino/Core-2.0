using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Stagestates
{
     
   [Table("Stagestate")]
    public class Stagestate : FullAuditedEntity
    {
        public virtual string Name { get; set; }
    }
}
