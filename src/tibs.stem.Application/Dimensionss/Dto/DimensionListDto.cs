using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace tibs.stem.Dimensions.Dto
{
    [AutoMap(typeof(Dimension))]
    public class DimensionListDto : FullAuditedEntityDto
    {
        public int Id { get; set; }
        public virtual string DimensionCode { get; set; }
        public virtual string DimensionName { get; set; }
    }
    
}
