using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.EntityFrameworkCore;
using tibs.stem.Products;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialProductStateCreator
    {
        private readonly stemDbContext _context;

        public InitialProductStateCreator(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.ProductStates.FirstOrDefault(p => p.Name == "Low");
            if (v1 == null)
            {
                _context.ProductStates.Add(
                    new ProductStates
                    {
                        Name = "Low"
                    });
            }

            var v2 = _context.ProductStates.FirstOrDefault(p => p.Name == "Medium");
            if (v2 == null)
            {
                _context.ProductStates.Add(
                    new ProductStates
                    {
                        Name = "Medium"
                    });
            }
            var v3 = _context.ProductStates.FirstOrDefault(p => p.Name == "High");
            if (v3 == null)
            {
                _context.ProductStates.Add(
                    new ProductStates
                    {
                        Name = "High"
                    });
            }

        }
    }
}
