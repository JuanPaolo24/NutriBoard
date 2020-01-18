using NutriBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriBoard.Web.ViewModels
{
    public static class NutriMetricsMapper
    {

        public static NutriMetricsViewModel ToNutriMetricsViewModel(this NutriMetrics nutriMetrics)
        {
            return new NutriMetricsViewModel
            {
                Date = nutriMetrics.Date,
                Protein = nutriMetrics.Protein,
                Carbohydrates = nutriMetrics.Carbohydrates,
                Fats = nutriMetrics.Fats,
                Calories = nutriMetrics.Calories,
                Weight = nutriMetrics.Weight,
                Steps = nutriMetrics.Steps,
                Sleep = nutriMetrics.Sleep,
                Username = nutriMetrics.Username
            };
        }

        public static NutriMetrics ToNutriMetricsModel(this NutriMetricsViewModel nutriMetrics)
        {
            return new NutriMetrics
            {
                Date = nutriMetrics.Date,
                Protein = nutriMetrics.Protein,
                Carbohydrates = nutriMetrics.Carbohydrates,
                Fats = nutriMetrics.Fats,
                Calories = nutriMetrics.Calories,
                Weight = nutriMetrics.Weight,
                Steps = nutriMetrics.Steps,
                Sleep = nutriMetrics.Sleep,
                Username = nutriMetrics.Username
            };
        }

    }
}
