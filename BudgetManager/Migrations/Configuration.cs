namespace BudgetManager.Migrations
{ 
using BudgetManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetManager.DAL.BudgetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BudgetManager.DAL.BudgetContext context)
        {
            var users = new List<User>
            {
                new User{NazwaUzyt = "Jacek"},
                 new User{NazwaUzyt = "Kasia"},
                  new User{NazwaUzyt = "Basia"}
            };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.NazwaUzyt, s));
            context.SaveChanges();

            var budgets = new List<Budget>
            {
                new Budget{EmountBud = 1000, DateBud = DateTime.Parse("2005-09-01"), NameBud = "Wyjazd"},
                new Budget{EmountBud = 1000, DateBud = DateTime.Parse("2005-04-01"), NameBud = "Przyjazd"},
                  new Budget{EmountBud = 450, DateBud = DateTime.Parse("2020-05-01"), NameBud = "Pyrkon 2020"}



            };
            budgets.ForEach(s => context.Budgets.AddOrUpdate(p => p.NameBud, s));
            context.SaveChanges();


            var categories = new List<Category>
            {
               
                new Category{NazwaKat= "Podróże"},
                new Category{NazwaKat= "Czynsz"},
               new Category{NazwaKat= "Energia"},
               new Category{NazwaKat= "Rozrywka"},
               new Category{NazwaKat= "Rata"},
               new Category{NazwaKat= "Jedzenie"},
               new Category{NazwaKat= "Inne"}



            };


            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.NazwaKat, s));
            context.SaveChanges();


            var expenses = new List<Expense>
            {
                new Expense{UserID = users.Single(s => s.NazwaUzyt == "Jacek").ID, BudgetID = budgets.Single(c => c.NameBud == "Wyjazd").BudgetID, CategoryID = categories.Single(f => f.NazwaKat =="Rozrywka").CategoryID, Amount = 60, DateExp = DateTime.Parse("2005-04-01"), Desription = "Samolot"  },
                new Expense{UserID = users.Single(s => s.NazwaUzyt == "Kasia").ID, BudgetID = budgets.Single(c => c.NameBud == "Przyjazd").BudgetID, CategoryID = categories.Single(f => f.NazwaKat == "Podróże").CategoryID, Amount = 60, DateExp = DateTime.Parse("2005-05-01"), Desription = "Samochód" },
               new Expense{UserID = users.Single(s => s.NazwaUzyt == "Kasia").ID, BudgetID = budgets.Single(c => c.NameBud == "Pyrkon 2020").BudgetID, CategoryID = categories.Single(f => f.NazwaKat == "Rozrywka").CategoryID, Amount = 120, DateExp = DateTime.Parse("2020-01-05"), Desription = "Strój" }



            };
            foreach (Expense e in expenses)
            {
                var expenseInDataBase = context.Expenses.Where(
                    s =>
                         s.User.ID == e.UserID &&
                         s.Category.CategoryID == e.CategoryID &&
                         s.Budget.BudgetID == e.BudgetID 
                          ).SingleOrDefault();
                if (expenseInDataBase == null)
                {
                    context.Expenses.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
