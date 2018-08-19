using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactApplication.Models
{
    public class ContactViewModel
    {
        public int ID { get; set; }

        [Display( Name ="First Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

  
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}