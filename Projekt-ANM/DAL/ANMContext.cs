using Projekt_ANM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Projekt_ANM.DAL
{
    public class ANMContext: DbContext
    {
        public ANMContext() : base("ANMContext")
        {
        }
        public DbSet<Current> Current { get; set; }
        public DbSet<Histories> Histories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}