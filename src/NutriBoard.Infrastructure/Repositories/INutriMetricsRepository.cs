using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NutriBoard.Core.Entities;

namespace NutriBoard.Infrastructure.Repositories
{
    public interface INutriMetricsRepository
    {
        void Add(NutriMetrics nutrimetrics);

        void Delete(NutriMetrics nutrimetrics);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<NutriMetrics>> GetAllMetricsAsync();

       
    }
}
