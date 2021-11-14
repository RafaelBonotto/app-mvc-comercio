using Microsoft.AspNetCore.Mvc;

namespace Comercio.Controllers
{    
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }   
    }
}
