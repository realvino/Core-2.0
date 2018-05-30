using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using tibs.stem.Products;

namespace tibs.stem.ProductImageUrls
{
    [Table("ProductImageUrl")]
    public class ProductImageUrl: FullAuditedEntity
    {
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}
