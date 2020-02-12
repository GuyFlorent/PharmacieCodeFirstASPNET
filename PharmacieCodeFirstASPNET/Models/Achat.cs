using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Achat
    {
        public int Id { get; set; }
        public virtual Stock Stock  { get; set; }
        
        public int Quantite_Totale { get; set; }
        public decimal Prix_Total { get; set; }
    }
}