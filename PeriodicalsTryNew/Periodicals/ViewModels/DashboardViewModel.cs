using System;
using System.ComponentModel.DataAnnotations;

namespace Periodicals.ViewModels
{
    public class DashboardViewModel
    {       
        [Display(Name = "Год")]
        [Range(2000, 2017, ErrorMessage = "Значение {0} должно быть между {1} и текущим годом")]
        public int StartYear { get; set; }

        public int Month { get; set; }
    }
}