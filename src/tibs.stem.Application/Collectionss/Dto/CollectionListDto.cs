using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Collections;

namespace tibs.stem.Collectionss.Dto
{
    [AutoMapFrom(typeof(Collection))]
    public class CollectionListDto : FullAuditedEntityDto
    {
        public new int Id { get; set; }
        public virtual string CollectionCode { get; set; }
        public virtual string CollectionName { get; set; }
    }
}
