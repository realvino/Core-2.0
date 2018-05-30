
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Departments;

namespace tibs.stem.Depatments.Dto
{
    [AutoMapTo(typeof(Department))]
    public class DepartmentInputDto
    {
        public int Id { get; set; }
        public virtual string DepatmentName { get; set; }
        public virtual string DepartmentCode { get; set; }
       
    }
}
