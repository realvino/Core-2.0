using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewAddressInfo")]
    public class NewAddressInfo : FullAuditedEntity
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
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        [ForeignKey("CityId")]
        public virtual City Citys { get; set; }
        public virtual int? CityId { get; set; }
    }
}
