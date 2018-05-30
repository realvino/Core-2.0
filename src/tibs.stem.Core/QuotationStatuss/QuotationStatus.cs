using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.QuotationStatuss
{
    [Table("QuotationStatus")]
    public class QuotationStatus : FullAuditedEntity
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

    }
}
