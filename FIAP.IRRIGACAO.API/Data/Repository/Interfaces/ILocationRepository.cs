using FIAP.IRRIGACAO.API.Models;

namespace FIAP.IRRIGACAO.API.Data.Repository
{
    public interface ILocationRepository
    {
        IEnumerable<LocationModel> GetAll();
    }
}
