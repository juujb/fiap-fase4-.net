using FIAP.IRRIGACAO.API.Data.Context;
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
        private readonly DatabaseContext _context;

        public FaucetController(
            ILogger<FaucetController> logger,
            DatabaseContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var faucetList = _context.Faucet.Include(f => f.Location).ToList();
            return View(faucetList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var locationList = _context.Location.ToList();

            var selectLocationList =
                new SelectList(locationList,
                                nameof(LocationModel.Id),
                                nameof(LocationModel.Name));

            ViewBag.LocationList = selectLocationList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(FaucetModel model)
        {
            _context.Faucet.Add(model);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"A torneira {model.Name} foi cadastrada com suceso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var faucet = _context.Faucet.Find(id);

            if (faucet == null)
                return NotFound();

            var locationList = _context.Location.ToList();

            var selectLocationList =
                new SelectList(locationList,
                                nameof(LocationModel.Id),
                                nameof(LocationModel.Name),
                                faucet.LocationId);
            
            ViewBag.LocationList = selectLocationList;

            return View(faucet);
        }

        [HttpPost]
        public IActionResult Edit(FaucetModel model)
        {
            _context.Faucet.Update(model);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"Os dados da torneira {model.Name} foram alterados com suceso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var faucet = _context.Faucet.Find(id);
            if (faucet != null)
            {
                _context.Faucet.Remove(faucet);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados da torneira {faucet.Name} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "Torneira inexistente.";
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
