using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.CustomerTypes;
using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialCustomerTypeCreater
    {
        private readonly stemDbContext _context;

        public InitialCustomerTypeCreater(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var v1 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Customer");
            if (v1 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Customer",
                        Code = "CUS"
                    });
            }

            var v2 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Competitor");
            if (v2 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Competitor",
                        Code = "CMT"
                    });
            }
            var v3 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Consultant");
            if (v3 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Consultant",
                        Code = "CONS"
                    });
            }
            var v4 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Contractor");
            if (v4 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Contractor",
                        Code = "CONT"
                    });
            }
            var v5 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Dealer");
            if (v5 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Dealer",
                        Code = "DEA"
                    });
            }
            var v6 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Distributor");
            if (v6 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Distributor",
                        Code = "DIS"
                    });
            }
            var v7 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "End User");
            if (v7 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "End User",
                        Code = "ENU"
                    });
            }
            var v8 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Known Contact");
            if (v8 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Known Contact",
                        Code = "KNC"
                    });
            }
            var v9 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "System Integrator");
            if (v9 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "System Integrator",
                        Code = "SYI"
                    });
            }
            var v10 = _context.CustomerTypes.FirstOrDefault(p => p.CustomerTypeName == "Competitor");
            if (v10 == null)
            {
                _context.CustomerTypes.Add(
                    new CustomerType
                    {
                        CustomerTypeName = "Manufacturer",
                        Code = "MAN"
                    });
            }
        }
    }
}
