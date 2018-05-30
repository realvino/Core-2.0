using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Quotationss.Dto
{     
   public class GetProductFilterInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int ProductCategoryId { get; set; }
        public int? ProductSpecificationId { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ProductCode,ProductSpecificationName,SuspectCode,Gpcode,Price,CategoryName,BafcoMade,CreationTime";
            }
        }
    }
}
