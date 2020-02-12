using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string NomProduit { get; set; }
        public decimal Prix_Unite { get; set; }
        public int Quantite { get; set; }
        public string Date_heure_ajout { get; set; }
        public virtual Stock Stock { get; set; }

    }
}