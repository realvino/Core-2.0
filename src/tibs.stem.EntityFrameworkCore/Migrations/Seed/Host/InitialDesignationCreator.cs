using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Designations;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialDesignationCreator
    {
        private readonly stemDbContext _context;

        public InitialDesignationCreator(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.Designations.FirstOrDefault(p => p.DesiginationName == "Manager");
            if (v1 == null)
            {
                _context.Designations.Add(
                    new Designation
                    {
                        DesiginationName = "Manager",
                        DesignationCode = "Man"
                    });
            }

            var v2 = _context.Designations.FirstOrDefault(p => p.DesiginationName == "Sales");
            if (v2 == null)
            {
                _context.Designations.Add(
                    new Designation
                    {
                        DesiginationName = "Sales",
                        DesignationCode = "Sal"
                    });
            }
            var v3 = _context.Designations.FirstOrDefault(p => p.DesiginationName == "Business Development");
            if (v2 == null)
            {
                _context.Designations.Add(
                    new Designation
                    {
                        DesiginationName = "Business Development",
                        DesignationCode = "BD"
                    });
            }
        }
    }
}
