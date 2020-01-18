using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NutriBoard.Web.ViewModels
{
    public class NutriMetricsViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }
        public int Weight { get; set; }
        public int Steps { get; set; }
        public int Sleep { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
