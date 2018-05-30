using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Views
{
    [Table("DateFilter")]
    public class DateFilter : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
