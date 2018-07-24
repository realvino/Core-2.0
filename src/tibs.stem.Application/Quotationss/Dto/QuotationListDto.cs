using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Quotations;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMap(typeof(Quotation))]
    public class QuotationListDto
    {
        public int Id { get; set; }
        public virtual string TermsandCondition { get; set; }
        public virtual string RefNo { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual int? NewCompanyId { get; set; }
        public virtual int QuotationStatusId { get; set; }
        public virtual long CreatorUserId { get; set; }
        public virtual long? SalesPersonId { get; set; }
        public virtual string SalesPersonName { get; set; }
        public int? StageId { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string StatusName { get; set; }     
        public virtual DateTime CreationTime { get; set; }
        public virtual string SCreationTime { get; set; }

        public virtual decimal Total { get; set; }
        public virtual string TotalFormat { get; set; }
        public virtual decimal DiscountAmount { get; set; }
        public virtual string DiscountAmountFormat { get; set; }
        public virtual string Email { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual int? AttentionContactId { get; set; }
        public virtual string AttentionName { get; set; }
        public virtual float Discount { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual DateTime? SubmittedDate { get; set; }
        public virtual bool Won { get; set; }
        public virtual DateTime? WonDate { get; set; }
        public virtual bool Lost { get; set; }
        public virtual DateTime? LostDate { get; set; }
        public virtual int? InquiryId { get; set; }
        public virtual string InquiryName { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual int? MileStoneId { get; set; }
        public virtual bool Optional { get; set; }
        public virtual bool Void { get; set; }
        public virtual string PONumber { get; set; }
        public virtual string ReasonRemark { get; set; }
        public virtual int? CompatitorId { get; set; }
        public virtual string CompatitorName { get; set; }
        public virtual string ReasonName { get; set; }
        public int? ReasonId{ get; set; }
        public virtual int Vat { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual bool IsVat { get; set; }
        public string SalespersonImage { get; set; }
        public string DesignerImage { get; set; }
        public string DesignerName { get; set; }
        public virtual DateTime OrgDate { get; set; }
        public virtual bool Revised { get; set; }
        public virtual int? RevisionId { get; set; }
        public virtual string RFQNo { get; set; }
        public virtual string RefQNo { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual bool Negotiation { get; set; }
        public virtual DateTime? NegotiationDate { get; set; }
        public virtual int OverAllDiscountAmount { get; set; }
        public virtual int OverAllDiscountPercentage { get; set; }
        public virtual DateTime? PaymentDate { get; set; }
        public virtual bool DiscountEmail { get; set; }
        public virtual decimal? Probability { get; set; }
        public string Stotal { get; set; }
        public int? LeadStatusId { get; set; }
        public string LeadStatusName { get; set; }
        public virtual string LcNumber { get; set; }
    }
}
