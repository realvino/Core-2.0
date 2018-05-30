using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Dimensions.Dto
{
    [AutoMapTo(typeof(Dimension))]
    public class DimensionInputDto
    {
        public int Id { get; set; }
        public virtual string DimensionCode { get; set; }
        public virtual string DimensionName { get; set; }
    }

}
