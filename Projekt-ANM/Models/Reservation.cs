using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Reservation
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Data wypożyczenia")]
        public DateTime Date1 { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Data zwrotu")]
        public DateTime Date2 { get; set; }

    }
}