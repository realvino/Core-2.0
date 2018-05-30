using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductAttributeGroups;

namespace tibs.stem.ProductAttributeGroupss.Dto
{
    [AutoMap(typeof(ProductAttributeGroup))]
    public class CreateAttributeGroupInput
    {
        public int Id { get; set; }
        public virtual string AttributeGroupName { get; set; }
        public virtual string AttributeGroupCode { get; set; }
       
    }
}
