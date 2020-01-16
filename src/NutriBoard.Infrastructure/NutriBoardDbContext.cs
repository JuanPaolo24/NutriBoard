using Microsoft.EntityFrameworkCore;
using NutriBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NutriBoard.Infrastructure
{
    public class NutriBoardDbContext : DbContext
    {
        public NutriBoardDbContext(DbContextOptions<NutriBoardDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<NutriMetrics> NutriMetrics { get; set; }
    }
}
