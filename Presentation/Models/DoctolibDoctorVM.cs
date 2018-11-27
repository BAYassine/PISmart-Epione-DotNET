using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Models
{
    public class DoctolibDoctorVM : DoctolibVM
    {
        public string description { get; set; }
        public string nbreRPPS { get; set; }
        public string statuts { get; set; }
        public string nbreInscriptionOrdre { get; set; }
        public string nbreRCS { get; set; }
        public bool membreAGA { get; set; }
        public string formeJuridique { get; set; }
        public string adresseSocialeSiege { get; set; }
        public string socialReason { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public List<string> skills { get; set; }
    }
}