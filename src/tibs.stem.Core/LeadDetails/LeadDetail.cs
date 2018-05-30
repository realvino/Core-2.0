using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.LeadSources;
using tibs.stem.LeadTypes;
using tibs.stem.Authorization.Users;
using tibs.stem.Inquirys;

namespace tibs.stem.LeadDetails
{
    [Table("LeadDetails")]
    public class LeadDetail : FullAuditedEntity
    {
        [ForeignKey("InquiryId")]
        public virtual Inquiry Inquirys { get; set; }
        public virtual int? InquiryId { get; set; }

        [ForeignKey("LeadSourceId")]
        public virtual LeadSource LeadSources { get; set; }
        public virtual int? LeadSourceId { get; set; }

        [ForeignKey("LeadTypeId")]
        public virtual LeadType LeadTypes { get; set; }
        public virtual int? LeadTypeId { get; set; }

        [ForeignKey("SalesManagerId")]
        public virtual User SalesManagers { get; set; }
        public virtual long? SalesManagerId { get; set; }

        [ForeignKey("CoordinatorId")]
        public virtual User Coordinators{ get; set; }
        public virtual long? CoordinatorId { get; set; }

        [ForeignKey("DesignerId")]
        public virtual User Designers { get; set; }
        public virtual long? DesignerId { get; set; }

        public float? EstimationValue { get; set; }
        public string Size { get; set; }
    }
}
