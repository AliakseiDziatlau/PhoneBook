using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationPhoneBook.Models
{
    public class PersonItem
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "The field 'Name' is necessary")]
        public string Name { get; set; }
        [DisplayName("Phone")]
        [Required(ErrorMessage = "The field 'Phone_number' is necessary")]
        public string Phone_number { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "The field 'Country' is necessary")]
        public string Country { get; set; }
        
        [DisplayName("City")]
        [Required(ErrorMessage = "The field 'Address' is necessary")]
        public string City { get; set; }

        [DisplayName("Street")]
        [Required(ErrorMessage = "The field 'Address' is necessary")]
        public string Street { get; set; }

        [DisplayName("House number")]
        [Required(ErrorMessage = "The field 'Address' is necessary")]
        public string House_number { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "The field 'Email' is necessary")]
        public string? Email { get; set; }
    }

    public class PhoneItemFilter {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Phone")]
        public string Phone { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Street")]
        public string Street { get; set; }
        [DisplayName("House number")]
        public string House_number { get; set; }
    }
    public class ModelPhoneItem { 
        public PersonItem PhoneItem { get; set; }
        public PhoneItemFilter PhoneItemFilter { get; set; }
        public List<PersonItem> listPhone { get; set;  }
    }
}
