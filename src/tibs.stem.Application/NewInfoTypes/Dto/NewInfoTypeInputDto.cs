using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.NewCustomerCompanys;

namespace tibs.stem.NewInfoTypes.Dto
{
    [AutoMapTo(typeof(NewInfoType))]
    public class NewInfoTypeInputDto
    {
        public int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual bool Info { get; set; }
    }
}
