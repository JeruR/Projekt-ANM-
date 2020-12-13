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
                new Car{ CarName="Test",CarRegistration="909XX",VIN="232323",ProductionYear=1990 }
            };
            cars.ForEach(s => context.Cars.Add(s));
            context.SaveChanges();

            var current = new List<Current>
            {
                new Current{ Car="Test",CarRegistration="909XX",RentalDate=DateTime.Parse("2020-02-03"),ReturnDate=DateTime.Parse("2020-02-03") },
                new Current{ Car="Test",CarRegistration="909XX",RentalDate=DateTime.Parse("2020-02-05"),ReturnDate=DateTime.Parse("2020-02-08") }
            };
            current.ForEach(s => context.Current.Add(s));
            context.SaveChanges();

            var histories = new List<Histories>
            {
                new Histories{ Car="Test",CarRegistration="909XX",RentalDate=DateTime.Parse("2020-01-03"),ReturnDate=DateTime.Parse("2020-01-03") },
                new Histories{ Car="Test",CarRegistration="909XX",RentalDate=DateTime.Parse("2020-01-05"),ReturnDate=DateTime.Parse("2020-01-08") }
            };
            histories.ForEach(s => context.Histories.Add(s));
            context.SaveChanges();
        }
    }
}