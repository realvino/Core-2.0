using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Companies.Dto
{
    public class CompanyCreateInput
    {
        public string CompanyName { get; set; }
        public bool InSales { get; set; }
        public int? IndustryId { get; set; }
    }
}
