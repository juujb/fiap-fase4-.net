using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Service;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.IRRIGACAO.API.Controllers
{
    [ApiController]
    [Route("api/v1/location")]
    public class LocationController : CrudController<LocationViewModel, LocationModel, LocationRegisterViewModel>
    {
        public LocationController(ILocationService service) : base(service)
        {
        }

    }
}
