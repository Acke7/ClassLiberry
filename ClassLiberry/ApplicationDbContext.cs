using Microsoft.EntityFrameworkCore;
using MyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShapeData> shapeDatas { get; set; }
        public DbSet<Rpc> rpcGames { get; set; }
        public DbSet<CalculationData> Calculations { get; set; }
        public ApplicationDbContext()
        {
            // en tom konstruktor behövs för att skapa migrations
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=ClassLibrary;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }
    }
}
