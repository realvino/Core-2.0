using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewCustomerType")]
    public class NewCustomerType : FullAuditedEntity
    {
        public virtual string Title { get; set; }
        public virtual bool? Company { get; set; }

    }
}
