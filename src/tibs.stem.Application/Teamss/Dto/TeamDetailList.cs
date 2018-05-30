using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TeamDetails;

namespace tibs.stem.Teamss.Dto
{
    [AutoMap(typeof(TeamDetail))]
    public class TeamDetailList
    {
        public int Id { get; set; }
        public virtual long SalesmanId { get; set; }
        public virtual string Salesman { get; set; }
        public virtual int? TeamId { get; set; }
        public virtual string TeamName { get; set; }
    }
}
