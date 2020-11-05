using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Histories
    {
        public int ID { get; set; }
        public string Car { get; set; }
        public string CarRegistration { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}