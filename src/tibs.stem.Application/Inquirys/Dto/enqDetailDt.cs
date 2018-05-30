using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Inquirys.Dto
{
   public class enqDetailDt
    {
        public virtual int? CompanyId { get; set; }
        public virtual int? DesignationId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string DesignationName { get; set; }
        public virtual string DepartmentName { get; set; }
        public virtual int? DepartmentId { get; set; }
        public virtual long? AssignedbyId { get; set; }
        public virtual string AssignedTime { get; set; }
        public int? ContactId { get; internal set; }
        public int? TeamId { get; internal set; }
        public virtual string TeamName { get; set; }
        public virtual bool Approved { get; set; }
        public virtual string Estimationvalue { get; set; }
        public virtual int Value { get; set; }
        public string SalesMan { get; internal set; }
        public string StageName { get; internal set; }
        public DateTime? ClosureDate { get; set; }
        public DateTime? LastActivity { get; set; }
        public virtual bool? IsExpire { get; set; }
        public int Estimation { get; set; }
    }
}
