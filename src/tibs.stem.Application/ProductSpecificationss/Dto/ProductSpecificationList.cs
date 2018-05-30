using Abp.AutoMapper;
using tibs.stem.ProductSpecificationDetails;
using tibs.stem.ProductSpecifications;

namespace tibs.stem.ProductSpecificationss.Dto
{
    [AutoMap(typeof(ProductSpecification))]
    public class ProductSpecificationList
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int? ProductGroupId { get; set; }
        public virtual string ProductGroupName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Reset { get; set; }
        public virtual bool BafcoMade { get; set; }


    }

    public class ProductSpecificationDetailList
    {
        public virtual int Id { get; set; }
        public virtual int AttributeGroupId { get; set; }
        public virtual string ProductSpecificationName { get; set; }
        public virtual string AttributeGroupName { get; set; }
        public virtual string AttributeGroupCode { get; set; }
     
    }

}
