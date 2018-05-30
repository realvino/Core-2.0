using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductFamilys;

namespace tibs.stem.ProductFamilyss.Dto
{
    [AutoMap(typeof(ProductFamily))]
    public class CreateProductFamilyInput
    {
        public int Id { get; set; }
        public virtual string ProductFamilyCode { get; set; }
        public virtual string ProductFamilyName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? Discount { get; set; }
        public virtual int? CollectionId { get; set; }
        public virtual int Warranty { get; set; }

    }
}
