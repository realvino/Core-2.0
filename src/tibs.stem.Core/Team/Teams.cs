using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Departments;

namespace tibs.stem.Team
{
    [Table("Teams")]
    public class Teams: FullAuditedEntity
    {
        public virtual string Name { get; set; }

        [ForeignKey("SalesManagerId")]
        public virtual User SalesManager { get; set; }
        public virtual long SalesManagerId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public virtual int DepartmentId { get; set; }
    }
}
