using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewInfoType")]
    public class NewInfoType : FullAuditedEntity
    {
        public virtual string ContactName { get; set; }
        public virtual bool? Info { get; set; }
    }
}
