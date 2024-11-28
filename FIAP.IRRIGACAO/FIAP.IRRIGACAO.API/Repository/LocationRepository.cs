using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Model;

namespace FIAP.IRRIGACAO.API.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly OracleContext _context;

        public LocationRepository(OracleContext context)
        {
            _context = context;
        }

        public IEnumerable<LocationModel> GetAll()
        {
            return _context.Location.ToList();
        }

    }
}
