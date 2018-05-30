using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;

namespace tibs.stem.Inquirys
{
     
   [Table("JobActivity")]
    public class JobActivity : FullAuditedEntity
    {
        public virtual string JobNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Remark { get; set; }
        [ForeignKey("DesignerId")]
        public virtual User Designer { get; set; }
        public virtual long? DesignerId { get; set; }
        [ForeignKey("InquiryId")]
        public virtual Inquiry Inquiry { get; set; }
        public virtual int? InquiryId { get; set; }
        public virtual bool Isopen { get; set; }        
        public virtual DateTime? AllottedDate { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        
    }
    
}
