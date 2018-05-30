using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Companies;
using tibs.stem.Designations;
using tibs.stem.TitleOfCourtes;

namespace tibs.stem.CompanyContacts
{
    [Table("CompanyContacts")]
    public class CompanyContact : FullAuditedEntity
    {
        [ForeignKey("CompanyId")]
        public virtual Company Companies { get; set; }
        public virtual int CompanyId { get; set; }

        public virtual string ContactPersonName { get; set; }

        [ForeignKey("DesiginationId")]
        public virtual Designation Desiginations { get; set; }
        public virtual int? DesiginationId { get; set; }

        public virtual string Email { get; set; }
        public virtual string Work_No { get; set; }
        public virtual string Mobile_No { get; set; }

        [ForeignKey("TitleId")]
        public virtual TitleOfCourtesy TitleOfCourtesies { get; set; }
        public virtual int TitleId { get; set; }

        public virtual string Address { get; set; }
        public virtual string Description { get; set; }
    }
}
