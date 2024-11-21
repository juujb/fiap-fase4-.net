using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Models;

namespace FIAP.IRRIGACAO.API.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DatabaseContext _context;

        public LocationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<LocationModel> GetAll()
        {
            return _context.Location.ToList();
        }

    }
}
