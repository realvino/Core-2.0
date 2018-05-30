using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.ProductGroups.Dto
{
    public class GetProductGroup
    {
        public ProductGroupListDto productGroup { get; set; }
        public FamilyData FamilyDatas { get; set; }
        public categoryData CategoryDatas { get; set; }     
        public Array ProductGroupDetails { get; set; }
    }
}
