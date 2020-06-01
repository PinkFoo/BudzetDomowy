using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace BudgetManager.Models
{
    public class Budget
    {
        public int BudgetID { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Budżet")]
        [Column(TypeName = "money")]
        public decimal EmountBud { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [Display(Name = "Miesiąc")]
        public DateTime DateBud { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Tytuł musi mieć mniej niż 50 znaków.")]
        [Display(Name = "Tytuł Budżetu")]
        public string NameBud { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }



    }
}