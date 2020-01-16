using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NutriBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NutriBoard.Infrastructure.Repositories
{
    public class NutriMetricsRepository : INutriMetricsRepository
    {
        private readonly NutriBoardDbContext _context;

        public NutriMetricsRepository(NutriBoardDbContext context)
        {
            _context = context;
        }


        public void Add(NutriMetrics nutrimetrics)
        {
            if (nutrimetrics == null)
            {
                throw new ArgumentNullException(nameof(nutrimetrics));
            }

            _context.Add(nutrimetrics);
        }

        public void Delete(NutriMetrics nutrimetrics)
        {
            if (nutrimetrics == null)
            {
                throw new ArgumentNullException(nameof(nutrimetrics));
            }

            _context.Remove(nutrimetrics);
        }

        public async Task<IEnumerable<NutriMetrics>> GetAllMetricsAsync()
        {
            return await _context.NutriMetrics
                .OrderByDescending(x => x.MetricId)
                .ToListAsync();
                
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
