using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Data.Repository;
using FIAP.IRRIGACAO.API.Models;
using FIAP.IRRIGACAO.API.Services;
using FIAP.IRRIGACAO.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IFaucetService _service;
        private readonly ILocationService _locationService;

        public FaucetController(
            IMapper mapper,
            ILogger<FaucetController> logger,
            IFaucetService service,
            ILocationService locationService
        )
        {
            _mapper = mapper;
            _logger = logger;
            _service = service;
            _locationService = locationService;
        }

        public IActionResult Index(int? page)
        {
            try
            {
                var faucetList = _service.GetAllPaged(page);
                return View(faucetList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar a lista de torneiras.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar os dados. Detalhes: {ex.Message}.";

                return View(new PagedList<FaucetViewModel>([] , 1, 10));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            try
            {
                var locationList = _locationService.GetAll();

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
        [Authorize]
        public IActionResult Create(FaucetViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Create(model);
                    TempData["SucessMessage"] = $"A torneira {model.Name} foi cadastrada com suceso.";
                    return RedirectToAction(nameof(Index));
                }

                var locationList = _locationService.GetAll();

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
        [Authorize]
        public IActionResult Edit(long id)
        {
            try
            {
                var faucet = _service.FindById(id);

                if (faucet == null)
                    return NotFound();

                var locationList = _locationService.GetAll();

                var selectLocationList =
                    new SelectList(locationList,
                                    nameof(LocationModel.Id),
                                    nameof(LocationModel.Name),
                                    faucet.LocationId);

                ViewBag.LocationList = selectLocationList;

                return View(faucet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar a torneira para edição.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar a torneira para edição. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(FaucetViewModel model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    _service.Update(model);
                    TempData["SucessMessage"] = $"Os dados da torneira {model.Name} foram alterados com suceso.";
                    return RedirectToAction(nameof(Index));
                }

                var locationList = _locationService.GetAll();

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
        [Authorize]
        public IActionResult Details(long id)
        {
            try
            {
                var model = _service.FindById(id);

                if (model == null)
                    return NotFound();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao carregar detalhes da torneira.");
                TempData["ErrorMessage"] = $"Ocorreu um erro ao carregar detalhes da torneira. Detalhes: {ex.Message}.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(long id)
        {
            try 
            { 
                var faucet = _service.Delete(id);

                if (faucet == null)
                    return NotFound();

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
