using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationPhoneBook.Models;

namespace WebApplicationPhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private static List<PhoneItem> listPhone = new List<PhoneItem>() {
                new PhoneItem{
                    ID=1,
                    Name = "Goga",
                    Phone = "123432",
                    Address = "34534"
                },
                new PhoneItem{
                    ID=2,
                    Name = "Aerg",
                    Phone = "123432",
                    Address = "34534"
                },
                new PhoneItem{
                    ID=3,
                    Name = "Gihohoih",
                    Phone = "1789789789",
                    Address = "3jlkjljlk"
                }
            };
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            

            return View(listPhone);
        }

        public IActionResult AddElement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddElement(PhoneItem phoneItem)
        {
            phoneItem.ID = listPhone.Count() + 1;
            listPhone.Add(phoneItem);
            return View("Index", listPhone);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
