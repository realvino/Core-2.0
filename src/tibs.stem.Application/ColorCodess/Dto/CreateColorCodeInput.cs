using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ColorCodes;

namespace tibs.stem.ColorCodess.Dto
{
    [AutoMapTo(typeof(ColorCode))]
    public class CreateColorCodeInput
    {
        public int Id { get; set; }
        public virtual string Component { get; set; }

        public virtual string Code { get; set; }
        public virtual string Color { get; set; }
    }
}
