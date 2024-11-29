using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FIAP.IRRIGACAO.API.Repository
{
    public class FaucetRepository : GenericRepository<FaucetModel>, IFaucetRepository
    {
        private readonly OracleContext _context;

        public FaucetRepository(OracleContext context) : base(context)
        {
            _context = context;
        }

        public override List<FaucetModel> GetAll()
        {
            return _context.Faucet
                .Include(f => f.Location)
                .ToList();
        }

        public override List<FaucetModel> GetAllPaged(int pageNumber, int pageSize)
        {
            return _context.Faucet
                .Include(f => f.Location)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public override FaucetModel? FindById(long id)
        {
            return _context.Faucet
                .Include(f => f.Location)
                .FirstOrDefault(f => f.Id == id);
        }
    }

}
