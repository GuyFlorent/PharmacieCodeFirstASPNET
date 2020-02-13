using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string NomProduit_stock { get; set; }
        
        public int Quantite_Produit { get; set; }
        
    }
}