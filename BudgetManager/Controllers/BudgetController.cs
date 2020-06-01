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

namespace BudgetManager.Controllers
{
    public class BudgetController : Controller
    {
        private BudgetContext db = new BudgetContext();

        // GET: Budget
        public ActionResult Index()
        {
            return View(db.Budgets.ToList());
        }

        // GET: Budget/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);

            var list = db.Expenses;
            decimal calyBudzet = budget.EmountBud;
            bool czyMozna = list.Where(x => x.BudgetID == budget.BudgetID).Select(o => o.Amount).Any();
            var sumaWyd = list.Where(x => x.BudgetID == budget.BudgetID);
            decimal sumSumaWyd = 0;
          
            if (czyMozna == true)
            {
                sumSumaWyd = sumaWyd.Sum(o => o.Amount);
            }
            decimal roznica = calyBudzet - sumSumaWyd;
            string wynSuma = String.Format("{0:0.00}", sumSumaWyd);
            string wynRozn = String.Format("{0:0.00}", roznica);
            ViewBag.Sumka = wynSuma;
            ViewBag.Roznica = wynRozn;

            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budget/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Budget/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmountBud,DateBud,NameBud")] Budget budget)
        {
          
            if (ModelState.IsValid && budget.EmountBud > 0)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
      
                return RedirectToAction("Index");
            }
            if(budget.EmountBud <= 0)
            {
                ViewBag.errorUjemny = "Kwota musi być większa niż 0.";
            }

            return View(budget);
        }

        // GET: Budget/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budget/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetID,EmountBud,DateBud,NameBud")] Budget budget)
        {
            if (ModelState.IsValid && budget.EmountBud > 0)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (budget.EmountBud <= 0)
            {
                ViewBag.errorUjemny = "Kwota musi być większa niż 0.";
            }
            return View(budget);
        }

        // GET: Budget/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
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
