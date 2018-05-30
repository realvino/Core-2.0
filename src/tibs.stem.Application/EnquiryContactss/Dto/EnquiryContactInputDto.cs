using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryContacts;

namespace tibs.stem.EnquiryContactss.Dto
{
    [AutoMapTo(typeof(EnquiryContact))]
    public class EnquiryContactInputDto
    {
        public int Id { get; set; }
        public virtual int InquiryId { get; set; }
        public virtual int ContactId { get; set; }
    }
}
