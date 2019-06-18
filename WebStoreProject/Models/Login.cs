using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Models
{
    public class Login
    {
        [Required]
        [Display(Name ="User name")]
        public string Username { get; set; }
        [Required]
        [Remote("LoginChecker", "Login", HttpMethod = "POST", ErrorMessage = "Wrong Info")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
