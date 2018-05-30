using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Departments
{
    [Table("Department")]
    public class Department : FullAuditedEntity
    {
        public virtual string DepatmentName { get; set; }
       
        public virtual string DepartmentCode { get; set; }


    }
}
