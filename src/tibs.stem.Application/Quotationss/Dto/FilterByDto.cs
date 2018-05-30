using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Quotationss.Dto
{
    public class FilterByDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
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
        public virtual int? QtnDateFilterId { get; set; }
        public virtual int? ClsDateFilterId { get; set; }
        public virtual int? LastActDateFilterId { get; set; }
        public void Normalize()
            {
                if (string.IsNullOrEmpty(Sorting))
                {
                    Sorting = "";
                }
            }
        }
}
