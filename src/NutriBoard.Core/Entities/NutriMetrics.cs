using System;
using System.Collections.Generic;
using System.Text;

namespace NutriBoard.Core.Entities
{
    public class NutriMetrics
    {
        public int MetricId { get; set; }
        public DateTime Date { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }
        public int Weight { get; set; }
        public int Steps { get; set; }
        public int Sleep { get; set; }
        public string Username { get; set; }
    }
}
