using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Orientations.Dto
{
    [AutoMapTo(typeof(Orientation))]
    public class OrientationInputDto
    {
        public int Id { get; set; }
        public virtual string OrientationCode { get; set; }
        public virtual string OrientationName { get; set; }
    }
    
}
