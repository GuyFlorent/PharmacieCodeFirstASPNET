using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class ProduitSelectViewModel
    {
        public int id { get; set; }
        public string  Image { get; set; }
        public string NomEtPrix_Unité { get; set; }
        public int Quantite_Stock_Restante { get; set; }
        public bool EstSelectionne { get; set; }

    }
}