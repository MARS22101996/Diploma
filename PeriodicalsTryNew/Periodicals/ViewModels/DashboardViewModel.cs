using System;
using System.ComponentModel.DataAnnotations;

namespace Periodicals.ViewModels
{
    public class DashboardViewModel
    {
        [Required]
        public string User { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
    }
}