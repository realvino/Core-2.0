using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.PriceLevels;

namespace tibs.stem.PriceLevelss.Dto
{
    [AutoMapFrom(typeof(PriceLevel))]
   public  class PriceLevelListDto
    {
        public int Id { get; set; }
        public virtual string PriceLevelCode { get; set; }
        public virtual string PriceLevelName { get; set; }
        public virtual bool DiscountAllowed { get; set; }
    }
}
