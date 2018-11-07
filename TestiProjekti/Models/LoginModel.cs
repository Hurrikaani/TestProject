using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestiProjekti.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email on pakollinen")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Salasana on pakollinen")]
        [DataType(DataType.Password)]
        public string Salasana { get; set; }
    }
}