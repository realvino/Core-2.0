using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Tenants.Dashboard.Dto
{
    public class GetConversionRatioListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal STotal { get; set; }
        public decimal WTotal { get; set; }
    }
    public class GetConvertionratio
    {
        public Array ConversionRatio { get; set; }
        public Array Catagries { get; set; }
    }
    public class ConversionRatio
    {
        public Array Data { get; set; }
        public string Name { get; set; }
    }
}
