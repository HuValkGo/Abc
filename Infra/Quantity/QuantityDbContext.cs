﻿using Abc.Data.Quantity;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantity
{
    public class QuantityDbContext:DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options)
            : base(options)
        {
        }
        public DbSet<MeasureData> Measures { get; set; }
        public DbSet<UnitData> Units { get; set; }
        public DbSet<MeasureData> SystemsOfUnits { get; set; }
        public DbSet<MeasureData> UnitFactors{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            InitializeTables(builder);

        }
        public static void InitializeTables(ModelBuilder builder)
        {
            builder.Entity<MeasureData>().ToTable(nameof(Measures));
            builder.Entity<UnitData>().ToTable(nameof(Units));
            builder.Entity<SystemOfUnitsData>().ToTable(nameof(SystemsOfUnits));
            builder.Entity<UnitFactorData>().ToTable(nameof(UnitFactors)).HasKey(x=>new{x.UnitId,x.SystemOfUnitsId});
        }
    }
}
