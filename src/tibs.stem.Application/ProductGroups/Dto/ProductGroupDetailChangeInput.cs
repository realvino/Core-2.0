using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    public class ProductGroupDetailChangeInput
    {
        public virtual int Source { get; set; }
        public virtual int Destination { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual int RowId { get; set; }
    }
}
