using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BudgetManager.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Nazwa użytkownika może mieć do 20 znaków.")]
        [Display(Name = "Nazwa Użytkownika")]
        public string NazwaUzyt { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }


    }
}