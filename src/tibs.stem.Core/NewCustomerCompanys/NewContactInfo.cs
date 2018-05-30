using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewContactInfo")]
    public class NewContactInfo : FullAuditedEntity
    {
        [ForeignKey("NewCompanyId")]
        public virtual NewCompany NewCompanys { get; set; }
        public virtual int? NewCompanyId { get; set; }
        [ForeignKey("NewContacId")]
        public virtual NewContact NewContacts { get; set; }
        public virtual int? NewContacId { get; set; }
        [ForeignKey("NewInfoTypeId")]
        public virtual NewInfoType NewInfoTypes { get; set; }
        public virtual int? NewInfoTypeId { get; set; }
        public virtual string InfoData { get; set; }
    }
}
