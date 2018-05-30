using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

namespace tibs.stem.ProductSpecificationss.Dto
{
    public class GetProductSpecInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
        public string Filter { get; set; }
        public void Normalize()
        {

            if (string.IsNullOrEmpty(Sorting))

            {

                Sorting = "Name";

            }

        }
    }
}