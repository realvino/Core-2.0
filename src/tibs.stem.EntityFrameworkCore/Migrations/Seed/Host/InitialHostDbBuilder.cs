using tibs.stem.EntityFrameworkCore;

namespace tibs.stem.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly stemDbContext _context;

        public InitialHostDbBuilder(stemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new InitialCustomerTypeCreater(_context).Create();
            new InitialDesignationCreator(_context).Create();
            new InitialTitleCreator(_context).Create();
            new InitialSourceTypeCreater(_context).Create();
            new InitialLeadSourceCreater(_context).Create();
            new DefaultStagestateCreator(_context).Create();
            new InitialProductStateCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
