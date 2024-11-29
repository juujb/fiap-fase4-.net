using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Model;

namespace FIAP.IRRIGACAO.API.Repository
{
    public class LocationRepository : GenericRepository<LocationModel>, ILocationRepository
    {
        public LocationRepository(OracleContext context) : base(context)
        {

        }

    }
}
