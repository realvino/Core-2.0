using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class GetSalesLeadList
    {
        public Array LeadDevelop { get; set; }
        public Array Catagries { get; set; }

    }
    public class SalesLeadListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Counts { get; set; }
        public int Total { get; set; }
        public string SourceName { get; set; }
    }
    public class SalesLeads
    {
        public Array Data { get; set; }
        public string Name { get; set; }
    }

    public class LeadsBreakdown
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
