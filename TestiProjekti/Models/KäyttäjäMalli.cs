using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestiProjekti.Models
{
    public class KäyttäjäMalli
    {

        public int KäyttäjäID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Etunimi on pakollinen")]
        [Display(Name = "Etunimi")]
        public string Etunimi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sukunimi on pakollinen")]
        [Display(Name = "Sukunimi")]
        public string Sukunimi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email on pakollinen")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Salasana on pakollinen")]
        [Display(Name = "Salasana")]
        [DataType(DataType.Password)]
        public string Salasana { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Salasanan vahvistaminen on pakollinen")]
        [DataType(DataType.Password)]
        [Compare("Salasana", ErrorMessage = "Salasanan tulee täsmätä.")]
        [Display(Name = "Toista salasana")]
        public string VahvistaSalasana { get; set; }
        public System.DateTime Luotu { get; set; }
        public string HyväksyttyViesti { get; set; }
    }
}