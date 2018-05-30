using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Collections
{
    [Table("Collection")]
   public class Collection : FullAuditedEntity
    {
        public virtual string CollectionCode { get; set; }
        public virtual string CollectionName { get; set; }
    }
}
