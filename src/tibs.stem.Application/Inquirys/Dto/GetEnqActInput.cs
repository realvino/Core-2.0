using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Inquirys.Dto
{
    public class GetEnqActOverAllInput
    {
        public string Filter { get; set; }
        public int SalesmanId { get; set; }
    }
    public class GetEnqActInput
    {
        public string Filter { get; set; }
        public int EnqId { get; set; }

    }
    public class GetEnqActCommentInput 
    {
        public string Filter { get; set; }
        public int ActId { get; set; }
    }
}
