using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Models;
using FIAP.IRRIGACAO.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using X.PagedList.Extensions;

namespace FIAP.IRRIGACAO.API.Controllers
{
    public class FaucetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FaucetController> _logger;
        private readonly DatabaseContext _context;

        public FaucetController(
            IMapper mapper,
            ILogger<FaucetController> logger,
            DatabaseContext context
        )
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            try
            {
                int pageNumber = page ?? 1;
                int pageSize = 10;

                var faucetList = _context.Faucet.Include(f => f.Location).OrderByDescending(f => f.Id).ToPagedList(pageNumber, pageSize);
                return View(faucetList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar a lista de torneiras.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar os dados. Detalhes: {ex.Message}.";

                return View(new PagedList<FaucetModel>(new List<FaucetModel>() , 1, 10));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var locationList = _context.Location.ToList();

                var selectLocationList =
                    new SelectList(locationList,
                                    nameof(LocationModel.Id),
                                    nameof(LocationModel.Name));

                ViewBag.LocationList = selectLocationList;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar carregar a lista de localizações.");
                TempData["ErrorMessage"] = $"Houve um erro ao tentar carregar as localizações. Detalhes: {ex.Message}.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Create(FaucetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var faucet = _mapper.Map<FaucetModel>(model);
                    faucet.Location = _context.Location.Find(faucet.LocationId);
                    _context.Add(faucet);
                    _context.SaveChanges();
                    TempData["SucessMessage"] = $"A torneira {model.Name} foi cadastrada com suceso.";
                    return RedirectToAction(nameof(Index));
                }

                var locationList = _context.Location.ToList();

                var selectLocationList =
                    new SelectList(locationList,
                                    nameof(LocationModel.Id),
                                    nameof(LocationModel.Name),
                                    model.LocationId);

                ViewBag.LocationList = selectLocationList;

                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar criar torneira.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar criar torneira. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            try
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

                return View(_mapper.Map<FaucetViewModel>(faucet));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar a torneira para edição.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar a torneira para edição. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Edit(FaucetViewModel model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var faucet = _mapper.Map<FaucetModel>(model);
                    faucet.Location = _context.Location.Find(faucet.LocationId);
                    _context.Faucet.Update(faucet);
                    _context.SaveChanges();
                    TempData["SucessMessage"] = $"Os dados da torneira {model.Name} foram alterados com suceso.";
                    return RedirectToAction(nameof(Index));
                }

                var locationList = _context.Location.ToList();

                var selectLocationList =
                    new SelectList(locationList,
                                    nameof(LocationModel.Id),
                                    nameof(LocationModel.Name),
                                    model.LocationId);

                ViewBag.LocationList = selectLocationList;

                return View(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar editar torneira.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar editar torneira. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            try
            {
                var model = _context.Faucet.Find(id);

                if (model == null)
                    return NotFound();

                var faucet = _mapper.Map<FaucetViewModel>(model);

                faucet.LocationName = _context.Location.Find(faucet.LocationId)?.Name;

                return View(faucet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar detalhes da torneira.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar detalhes da torneira. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            try 
            { 
                var faucet = _context.Faucet.Find(id);

                if (faucet == null) 
                {
                    TempData["ErrorMessage"] = "Torneira inexistente.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Faucet.Remove(faucet);
                _context.SaveChanges();
                TempData["SucessMessage"] = $"Os dados da torneira {faucet.Name} foram removidos com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar remover torneira.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao tentar remover torneira. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
