using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.NewCompanyContacts.Dto
{
     public class CompanyInputDto
    {
        public string CompanyName { get; set; }
    }
    public class ContactInputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NewCompanyId { get; set; }
        
    }
}
