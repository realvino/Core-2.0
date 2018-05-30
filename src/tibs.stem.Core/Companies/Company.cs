using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Citys;
using tibs.stem.CustomerTypes;

namespace tibs.stem.Companies
{
    [Table("Companies")]
    public class Company : FullAuditedEntity
    {
        public virtual string CompanyName { get; set; }
        public virtual string CompanyCode { get; set; }
        public virtual string Address { get; set; }
        [ForeignKey("CityId")]
        public virtual City Cities { get; set; }
        public virtual int CityId { get; set; }
        [ForeignKey("CustomerTypeId")]
        public virtual CustomerType CustomerTypes { get; set; }
        public virtual int CustomerTypeId { get; set; }
        public virtual string TelNo { get; set; }
        public virtual string Email { get; set; }
        public virtual string Fax { get; set; }
        public virtual string PhoneNo { get; set; }
        public virtual string Mob_No { get; set; }

        [ForeignKey("AccountManagerId")]
        public virtual User AbpAccountManager { get; set; }
        public virtual long? AccountManagerId { get; set; }
    }
}
