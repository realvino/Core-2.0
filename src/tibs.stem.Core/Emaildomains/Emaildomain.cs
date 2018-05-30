using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Emaildomains
{
     
     [Table("Emaildomain")]
    public class Emaildomain : FullAuditedEntity
    {  
        public virtual string EmaildomainName { get; set; }
    }
}
