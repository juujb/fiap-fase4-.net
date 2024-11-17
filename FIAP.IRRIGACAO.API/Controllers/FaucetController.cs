using FIAP.IRRIGACAO.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FIAP.IRRIGACAO.API.Controllers
{
    public class FaucetController : Controller
    {
        private readonly ILogger<FaucetController> _logger;

        public FaucetController(ILogger<FaucetController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<FaucetModel> faucetList = [];
            return View(faucetList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var locationList = GetLocations();

            var selectLocationList =
                new SelectList(locationList,
                                nameof(LocationModel.Id),
                                nameof(LocationModel.Name));

            ViewBag.LocationList = selectLocationList;

            FaucetModel faucet = new() { Location = new LocationModel() { Name = "Teste" } };
            return View(faucet);
        }

        [HttpPost]
        public IActionResult Create(FaucetModel model)
        {
            Console.WriteLine("Gravando a torneira");
            TempData["mensagemSucesso"] = $"A Torneira de Id {model.Id} foi cadastrada com suceso";
            return RedirectToAction(nameof(Index));
        }

        public static List<LocationModel> GetLocations()
        {
            var locationList = new List<LocationModel>
            {
                new LocationModel { Id = 1, Name = "Praça" },
                new LocationModel { Id = 2, Name = "Escola" },
                new LocationModel { Id = 3, Name = "Prefeitura" }
            };

            return locationList;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
