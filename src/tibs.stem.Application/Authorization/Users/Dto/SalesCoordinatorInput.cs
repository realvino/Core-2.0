using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.AbpSalesCoOrinators;

namespace tibs.stem.Authorization.Users.Dto
{
    [AutoMapTo(typeof(AbpSalesCoOrinator))]
    public class SalesCoordinatorInput
    {
        public virtual int Id { get; set; }
        public virtual long? CoordinatorId { get; set; }
        public virtual long? UserId { get; set; }

    }
}
