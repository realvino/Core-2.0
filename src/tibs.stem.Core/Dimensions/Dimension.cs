using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace tibs.stem.Dimensions
{
    [Table("Dimension")]
    public  class Dimension : FullAuditedEntity
    {
        public virtual string DimensionCode { get; set; }
        public virtual string DimensionName { get; set; }
    }
}
