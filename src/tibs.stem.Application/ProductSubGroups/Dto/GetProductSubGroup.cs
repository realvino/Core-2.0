using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.ProductSubGroups.Dto
{
    public class GetProductSubGroup
    {
        public ProductSubGroupListDto productSubGroup { get; set; } 
    }
}