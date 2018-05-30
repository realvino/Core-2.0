using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.NewInfoTypes.Dto
{
    public class NewInfoTypeListDto
    {
        public int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual bool Info { get; set; }
    }
}
