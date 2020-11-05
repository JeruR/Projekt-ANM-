using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Cars
    {
        public int ID { get; set; }
        public string Car { get; set; }
        public string CarRegistration { get; set; }
        public string VIN { get; set; }
        public int ProductionYear { get; set; }
    }
}