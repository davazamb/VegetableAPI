using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace VegetableAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {     
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public DbSet<Vegetable> Vegetables { get; set; }
    }
}