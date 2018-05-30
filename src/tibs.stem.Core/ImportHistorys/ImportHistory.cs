using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;

namespace tibs.stem.ImportHistorys
{
    [Table("ImportHistory")]
    public class ImportHistory : FullAuditedEntity
    {
        public virtual string FileName { get; set; }
        
        [ForeignKey("QuotationId")]
        public virtual Quotation Quotations { get; set; }
        public virtual int? QuotationId { get; set; }

        public virtual string ProductCode { get; set; }
        public virtual string Quantity { get; set; }
        public virtual string SectionName { get; set; }
        public virtual string Status { get; set; }
        
    }
    
}
