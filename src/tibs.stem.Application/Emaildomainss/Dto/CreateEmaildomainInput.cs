using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Emaildomains;

namespace tibs.stem.Emaildomainss.Dto
{
     
   [AutoMap(typeof(Emaildomain))]
    public class CreateEmaildomainInput
    {
        public virtual int Id { get; set; }
        public virtual string EmaildomainName { get; set; }
    }
}
