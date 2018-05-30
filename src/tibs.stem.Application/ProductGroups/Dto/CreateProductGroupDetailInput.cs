using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    [AutoMapTo(typeof(ProductGroupDetail))]
    public class CreateProductGroupDetailInput 
    {
        public virtual int AttributeGroupId { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual string Metedata { get; set; }
        public int Id { get; internal set; }
    }
}
