using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt_ANM.Models
{
    public class Users
    {
        public string ID { get; set; }
        [DisplayName("Email")]
        public string Login { get; set; }
        [DisplayName("Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Imię")]
        public string Name { get; set; } 
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }  
        [DisplayName("Imię i Nazwisko")]
        public string UserName { get; set; } 
        [DisplayName("Rola")]
        public string Roles { get; set; }

       

    }
}