using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Sources.Dto
{
    [AutoMap(typeof(Source))]
    public class SourceListDto 
    {
        public int Id { get; set; }
        public virtual string SourceName { get; set; }
        public virtual string SourceCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual int TypeId { get; set; }
        public virtual string TypeName { get; set; }

    }
}
