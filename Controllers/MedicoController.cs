using Microsoft.AspNetCore.Mvc;

namespace ApiCentroMedico.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
