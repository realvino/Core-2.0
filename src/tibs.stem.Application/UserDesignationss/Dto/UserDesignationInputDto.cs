using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.UserDesignations;

namespace tibs.stem.UserDesignationss.Dto
{
    [AutoMap(typeof(UserDesignation))]
    public class UserDesignationInputDto
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
