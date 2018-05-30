using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
    public class EnquiryDt
    {
        public virtual int? CompanyId { get; set; }
        public virtual int? DesignationId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string DesignationName { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual int? DepartmentId { get; set; }
        public virtual long? AssignedbyId { get; set; }
        public virtual string AssignedTime { get; set; }
        public virtual int? ContactId { get; set; }
        public virtual int? LeadTypeId { get; set; }
        public virtual string LeadTypeName { get; set; }
        public virtual int? CompatitorsId { get; set; }
        public virtual string CompatitorName { get; set; }
        public virtual decimal EstimationValue { get; set; }
        public virtual string Size { get; set; }
        public virtual string Summary { get; set; }
        public int? TeamId { get; internal set; }
        public virtual string TeamName { get; set; }
        public virtual string ContactName { get;  set; }
        public virtual bool Approved { get; set; }
        public DateTime? ClosureDate { get; set; }
        public DateTime? LastActivity { get; set; }


    }
}
