using FIAP.IRRIGACAO.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FIAP.IRRIGACAO.API.Controllers
{
    public class FaucetController : Controller
    {
        private readonly ILogger<FaucetController> _logger;
        private readonly IEnumerable<FaucetModel> _faucetList;

        public FaucetController(ILogger<FaucetController> logger)
        {
            _logger = logger;
            _faucetList = [new() { Id = 1, Name = "Teste", IsEnabled = true, Location = new LocationModel() { Id = 1, Name = "Praça" } }];
        }

        public IActionResult Index()
        {
            return View(_faucetList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var locationList = GetLocations();

            ViewBag.LocationList = locationList
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList();

            return View();
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
            var faucet = _faucetList.First(faucet => faucet.Id == id);

            return View(faucet);
        }

        [HttpPost]
        public IActionResult Edit(FaucetModel model)
        {
            TempData["mensagemSucesso"] = $"Os dados da torneira {model.Name} foram alterados com suceso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var faucet = _faucetList.First(f => f.Id == id);
            if (faucet != null)
                TempData["mensagemSucesso"] = $"Os dados da Torneira {faucet.Name} foram removidos com sucesso";
            else
                TempData["mensagemSucesso"] = $"Torneira inexistente.";

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
