using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPhoneBook.Models
{
    public class PhoneItem
    {
        public int ID { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "The field 'Name' is necessary")]
        public string Name { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "The field 'Phone' is necessary")]
        public string Phone { get; set; }
        [DisplayName("Address")]
        [Required(ErrorMessage = "The field 'Address' is necessary")]
        public string Address { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "The field 'Email' is necessary")]
        public string? Email { get; set; }
    }

    public class PhoneItemFilter { 
        public string name { get; set; }
    }
    public class ModelPhoneItem { 
        public PhoneItem PhoneItem { get; set; }
        public PhoneItemFilter PhoneItemFilter { get; set; }
        public List<PhoneItem> listPhone { get; set;  }
    }
}
