using FIAP.IRRIGACAO.API.Models;
using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<FaucetModel> faucetList = new List<FaucetModel>();
            return View(faucetList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Console.WriteLine("Executou a Action Cadastrar()");
            return View(new FaucetModel());
        }

        [HttpPost]
        public IActionResult Create(FaucetModel clienteModel)
        {
            Console.WriteLine("Gravando a torneira");
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
