using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryDetails;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapTo(typeof(EnquiryDetail))]
    public class LinkedCompanyInput
    {
        public virtual int? InquiryId { get; set; }
        public virtual int? DesignationId { get; set; }
        public virtual int? CompanyId { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual int? DepartmentId { get; set; }
        public virtual long? AssignedbyId { get; set; }
        public DateTime? AssignedbyDate { get; set; }
        public int Id { get; set; }
        public virtual int? TeamId { get; set; }

    }
}
