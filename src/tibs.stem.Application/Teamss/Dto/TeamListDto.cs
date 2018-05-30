using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Team;

namespace tibs.stem.Teamss.Dto
{
    [AutoMap(typeof(Teams))]
    public class TeamListDto
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual long SalesManagerId { get; set; }
        public virtual string SalesManager { get; set; }
        public virtual int DepartmentId { get; set; }
        public virtual string DepartmentName { get; set; }

    }
}
