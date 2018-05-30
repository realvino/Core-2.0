using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tibs.stem.Select2.Dtos 
{
    public class Select2Result
    {
        public datadto[] select2data { get; set; }
    }
    public class datadto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
  
}
