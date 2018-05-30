using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Designations;
using tibs.stem.Industrys;

namespace tibs.stem.Inquirys.Dto
{
    [AutoMapTo(typeof(Designation))]
    public class DesignationInputDto
    {
        public string DesignationCode { get; set; }
        public string DesiginationName { get; set; }
    }

}
