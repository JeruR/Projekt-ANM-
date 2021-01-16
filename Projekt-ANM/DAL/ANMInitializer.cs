using Projekt_ANM.Controllers;
using Projekt_ANM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projekt_ANM.DAL
{
    public class ANMInitializer : DropCreateDatabaseAlways<ANMContext>
    {
        protected override void Seed(ANMContext context)
        {
            var cars = new List<Car>
            {
                new Car{ CarName="Volkswagen Golf VII",CarRegistration="PO12314",VIN="WVWZZZ16ZCM140910",ProductionYear=2011 },
                new Car{ CarName="Skoda Octavia",CarRegistration="PO22222",VIN="WVWZZZ16ZCM140911",ProductionYear=2018 },
                new Car{ CarName="Opel Corsa",CarRegistration="PO33333",VIN="WVWZZZ16ZCM140921",ProductionYear=2011 },
                new Car{ CarName="Opel Corsa",CarRegistration="PO44444",VIN="WVWZZZ16ZCM140922",ProductionYear=2012 },
                new Car{ CarName="Opel Corsa",CarRegistration="PO55555",VIN="WVWZZZ16ZCM140923",ProductionYear=2013 },
                new Car{ CarName="Opel Corsa",CarRegistration="PO66666",VIN="WVWZZZ16ZCM140924",ProductionYear=2014 },
                new Car{ CarName="Skoda Octavia",CarRegistration="PO77777",VIN="WVWZZZ16ZCM140925",ProductionYear=2015 },
                new Car{ CarName="Skoda Octavia",CarRegistration="PO88888",VIN="WVWZZZ16ZCM140926",ProductionYear=2016 },
                new Car{ CarName="Ford Fokus",CarRegistration="PO98977",VIN="WVWZZZ16ZCM140924",ProductionYear=2014 },
                new Car{ CarName="Skoda Octavia",CarRegistration="PO12122",VIN="WVWZZZ16ZCM140128",ProductionYear=2019 },
                new Car{ CarName="Skoda Fabia",CarRegistration="PO12348",VIN="WVWZZZ16ZCM140937",ProductionYear=2020 },
                new Car{ CarName="Skoda Fabia",CarRegistration="PO12348",VIN="WVWZZZ16ZCM140937",ProductionYear=2020 },
                new Car{ CarName="Ford Fokus",CarRegistration="PO45784",VIN="WVWZZZ16ZCM140912",ProductionYear=2011 },
                new Car{ CarName="Opel Astra",CarRegistration="PO12377",VIN="WVWZZZ16ZCM140916",ProductionYear=2015 }
            };
            cars.ForEach(s => context.Cars.Add(s));
            context.SaveChanges();

            var current = new List<Current>
            {
                new Current{ Car="Volkswagen Golf VII",CarRegistration="PO12314",RentalDate=DateTime.Parse("2021-02-03"),ReturnDate=DateTime.Parse("2021-02-03"),userid="Filip Ptak" },
                new Current{ Car="Opel Corsa",CarRegistration="PO33333",RentalDate=DateTime.Parse("2021-01-03"),ReturnDate=DateTime.Parse("2021-01-03"),userid="Paweł Kowalski"  },
                new Current{ Car="Opel Astra",CarRegistration="PO12377",RentalDate=DateTime.Parse("2021-01-05"),ReturnDate=DateTime.Parse("2021-01-08"),userid="Stefan Kwiat"  },
                new Current{ Car="Skoda Fabia",CarRegistration="PO12348",RentalDate=DateTime.Parse("2021-02-05"),ReturnDate=DateTime.Parse("2021-02-08"),userid="Paweł Czajka"  }
            };
            current.ForEach(s => context.Current.Add(s));
            context.SaveChanges();


            var users = new List<Users>();
            AccountController accountController = new AccountController();
            foreach (var item in accountController.GetUsers())
            {
                foreach (var item2 in item.Roles)
                {
                    users.Add(new Users { ID = item.Id, Login = item.Email, UserName = item.UserName, Roles = GetNameRole(item2.RoleId) });
                }
            }
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();


        }
        private string GetNameRole(string id)
        {
            if (id == "1")
            {
                return "Administrator";
            }
            else
            {
                return "Użytkownik";
            }
        }
      

    }
}