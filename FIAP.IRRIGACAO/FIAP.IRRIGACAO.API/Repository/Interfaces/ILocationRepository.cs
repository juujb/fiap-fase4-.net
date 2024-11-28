using FIAP.IRRIGACAO.API.Model;

namespace FIAP.IRRIGACAO.API.Repository
{
    public interface ILocationRepository
    {
        IEnumerable<LocationModel> GetAll();
    }
}
