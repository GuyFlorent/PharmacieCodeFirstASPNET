using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Client
    {
        public int Id { get; set; }
      //  [Required(ErrorMessage = "Le Nom ne doit pas être vide")]
        
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "L'Email ne doit pas être vide")]
      // [Remote("verifEmail","Login",ErrorMessage ="email existe deja !!!!")]
        public string Email { get; set; }
     //  [System.ComponentModel.DataAnnotations.Compare("Email",ErrorMessage ="Email n'est pas identique")]
        [Display(Name = "Confiramation Email")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage ="Le Mot de passe ne doit pas être vide")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Le format de la date est incorect")]
        [Display(Name = "Date de naissance")]
        public string Date_Naissance { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
    }
}