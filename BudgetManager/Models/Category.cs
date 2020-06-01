using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [StringLength(50)]
        [Display(Name = "Kategoria")]
        public string NazwaKat { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

    }
}