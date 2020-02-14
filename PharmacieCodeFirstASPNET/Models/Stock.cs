using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Stock
    {
        public int Id { get; set; }
        [Display(Name ="Nom du produit")]
        public string NomProduit_stock { get; set; }
        [Display(Name = "Image du produit")]
        public string image_stok { get; set; }
        [Display(Name = "Prix à l'unité")]
        public decimal Prix_unite_stock { get; set; }
        [Display(Name = "Quantité restante")]
        public int Quantite_Produit { get; set; }
        
    }
}