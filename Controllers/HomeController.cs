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

        private ApplicationContext db;
        //private ICtrlDataBase _ctrlDataBase;
        private List<string> _listCity = new List<string>() { "Polotsk", "Minsk", "Vitebsk", "Warszawa" };
        private List<string> _listCountry = new List<string>() { "Belarus", "Poland" };
        private List<string> _listStreet = new List<string>() { "Molodeznaya", "Parkowaya" };
        private List<string> _listHouseNumber = new List<string>() { "4", "5A" };

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            db = context;
            _logger = logger;
    
        }

        public IActionResult Index()
        {
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = db.Person.ToList();
            ViewBag.IsFilterClose = true;
            return View(modelPhoneItem);
        }

        public IActionResult GetFilter(PhoneItemFilter PhoneItemFilter)
        {
            IQueryable<PersonItem> _listPhone = db.Person;
            if (PhoneItemFilter.Name != null && PhoneItemFilter.Name.Length > 0) {
                _listPhone = _listPhone.Where(x => x.Name.Contains(PhoneItemFilter.Name));
            }
            if (PhoneItemFilter.Phone != null && PhoneItemFilter.Phone.Length > 0)
            {
                _listPhone = _listPhone.Where(x => x.Phone_number.Contains(PhoneItemFilter.Phone));
            }
            if (PhoneItemFilter.Country != null && PhoneItemFilter.Country.Length > 0)
            {
                _listPhone = _listPhone.Where(x => x.Country.Contains(PhoneItemFilter.Country));
            }
            if (PhoneItemFilter.City != null && PhoneItemFilter.City.Length > 0)
            {
                _listPhone = _listPhone.Where(x => x.City.Contains(PhoneItemFilter.City));
            }
            if (PhoneItemFilter.Street != null && PhoneItemFilter.Street.Length > 0)
            {
                _listPhone = _listPhone.Where(x => x.Street.Contains(PhoneItemFilter.Street));
            }
            if (PhoneItemFilter.House_number != null && PhoneItemFilter.House_number.Length > 0)
            {
                _listPhone = _listPhone.Where(x => x.Name.Contains(PhoneItemFilter.House_number));
            }

            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = _listPhone.ToList();
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
            //ViewBag.ListCity = _listCity;
            //ViewBag.ListCountry = _listCountry;
            //ViewBag.ListStreet = _listStreet;
            //ViewBag.ListHouseNumber = _listHouseNumber;
            return View();
        }
        public IActionResult EditPhone(int id)
        {
            var phone = db.Person.ToList().Find(el => el.Id == id);
            //ViewBag.ListCity = _listCity;
            //ViewBag.ListCountry = _listCountry;
            //ViewBag.ListStreet = _listStreet;
            //ViewBag.ListHouseNumber = _listHouseNumber;
            return View("AddElement", phone);
        }

        [HttpPost]
        public IActionResult AddElement(PersonItem phoneItem)
        {
            if (phoneItem.Id == 0)
            {
                db.Person.Add(phoneItem);
            }
            else
            {
                //_ctrlDataBase.Edit(phoneItem.Id, phoneItem);
                db.Person.Update(phoneItem);
            }
            db.SaveChanges();
            ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            modelPhoneItem.listPhone = db.Person.ToList();
            return View("Index", modelPhoneItem);
        }

        public string DeletePhone(int id)
        {
            //_ctrlDataBase.Delete(id);
            try
            {
                PersonItem item = new PersonItem()
                {
                    Id = id
                };
                db.Person.Attach(item);
                db.Person.Remove(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                _logger.LogError(ex.Message);
                return ex.Message;

            }
            //ModelPhoneItem modelPhoneItem = new ModelPhoneItem();
            //modelPhoneItem.listPhone = db.Person.ToList();
            //return View("Index", modelPhoneItem);
            return "ok";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
