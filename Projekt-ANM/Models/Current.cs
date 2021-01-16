using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Current
    {

        public int ID { get; set; }
        [DisplayName("Samochód")]
        public string Car { get; set; }
        [DisplayName("Rejestracja")]
        public string CarRegistration { get; set; }
        [DisplayName("Data wypożyczenia")]
        public DateTime RentalDate { get; set; }
        [DisplayName("Data zwrotu")]
        public DateTime ReturnDate { get; set; }   
        [DisplayName("Osoba")]
        public string userid { get; set; }
    }
}