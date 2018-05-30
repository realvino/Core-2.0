using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.SourceTypes;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialSourceTypeCreater
    {
        private readonly stemDbContext _context;

        public InitialSourceTypeCreater(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.SourceTypes.FirstOrDefault(p => p.SourceTypeName == "Marketing");
            if (v1 == null)
            {
                _context.SourceTypes.Add(
                    new SourceType
                    {
                       SourceTypeName = "Marketing",
                       SourceTypeCode ="MR"
                    });
            }

            var v2 = _context.SourceTypes.FirstOrDefault(p => p.SourceTypeName == "Sales");
            if (v2 == null)
            {
                _context.SourceTypes.Add(
                    new SourceType
                    {
                        SourceTypeName = "Sales",
                        SourceTypeCode = "SL"
                    });
            }
           
        }
    }
}
