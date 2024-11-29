using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Service;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.IRRIGACAO.API.Controllers
{
    [ApiController]
    [Route("api/v1/faucet")]
    public class FaucetController : CrudController<FaucetViewModel, FaucetModel, FaucetRegisterViewModel>
    {
        public FaucetController(IFaucetService service) : base(service)
        {
        }

    }
}
