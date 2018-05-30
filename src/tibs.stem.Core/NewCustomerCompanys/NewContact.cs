using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Designations;
using tibs.stem.Industrys;
using tibs.stem.TitleOfCourtes;

namespace tibs.stem.NewCustomerCompanys
{
    [Table("NewContact")]
    public class NewContact : FullAuditedEntity
    {
        public virtual string Name { get; set; }

        [ForeignKey("NewCustomerTypeId")]
        public virtual NewCustomerType NewCustomerTypes { get; set; }
        public virtual int? NewCustomerTypeId { get; set; }

        [ForeignKey("NewCompanyId")]
        public virtual NewCompany NewCompanys { get; set; }
        public virtual int? NewCompanyId { get; set; }

        [ForeignKey("TitleId")]
        public virtual TitleOfCourtesy TitleOfCourtesies { get; set; }
        public virtual int? TitleId { get; set; }

        [ForeignKey("DesignationId")]
        public virtual Designation Designations { get; set; }
        public virtual int? DesignationId { get; set; }

        public virtual string LastName { get; set; }

    }
}
