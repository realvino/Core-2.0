using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.LineTypes.Dto
{
    [AutoMapTo(typeof(LineType))]
    public class LineTypeInputDto
    {
        public int Id { get; set; }
        public virtual string LineTypeName { get; set; }
        public virtual string LineTypeCode { get; set; }
    }
}
