using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class ProduitPanierViewModel
    {
        
        public Stock stock { get; set; }
        public int Quantite { get; set; }
    }
}