using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.TemporaryProducts
{
    [Table("TemporaryProductImages")]
    public class TemporaryProductImage : FullAuditedEntity
    {
        [ForeignKey("TemporaryProductId")]
        public virtual TemporaryProduct TemporaryProducts { get; set; }
        public virtual int TemporaryProductId { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}
