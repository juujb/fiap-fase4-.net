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
            IEnumerable<FaucetModel> faucetList = [new() { Id = 1, Name = "Teste", IsEnabled = true, Location = new LocationModel() { Id = 1, Name = "Teste" } }];
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var locationList = GetLocations();

            var selectLocationList =
                new SelectList(locationList,
                                nameof(LocationModel.Id),
                                nameof(LocationModel.Name));

            ViewBag.LocationList = selectLocationList;
            var faucet = new FaucetModel() { Location = new LocationModel() { Id = id, Name = "Teste" } };

            return View(faucet);
        }

        [HttpPost]
        public IActionResult Edit(FaucetModel model)
        {
            TempData["mensagemSucesso"] = $"Os dados da torneira {model.Name} foram alterados com suceso";
            return RedirectToAction(nameof(Index));
        }

        private static List<LocationModel> GetLocations()
        {
            var locationList = new List<LocationModel>
            {
                new() { Id = 1, Name = "Praça" },
                new() { Id = 2, Name = "Escola" },
                new() { Id = 3, Name = "Prefeitura" }
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
