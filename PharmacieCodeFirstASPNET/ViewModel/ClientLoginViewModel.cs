using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class ClientLoginViewModel
    {
        public ClientLogin client { get; set; }
        public bool Authentifie { get; set; }
    }
}