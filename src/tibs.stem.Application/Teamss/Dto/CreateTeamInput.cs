using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Team;

namespace tibs.stem.Teamss.Dto
{
    [AutoMapTo(typeof(Teams))]
    public class CreateTeamInput
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual long SalesManagerId { get; set; }
        public virtual int DepartmentId { get; set; }
       
             
    }
}
