using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.Products.Dto
{
    public class GetProductInput :  PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ProductCode,ProductSpecificationName,SuspectCode,Gpcode,Price,CategoryName,BafcoMade";
            }
        }

    }
}
