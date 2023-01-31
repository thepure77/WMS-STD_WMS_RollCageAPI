
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RollCageDataAccess.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class MasterDbContext : DbContext
    {
        public virtual DbSet<ms_Location> ms_Location { get; set; }
        public virtual DbSet<ms_RollCage> ms_RollCage { get; set; }
        public virtual DbSet<ms_RollCageType> ms_RollCageType { get; set; }
        public virtual DbSet<ms_BoxSize> ms_BoxSize { get; set; }
        public virtual DbSet<ms_BoxType> ms_BoxType { get; set; }
        public virtual DbSet<MS_ProductConversionBarcode> MS_ProductConversionBarcode { get; set; }

        public virtual DbSet<View_LocationRollCage> View_LocationRollCage { get; set; }
        public virtual DbSet<View_DockLocationRollCage> View_DockLocationRollCage { get; set; }
        public virtual DbSet<tmp_SuggestRollCageLocation> tmp_SuggestRollCageLocation { get; set; }
        public virtual DbSet<ms_Staging_location> ms_Staging_location { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("MasterConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

}
