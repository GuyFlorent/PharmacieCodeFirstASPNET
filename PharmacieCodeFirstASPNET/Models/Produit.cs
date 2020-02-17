using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Produit
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nom du produit")]
       // [Remote("VerifProduitExist","Produit", ErrorMessage = "Ce Nom de produit existe déja! Le stock sera incrémeté")]
        public string NomProduit { get; set; }
        [Required]
        [Display(Name = "Prix à l'unité")]
        public decimal Prix_Unite { get; set; }
        [Required]
        [Display(Name ="Quantité")]
        public int Quantite { get; set; }
        [Required]
        [Display(Name = "Date d'ajout")]
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$", ErrorMessage = "Le format de la date est incorect")]
        public string Date_heure_ajout { get; set; }
        public virtual Stock Stock { get; set; }
        public string Image { get; set; }

    }
}