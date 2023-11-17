using APIAjax.Models;
using Microsoft.AspNetCore.Mvc;
using MSIT153Site.Models.ViewModel;

namespace APIAjax.Controllers
{
    public class ApiController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DemoContext _context;
        public ApiController(IWebHostEnvironment host,DemoContext context)
        {
            _host = host;
            _context = context;
        }
        public IActionResult Index(string name, int age = 30)
        {
            System.Threading.Thread.Sleep(5000);
            if (string.IsNullOrEmpty(name))
            {
                name = "guest";
            }
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            return Content($"Hello {name}， You are {age} years old.");
        }
        public IActionResult register(Members member,IFormFile formFile)
        {
            string strPath = Path.Combine(_host.WebRootPath, "uploads", formFile.FileName);
            using (var fileStream=new FileStream(strPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            member.FileName=formFile.FileName;
            byte[]?imgByte = null;
            using(var memoryStream =new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileData = imgByte;
             _context.Members.Add(member);
            _context.SaveChanges();
            return Content("新增成功");
            //return Content("<h2>Ajax 你好 !!</h2>","text/html", System.Text.Encoding.UTF8);
            //return Content($"Hello {member.name}，{member.email},  You are {member.age} years old.");
        }
        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            return Json(cities);
        }
        public IActionResult  districts(string city)
        {
            var districts=_context.Address.Where(a=>a.City==city).Select(a=>a.SiteId).Distinct();
            return Json(districts);

        }
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(a => a.Road).Distinct();
            return Json(roads);
        }
        public IActionResult GetImageByte(int id = 1)
        {
            Members member = _context.Members.Find(id);
            byte[] img = member.FileData;
            if(img != null)
            {
                return File(img, "image/jpeg");
            }
            return NotFound();
        }
    }
}
