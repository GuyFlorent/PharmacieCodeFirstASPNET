using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class RechercheProduitViewModel
    {
        public string Recherche { get; set; }
       public  List<Stock> ListeProduits { get; set; }
    }
}