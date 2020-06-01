using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models
{

  
    public class Expense
    {
        public int ExpenseID { get; set; }
        public int UserID { get; set; }
        public int BudgetID { get; set; }

        public int CategoryID { get; set; }

        [StringLength(100, ErrorMessage = "Opis może mieć do 100 znaków.")]
        public string Desription { get; set; }
        [DataType(DataType.Currency, ErrorMessage = "O nie" )]
        [Column(TypeName = "money")]
        [Display(Name = "Kwota")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Wydatku")]
        public DateTime DateExp { get; set; }

        public virtual User User { get; set; }

        public virtual Budget Budget { get; set; }

        public virtual Category Category { get; set; }
    }
}