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
                .OrderByDescending(x => x.Id)
                .ToListAsync();
                
        }

        //This is set to IEnumerable because there might be a case where there are two entries on that day
        public async Task<IEnumerable<NutriMetrics>> GetNutriMetricsByDateAsync(string date)
        {
            return await _context.NutriMetrics
                 .Where(d => d.Date == date)
                 .ToListAsync();
        }

        public async Task<NutriMetrics> GetNutriMetricsById(int id)
        {
            return await _context.NutriMetrics
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
