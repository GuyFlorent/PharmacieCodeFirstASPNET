using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class ProduitAchatViewModel : IValidatableObject
    {
        public List<ProduitSelectViewModel> ListeDesProduit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ListeDesProduit.Any(r => r.EstSelectionne))
                yield return new ValidationResult("Vous devez choisir au moins un produit pour finaliser l'achat", new[] { "ListeDesProduit" });
        }
    }
}