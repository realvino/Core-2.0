using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.ProductGroups;

namespace tibs.stem.ProductSubGroups
{
    [Table("ProductSubGroup")] 
    public class ProductSubGroup : FullAuditedEntity
    {
        [Required]
        public virtual string ProductSubGroupName { get; set; }

        [Required]
        public virtual string ProductSubGroupCode { get; set; }

        [ForeignKey("GroupId")]
        public virtual ProductGroup productGroups { get; set; } 
        public virtual int GroupId { get; set; } 

    }
}
