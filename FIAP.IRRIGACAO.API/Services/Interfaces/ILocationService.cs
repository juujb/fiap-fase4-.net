using FIAP.IRRIGACAO.API.ViewModels;

namespace FIAP.IRRIGACAO.API.Data.Repository
{
    public interface ILocationService
    {
        IEnumerable<LocationViewModel> GetAll();
    }
}
