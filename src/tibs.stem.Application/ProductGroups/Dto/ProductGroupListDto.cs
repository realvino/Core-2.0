using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    [AutoMapFrom(typeof(ProductGroup))]
    public class ProductGroupListDto
    {
        public int Id { get; set; }

        public virtual string ProductGroupName { get; set; }

        public virtual int? FamilyId { get; set; }

        public virtual string AttributeData { get; set; }

        public virtual bool IsEditable { get; set; }

        public virtual int? ProductCategoryId { get; set; }
        public virtual string ProductCategoryName { get; set; }
    }
}
