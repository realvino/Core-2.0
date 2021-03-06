﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Ybafcos;

namespace tibs.stem.Ybafcoss.Dto
{
    [AutoMap(typeof(Ybafco))]
    public class CreateYbafcoInput
    {
        public virtual int Id { get; set; }
        public virtual string YbafcoCode { get; set; }
        public virtual string YbafcoName { get; set; }
    }
}
