using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Authorization.Users;

namespace tibs.stem.AbpSalesCoOrinators
{
    [Table("AbpSalesCoOrinator")]
    public class AbpSalesCoOrinator : FullAuditedEntity
    {
        [ForeignKey("CoordinatorId")]
        public virtual User Coordinator { get; set; }
        public virtual long? CoordinatorId { get; set; }


        [ForeignKey("UserId")]
        public virtual User AbpUser { get; set; }
        public virtual long? UserId { get; set; }

    }
}
