using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Designations;

namespace tibs.stem.ContactDesignation.Dto
{
    [AutoMap(typeof(Designation))]
    public class ContactDesignationInput
    {
        public int Id { get; set; }
        public string DesignationCode { get; set; }
        public string DesiginationName { get; set; }
    }
}
