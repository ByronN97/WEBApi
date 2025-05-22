using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Services
{
    public class TokenServices : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
