using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Car
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Auto")]
        public string CarName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej następującą liczbę znaków: {2}.", MinimumLength = 3),MaxLength(9)]
        [DisplayName("Rejestracja")]
        public string CarRegistration { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej następującą liczbę znaków: {2}.", MinimumLength = 17),MaxLength(17)]
        [DisplayName("Numer VIN")]
        public string VIN { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Rok produkcji")]
        public int ProductionYear { get; set; }
    }
}