using APIAjax.Models;
using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models.ViewModel;
using System.Diagnostics;

namespace APIAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext _context;

        public HomeController(ILogger<HomeController> logger,DemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult First()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(_context.Members);
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult Fetch()
        {
            return View();
        }
        public IActionResult Members()
        {
            return View(_context.Members);
        }
        public IActionResult History()
        {
            return View();
        }
        public IActionResult jQuery()
        {
            return View();  
        }
        public IActionResult Partial1()
        {
            return PartialView();
        }
        public IActionResult Partial2()
        {
            ViewBag.gay = "URGayFromPartial2";
            return PartialView();
        }
        public IActionResult checkAccount(MemberViewModel? member)
        {
            if (member != null) 
            {
                string txt = member.name;
                if (txt != null)
                {
                    string feedback = "123";
                    foreach(var item in _context.Members)
                    {
                        if(item.Name == txt)
                        {
                            feedback = "000";
                        }
                    }
                    return Content(feedback);
                }
            }
            return Content("000");
        }
    }
}