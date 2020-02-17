using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Models
{
    public class ClientPassword
    {
        public int id { get; set; }
        [Required]
        [Remote("VerifPassword", "Client",ErrorMessage ="Le Mot de passe n'est pas identique à l'ancien")]
        [Display(Name ="Ancien Mot de passe")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Nouveau Mot de passe")]
        public string NewPass { get; set; }
    }
}