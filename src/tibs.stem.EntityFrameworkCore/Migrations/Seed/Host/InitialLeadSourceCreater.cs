using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.LeadSources;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialLeadSourceCreater
    {

        private readonly stemDbContext _context;

        public InitialLeadSourceCreater(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.LeadSources.FirstOrDefault(p => p.LeadSourceName == "Existing");
            if (v1 == null)
            {
                _context.LeadSources.Add(
                    new LeadSource
                    {
                        LeadSourceName = "Existing",
                        LeadSourceCode = "EXT"
                    });
            }

            var v2 = _context.LeadSources.FirstOrDefault(p => p.LeadSourceName == "Marketing");
            if (v2 == null)
            {
                _context.LeadSources.Add(
                    new LeadSource
                    {
                        LeadSourceName = "Marketing",
                        LeadSourceCode = "MRK"
                    });
            }

            var v3 = _context.LeadSources.FirstOrDefault(p => p.LeadSourceName == "Referral");
            if (v3 == null)
            {
                _context.LeadSources.Add(
                    new LeadSource
                    {
                        LeadSourceName = "Referral",
                        LeadSourceCode = "REF"
                    });
            }
            var v4 = _context.LeadSources.FirstOrDefault(p => p.LeadSourceName == "Outbound");
            if (v4 == null)
            {
                _context.LeadSources.Add(
                    new LeadSource
                    {
                        LeadSourceName = "Outbound",
                        LeadSourceCode = "OUT"
                    });
            }
            var v5 = _context.LeadSources.FirstOrDefault(p => p.LeadSourceName == "Self Generated");
            if (v5 == null)
            {
                _context.LeadSources.Add(
                    new LeadSource
                    {
                        LeadSourceName = "Self Generated",
                        LeadSourceCode = "SLF"
                    });
            }
        }
    }
}
