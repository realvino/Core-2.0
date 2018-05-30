using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.EnquiryContactss.Dto
{
   
    public class EnquiryContactListDto
    {
        public int Id { get; set; }
        public virtual string EnquiryName { get; set; }
        public virtual string NewContactName { get; set; }
        public virtual int ContactId { get; set; }
        public virtual string NewCompanyName { get; set; }
        public virtual string NewCustomerTypeTitle { get; set; }
        public bool Default { get; internal set; }
    }
}
