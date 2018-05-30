using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ColorCodes;

namespace tibs.stem.ColorCodess.Dto
{
    [AutoMapFrom(typeof(ColorCode))]
    public class ColorCodeList : FullAuditedEntityDto
    {
        public int Id { get; set; }

        public virtual string Component { get; set; }

        public virtual string Code { get; set; }
        public virtual string Color { get; set; }

    }
   
}
