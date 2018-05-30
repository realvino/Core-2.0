using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Industrys;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewCompany")]
    public class NewCompany : FullAuditedEntity
    {
        public virtual string Name { get; set; }

        [ForeignKey("NewCustomerTypeId")]
        public virtual NewCustomerType NewCustomerTypes { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }

        [ForeignKey("AccountManagerId")]
        public virtual User AbpAccountManager { get; set; }
        public virtual long? AccountManagerId { get; set; }
        public virtual string CustomerId { get; set; }

        [ForeignKey("ApprovedById")]
        public virtual User AbpApprovedBy { get; set; }
        public virtual long? ApprovedById { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual string TradeLicense { get; set; }
        public virtual string TRNnumber { get; set; }

        [ForeignKey("IndustryId")]
        public virtual Industry Industries { get; set; }

        public virtual int? IndustryId { get; set; }
        public virtual int Discountable { get; set; }
        public virtual int UnDiscountable { get; set; }
    }
}
