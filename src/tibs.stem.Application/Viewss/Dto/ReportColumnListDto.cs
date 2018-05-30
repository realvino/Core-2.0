using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Views;

namespace tibs.stem.Viewss.Dto
{
    [AutoMap(typeof(ReportColumn))]
    public class ReportColumnListDto
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual int Type { get; set; }
    }
}
