using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Emaildomains;

namespace tibs.stem.Emaildomainss.Dto
{
     
    [AutoMapFrom(typeof(Emaildomain))]
    public class EmaildomainList 
    {
        public virtual int Id { get; set; }
        public virtual string EmaildomainName { get; set; }

    }
}
