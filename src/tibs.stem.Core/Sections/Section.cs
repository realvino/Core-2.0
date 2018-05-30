using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;

namespace tibs.stem.Sections
{
    [Table("Section")]
    public class Section : FullAuditedEntity
    {
        public virtual string Name { get; set; }

        [ForeignKey("QuotationId")]
        public virtual Quotation quotation { get; set; }
        public virtual int QuotationId { get; set; }
    }
}
