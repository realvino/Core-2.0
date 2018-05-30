using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Products.Dto
{
    public class GetProductGroupss
    {
        public int ProductGroupId { get; set; }
        public int ProductSubGroupId { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductSubGroupName { get; set; }

    }
}
