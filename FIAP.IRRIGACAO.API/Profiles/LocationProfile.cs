using AutoMapper;
using FIAP.IRRIGACAO.API.Models;
using FIAP.IRRIGACAO.API.ViewModels;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationViewModel, LocationModel>().ReverseMap();
    }
}
