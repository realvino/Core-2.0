using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class GetClosureTicketInput
    {
        public string Filter { get; set; }
        public string ClosureDate { get; set; }
        public int TeamId { get; set; }
        public int SalesId { get; set; }
        public int TypeId { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
    }
}
