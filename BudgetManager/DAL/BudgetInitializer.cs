using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BudgetManager.Models;


namespace BudgetManager.DAL
{
    public class BudgetInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BudgetContext>
    {
        protected override void Seed(BudgetContext context)
        {
            var users = new List<User>
            {
                new User{NazwaUzyt = "Jacek"},
                 new User{NazwaUzyt = "Kasia"},
                  new User{NazwaUzyt = "Basia"}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var budgets = new List<Budget>
            {
                new Budget{EmountBud = 1000, DateBud = DateTime.Parse("2005-09-01"), NameBud = "Wyjazd"},
                new Budget{EmountBud = 1000, DateBud = DateTime.Parse("2005-04-01"), NameBud = "Przyjazd"},

            };
            budgets.ForEach(k => context.Budgets.Add(k));
            context.SaveChanges();

            var categories = new List<Category>
            {
new Category{NazwaKat = "Alkohol"},
new Category{NazwaKat = "Podróże"},
new Category{NazwaKat = "Czynsz"}



            };
            categories.ForEach(o => context.Categories.Add(o));
            context.SaveChanges();

            var expenses = new List<Expense>
            {
                new Expense{UserID = 1, BudgetID = 1, Amount = 60, DateExp = DateTime.Parse("2005-04-01"), Desription = "Samolot", CategoryID = 1 },
                new Expense{UserID = 2, BudgetID = 2, Amount = 60, DateExp = DateTime.Parse("2005-03-01"), Desription = "Samochód", CategoryID = 2 }

            };
            expenses.ForEach(o => context.Expenses.Add(o));
            context.SaveChanges();
        }
    }
}