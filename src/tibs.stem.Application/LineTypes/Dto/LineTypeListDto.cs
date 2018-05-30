using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LineTypes.Dto
{
    [AutoMapFrom(typeof(LineType))]
    public class LineTypeListDto
    {
        public int Id { get; set; }
        public virtual string LineTypeName { get; set; }
        public virtual string LineTypeCode { get; set; }
    }
}
