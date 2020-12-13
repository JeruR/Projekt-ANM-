using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Car
    {
        public int ID { get; set; }
        [DisplayName("Auto")]
        public string CarName { get; set; }
        [DisplayName("Rejestracja")]
        public string CarRegistration { get; set; }
        [DisplayName("Numer VIN")]
        public string VIN { get; set; }
        [DisplayName("Rok produkcji")]
        public int ProductionYear { get; set; }
    }
}