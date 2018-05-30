using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Industrys;

namespace tibs.stem.Industries.Dto
{
    [AutoMapTo(typeof(Industry))]
    public class IndustryInputDto
    {
        public int Id { get; set; }
        public virtual string IndustryCode { get; set; }
        public virtual string IndustryName { get; set; }
    }
}
