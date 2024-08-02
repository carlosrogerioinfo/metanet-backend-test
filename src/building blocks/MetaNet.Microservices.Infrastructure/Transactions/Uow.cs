using MetaNet.Microservices.Infrastructure.Contexts;

namespace MetaNet.Microservices.Infrastructure.Transactions
{
    public class Uow : IUow
    {
        private readonly MetaNetDataContext _context;

        public Uow(MetaNetDataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do Nothing
        }
    }
}
