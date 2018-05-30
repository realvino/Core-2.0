using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductSubGroups;
using tibs.stem.Views;

namespace tibs.stem.Viewss.Dto
{
    [AutoMapFrom(typeof(View))]
    public class ViewListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public virtual bool IsEditable { get; set; }
        public virtual bool IsEnquiry { get; set; }
        public virtual int? QuotationStatusId { get; set; }
        public virtual long? AllPersonId { get; set; }
        public virtual int? GraterAmount { get; set; }
        public virtual int? LessAmount { get; set; }
        public virtual int? DateFilterId { get; set; }
        public virtual int? ClosureDateFilterId { get; set; }
        public virtual int? LastActivityDateFilterId { get; set; }
        public string CreationTime { get; set; }
        public string CreatedBy { get; set; }
        public string StatusName { get; set; }
        public string PersonName { get; set; }
        public string FilterName { get; set; }
        public virtual int? EnqStatusId { get; set; }
        public virtual string UserIds { get; set; }
        public virtual string QuotationCreateBy { get; set; }
        public virtual string QuotationStatus { get; set; }
        public virtual string Salesman { get; set; }
        public virtual string InquiryCreateBy { get; set; }
        public virtual string PotentialCustomer { get; set; }
        public virtual string MileStoneName { get; set; }
        public virtual string EnquiryStatus { get; set; }
        public virtual string TeamName { get; set; }
        public virtual string Coordinator { get; set; }
        public virtual string Designer { get; set; }
        public virtual string DesignationName { get; set; }
        public virtual string Emirates { get; set; }
        public virtual string DepatmentName { get; set; }
        public virtual string Categories { get; set; }
        public virtual string Status { get; set; }
        public virtual string WhyBafco { get; set; }
        public virtual string Probability { get; set; }
        public virtual string QuotationCreation { get; set; }
        public virtual string InquiryCreation { get; set; }
        public virtual string ClosureDate { get; set; }
        public virtual string LastActivity { get; set; }
        public virtual string StatusForQuotation { get; set; }


    }
}
