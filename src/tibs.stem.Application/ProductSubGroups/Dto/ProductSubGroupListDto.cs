using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.ProductSubGroups.Dto
{
    [AutoMapFrom(typeof(ProductSubGroup))] 
    public class ProductSubGroupListDto
    {
        public int Id { get; set; }

        public virtual string ProductSubGroupName { get; set; }

        public virtual string ProductSubGroupCode { get; set; }

        public virtual int GroupId { get; set; }

        public virtual string ProductGroupName { get; set; } 

    }
}
