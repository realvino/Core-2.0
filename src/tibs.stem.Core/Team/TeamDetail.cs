using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;
using tibs.stem.Team;

namespace tibs.stem.TeamDetails
{
    [Table("TeamDetail")]
    public class TeamDetail: FullAuditedEntity
    {
        [ForeignKey("TeamId")]
        public virtual Teams Team { get; set; }
        public virtual int? TeamId { get; set; }

        [ForeignKey("SalesmanId")]
        public virtual User Salesman { get; set; }
        public virtual long SalesmanId { get; set; }
    }
}
