using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductAttributeGroupss.Dto
{
    public class AttributeGroupDetailListDto
    {
        public  int Id { get; set; }
        public virtual int AttributeGroupId { get; set; }
        public virtual int AttributeId { get; set; }
        public virtual string AttributeGroupName { get; set; }
        public virtual string AttributeName { get; set; }
        public virtual string AttributeCode { get; set; }
        public virtual string ImgPath { get; set; }
        public virtual bool Selected { get; set; }


    }
}
