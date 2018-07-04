using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class LostReasonGraphList
    {
        public string Reason { get; set; }
        public decimal Total { get; set; }
        public string Color { get; set; }
    }
    public class GetLeadSummaryGraphList
    {
        public int StageId { get; set; }
        public string StageName { get; set; }
        public decimal Total { get; set; }
        public string Color { get; set; }
    }
    public class GraphInput
    {
        public string UserId { get; set; }
        public string TeamId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
