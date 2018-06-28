using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Products;

namespace tibs.stem.Finish
{
    [Table("FinishedDetail")]
    public class FinishedDetail: FullAuditedEntity
    {
        [ForeignKey("FinishedId")]
        public virtual Finished Finishedd { get; set; }
        public virtual int FinishedId { get; set; }
        [ForeignKey("ProductId")]
        public virtual int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public virtual string GPCode { get; set; }
        public virtual decimal Price { get; set; }
    }
}
