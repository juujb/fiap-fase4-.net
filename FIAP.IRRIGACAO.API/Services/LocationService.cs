using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Repository;
using FIAP.IRRIGACAO.API.ViewModels;

namespace FIAP.IRRIGACAO.API.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public LocationService(
            ILocationRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<LocationViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<LocationViewModel>>(_repository.GetAll());
        }

    }
}
