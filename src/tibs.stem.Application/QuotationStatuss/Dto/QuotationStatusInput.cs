using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationStatuss;

namespace tibs.stem.QuotationStatuss.Dto
{
    [AutoMap(typeof(QuotationStatus))]
    public class QuotationStatusInput
    {
        public int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }


    }
}
