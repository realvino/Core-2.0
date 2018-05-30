using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    public class ProductDetailDtos
    {
        public int Id { get; set; }
        public virtual int GroupId { get; set; }
        public virtual string GroupName { get; set; }
      
    }
}
