using AutoMapper;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Repository;
using FIAP.IRRIGACAO.API.ViewModel;

namespace FIAP.IRRIGACAO.API.Service
{
    public class LocationService : GenericService<LocationViewModel, LocationModel, LocationRegisterViewModel>, ILocationService
    {
        public LocationService(
            ILocationRepository repository,
            IMapper mapper) : base(repository, mapper)
        {

        }

    }
}
