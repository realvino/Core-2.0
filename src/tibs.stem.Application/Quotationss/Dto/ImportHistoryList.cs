using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ImportHistorys;

namespace tibs.stem.Quotationss.Dto
{
    [AutoMapFrom(typeof(ImportHistory))]
    public class ImportHistoryList
    {
        public int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual int? QuotationId { get; set; }
        public virtual string ProductCode { get; set; }
        public virtual string Quantity { get; set; }
        public virtual string SectionName { get; set; }
        public virtual string Status { get; set; }
        public virtual string CreationTime { get; set; }

    }
}
