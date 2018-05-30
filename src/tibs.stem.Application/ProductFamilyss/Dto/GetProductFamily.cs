using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.ProductFamilyss.Dto
{
    public class GetProductFamily
    {
        public CreateProductFamilyInput productFamily { get; set; }

        public Collectiondata Collectiondatas { get; set; }
    }
}
