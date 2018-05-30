using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.UserDesignations
{
    [Table("UserDesignation")]
    public class UserDesignation : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
