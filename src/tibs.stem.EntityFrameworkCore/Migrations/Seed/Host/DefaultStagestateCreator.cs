using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.Stagestates;

namespace tibs.stem.Migrations.Seed.Host
{     
    public class DefaultStagestateCreator
    {
        private readonly stemDbContext _context;

        public DefaultStagestateCreator(stemDbContext context)
        {
            _context = context;  
        }

        public void Create()
        {
            var v1 = _context.Stagestates.FirstOrDefault(p => p.Name == "Won");
            if (v1 == null)
            {
                _context.Stagestates.Add(
                    new Stagestate
                    {
                        Name = "Won"
                    });
            }

            var v2 = _context.Stagestates.FirstOrDefault(p => p.Name == "Lost");
            if (v2 == null)
            {
                _context.Stagestates.Add(
                    new Stagestate
                    {
                        Name = "Lost"
                    });
            }
            var v3 = _context.Stagestates.FirstOrDefault(p => p.Name == "Junk");
            if (v3 == null)
            {
                _context.Stagestates.Add(
                    new Stagestate
                    {
                        Name = "Junk"
                    });
            }
            
        }
    }
}
