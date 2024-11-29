using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.ViewModel;

namespace FIAP.IRRIGACAO.API.Profile
{
    public class LocationProfile : AutoMapper.Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationViewModel, LocationModel>().ReverseMap();
            CreateMap<LocationRegisterViewModel, LocationModel>();
        }
    }
}
