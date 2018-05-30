using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.CustomerTypes
{
    [Table("CustomerType")]
    public class CustomerType : FullAuditedEntity
    {
        public virtual string CustomerTypeName { get; set; }
        public virtual string Code { get; set; }
    }
}
