using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Finish;

namespace tibs.stem.Finishedd.Dto
{
    [AutoMapTo(typeof(Finished))]
    public class FinishedInput
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
