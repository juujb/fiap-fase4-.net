using AutoMapper;
using FIAP.IRRIGACAO.API.Models;
using FIAP.IRRIGACAO.API.ViewModels;

public class FaucetProfile : Profile
{
    public FaucetProfile()
    {
        CreateMap<FaucetViewModel, FaucetModel>()
            .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled ? "True" : "False"))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new LocationModel() { Id = src.Id, Name = src.LocationName! })).ReverseMap();

        CreateMap<FaucetModel, FaucetViewModel>()
            .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled == "True"))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location!.Name)).ReverseMap();

    }
}
