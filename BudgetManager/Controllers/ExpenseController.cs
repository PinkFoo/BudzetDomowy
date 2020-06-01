using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetManager.DAL;
using BudgetManager.Models;
using PagedList;
using Newtonsoft.Json;



namespace BudgetManager.Controllers
{
    public class ExpenseController : Controller
    {
        private BudgetContext db = new BudgetContext();

        // GET: Expense
        public ActionResult Index(string sortOrder,  int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

              var expenses = db.Expenses.Include(e => e.Budget).Include(e => e.Category).Include(e => e.User);

            switch (sortOrder)
            {
                case "name_desc":
                    expenses = expenses.OrderByDescending(s => s.Category.NazwaKat);
                    break;
                case "Date":
                    expenses = expenses.OrderBy(s => s.DateExp);
                    break;
                case "date_desc":
                    expenses = expenses.OrderByDescending(s => s.DateExp);
                    break;
                default:  
                    expenses = expenses.OrderBy(s => s.Category.NazwaKat);
                    break;
            }

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(expenses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Expense/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expense/Create
        public ActionResult Create()
        {
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "NameBud");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "NazwaKat");
            ViewBag.UserID = new SelectList(db.Users, "ID", "NazwaUzyt");
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,BudgetID,CategoryID,Desription,Amount,DateExp")] Expense expense)
        {
            Budget budzet = db.Budgets.Where(x => x.BudgetID == expense.BudgetID).SingleOrDefault();
          
            decimal zasob = budzet.EmountBud;
            var list = db.Expenses;
            decimal s = expense.Amount;

            bool czyMozna = list.Where(x => x.BudgetID == budzet.BudgetID).Select(o => o.Amount).Any();
            var sum1 = list.Where(x => x.BudgetID == budzet.BudgetID);
            /**/
            decimal sumka = 0;
            if (czyMozna == true)
            {
                sumka = sum1.Sum(o => o.Amount);
            }

            decimal sumka1 = s + sumka;



            DateTime data = expense.DateExp;
            if (ModelState.IsValid && (sumka1 < zasob ) && data <= DateTime.Today && expense.Amount > 0)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if(sumka1>zasob)
            {
                ViewBag.ErrorMax = "Po dodaniu tego wydatku łączna kwota wydatków w tym budżecie przekroczy dostępny zasób!";
                ViewBag.ErrorMax2 = "Aby dodać tę kwotę należy zwiększyć kwotę dostępną w wybranym budżecie kierując się do zakładki ,,Zarządzaj budżetami\".";
            }
            if(data>DateTime.Today)
            {
                ViewBag.ErrorData = "Możliwe jest tylko dodanie wydatku wykonanego w przeszłości.";
            }
            if (expense.Amount <= 0)
            {
                ViewBag.errorUjemny = "Kwota musi być większa niż 0.";
            }

            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "NameBud", expense.BudgetID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "NazwaKat", expense.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "NazwaUzyt", expense.UserID);
            return View(expense);
        }

       // Wykresy --------------------------------------

            public ActionResult ChartUser()
        {
            var list = db.Expenses;
            List<decimal> repartitions = new List<decimal>();
            var ages = list.Select(x => x.User.NazwaUzyt).Distinct();

            foreach (var item in ages)
            {
                repartitions.Add(list.Where(x => x.User.NazwaUzyt == item).Sum(z => z.Amount));

            }

            var rep = repartitions;
            ViewBag.AGES = ages;
            ViewBag.REP = repartitions.ToList();

            return View(); 
        }


       public ActionResult Dashboard()
        {
            var list = db.Expenses;
            List<decimal> repartitions = new List<decimal>();
            var ages = list.Select(x => x.Category.NazwaKat).Distinct();

            foreach (var item in ages)
            {
                repartitions.Add(list.Where(x => x.Category.NazwaKat == item).Sum(z => z.Amount));
        
            }

            var rep = repartitions;
            ViewBag.AGES = ages;
            ViewBag.REP = repartitions.ToList();

            return View();
        }





        // GET: Expense/Edit/5
        public ActionResult Edit(int? id)
        {
            Expense expense = db.Expenses.Find(id);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "NameBud", expense.BudgetID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "NazwaKat", expense.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "NazwaUzyt", expense.UserID);
            return View(expense);
        }

        // POST: Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseID,UserID,BudgetID,CategoryID,Desription,Amount,DateExp")] Expense expense)
        {

            Budget budzet = db.Budgets.Where(x => x.BudgetID == expense.BudgetID).SingleOrDefault();

            decimal zasob = budzet.EmountBud;
            var list = db.Expenses;
            decimal s = expense.Amount;

            bool czyMozna = list.Where(x => x.BudgetID == budzet.BudgetID).Select(o => o.Amount).Any();
            var sum1 = list.Where(x => x.BudgetID == budzet.BudgetID);
            /**/
            decimal sumka = 0;
            if (czyMozna == true)
            {
                sumka = sum1.Sum(o => o.Amount);
            }

            decimal sumka1 = s + sumka;


            DateTime data = expense.DateExp;

            if (ModelState.IsValid && sumka1 < zasob && data <= DateTime.Today && expense.Amount > 0)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (sumka1 > zasob)
            {
                ViewBag.ErrorMax = "Po dodaniu tego wydatku łączna kwota wydatków w tym budżecie przekroczy dostępny zasób!";
                ViewBag.ErrorMax2 = "Aby dodać tę kwotę należy zwiększyć kwotę dostępną w wybranym budżecie kierując się do zakładki ,,Zarządzaj budżetami\".";
            }
            if (data > DateTime.Today)
            {
                ViewBag.ErrorData = "Możliwe jest tylko dodanie wydatku wykonanego w przeszłości.";
            }
            if (expense.Amount <= 0)
            {
                ViewBag.errorUjemny = "Kwota musi być większa niż 0.";
            }
            ViewBag.BudgetID = new SelectList(db.Budgets, "BudgetID", "NameBud", expense.BudgetID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "NazwaKat", expense.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "NazwaUzyt", expense.UserID);
            return View(expense);
        }

        // GET: Expense/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
