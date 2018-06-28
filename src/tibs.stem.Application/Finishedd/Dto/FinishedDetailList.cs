using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Finish;

namespace tibs.stem.Finishedd.Dto
{
    [AutoMapFrom(typeof(FinishedDetail))]
    public class FinishedDetailList
    {
        public virtual int Id { get; set; }
        public virtual string GPCode { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int FinishedId { get; set; }
        public virtual string FinishedCode { get; set; }
        public virtual string FinishedName { get; set; }
        public virtual string FinishedDescription { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string ProductSuspectCode { get; set; }
        public virtual string ProductGpcode { get; set; }
        public virtual string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        
    }
}
