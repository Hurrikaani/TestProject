using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestiProjekti.Models
{
    public class OpiskelijaMalli
    {
        public int OpiskelijaID { get; set; }
        [Required(ErrorMessage = "Anna Koodi")]
        public string Koodi { get; set; }
        [Required(ErrorMessage = "Anna nimi")]
        public string Nimi { get; set; }
        [Required(ErrorMessage = "Anna ikä")]
        public Nullable<int> Ikä { get; set; }
        [Required(ErrorMessage = "Anna osoite")]
        public string Osoite { get; set; }
        [Required(ErrorMessage = "Anna maa")]
        public string Maa { get; set; }
        [Required(ErrorMessage = "Anna email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Anna postinumero")]
        public string Postinumero { get; set; }
    }
}