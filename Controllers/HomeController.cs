using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationPhoneBook.Models;
using WebApplicationPhoneBook.services;

namespace WebApplicationPhoneBook.Controllers
{

    public class HomeController : Controller
    {
        //private CtrlDataBase ctrlDataBase = new CtrlDataBase();
        private readonly ILogger<HomeController> _logger;

        private ICtrlDataBase _ctrlDataBase;
        private List<string> _listCity = new List<string>() { "Polotsk", "Minsk", "Vitebsk", "Warszawa" };

        public HomeController(ILogger<HomeController> logger, ICtrlDataBase ctrlDataBase)
        {
            _ctrlDataBase = ctrlDataBase;
            _logger = logger;
    
        }


        public IActionResult Index()
        {
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = _ctrlDataBase.GetList();
            ViewBag.IsFilterClose = true;
            return View(modelPhoneItem);
        }

        public IActionResult GetFilter(PhoneItemFilter PhoneItemFilter)
        {
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = _ctrlDataBase.GetList(PhoneItemFilter);
            ViewBag.IsFilterClose = false;
            return View("Index", modelPhoneItem);
        }

        //public IActionResult GetFilter(string Name, string Phone, string Adress )
        //{
        //    ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
        //    modelPhoneItem.listPhone = _ctrlDataBase.GetFullList(Name, Phone, Adress);
        //    return View("Index", modelPhoneItem);
        //}



        public IActionResult AddElement()
        {
            ViewBag.ListCity = _listCity;
            return View();
        }
        public IActionResult EditPhone(int id)
        {
            var phone = _ctrlDataBase.GetList().Find(el => el.ID == id);
            ViewBag.ListCity = _listCity;
            return View("AddElement", phone);
        }

        [HttpPost]
        public IActionResult AddElement(PhoneItem phoneItem)
        {
            if (phoneItem.ID == 0)
            {
                _ctrlDataBase.Add(phoneItem);
            }
            else
            {
                _ctrlDataBase.Edit(phoneItem.ID, phoneItem);
            }
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = _ctrlDataBase.GetList();
            return View("Index", modelPhoneItem);
        }

        public IActionResult DeletePhone(int id)
        {
            _ctrlDataBase.Delete(id);
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = _ctrlDataBase.GetList();
            return View("Index", modelPhoneItem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
