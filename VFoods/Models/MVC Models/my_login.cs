using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VFoods.Models.MVC_Models
{
    public class my_login
    {
        
        public int Cred_ID { get; set; }
        [Required(ErrorMessage = "User name is required ! ! !")]
        [Display(Name = "User Name : ")]
        public string Cred_Username { get; set; }

        [Required(ErrorMessage = "Password is required ! ! !")]
        [Display(Name = "Password : ")]
        public string Cred_Password { get; set; }
    }
}