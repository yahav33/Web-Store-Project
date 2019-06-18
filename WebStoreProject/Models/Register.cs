using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Services;

namespace WebStoreProject.Models
{
    public class Register
    {
        //need to add custom validation to the user check if the name is available    
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Remote("CheckBirthDate", "Register", HttpMethod = "POST", ErrorMessage = "age must be begger than 15 years")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required]
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Remote("CheckUser", "Register", HttpMethod = "POST", ErrorMessage = "Username is Already Exist")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ComfirmPassword { get; set; }

    }
}
