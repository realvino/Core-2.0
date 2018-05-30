using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductSpecificationDetails;
using tibs.stem.ProductSpecifications;
using tibs.stem.ProdutSpecLinks;

namespace tibs.stem.ProductSpecificationss.Dto
{
    [AutoMap(typeof(ProductSpecification))]
    public class CreateProductSpecification
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int? ProductGroupId { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Reset { get; set; }
        public virtual bool BafcoMade { get; set; }

    }

    [AutoMap(typeof(ProdutSpecLink))]
    public class CreateProductSpecificationInput
    {
        public virtual int Id { get; set; }
        public virtual int ProductGroupId { get; set; }
        public virtual int? ProductSpecificationId { get; set; }
        public virtual int AttributeGroupId { get; set; }
        public virtual int AttributeId { get; set; }

    }
}
