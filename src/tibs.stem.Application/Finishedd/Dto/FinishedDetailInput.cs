using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Finish;

namespace tibs.stem.Finishedd.Dto
{
    [AutoMapTo(typeof(FinishedDetail))]
    public class FinishedDetailInput
    {
        public virtual int Id { get; set; }
        public virtual int FinishedId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string GPCode { get; set; }
        public virtual decimal Price { get; set; }
    }
}
