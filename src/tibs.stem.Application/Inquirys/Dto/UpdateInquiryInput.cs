using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class UpdateInquiryInput
    {
        public int Id { get; set; }
        public virtual string UpdateStatusName { get; set; }
        public virtual string CurrentStatusName { get; set; }

    }
}
