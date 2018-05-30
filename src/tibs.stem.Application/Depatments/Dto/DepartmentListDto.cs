using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Departments;

namespace tibs.stem.Depatments.Dto
{
    [AutoMapFrom(typeof(Department))]
    public class DepartmentListDto
    {
        public int Id { get; set; }
        public virtual string DepatmentName { get; set; }
        public virtual string DepartmentCode { get; set; }
        
    }
}
