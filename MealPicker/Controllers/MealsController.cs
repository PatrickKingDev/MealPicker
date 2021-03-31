using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MealPicker.Models;
using MealPicker.Models.ViewModels;
using MealPicker.Repository;

namespace MealPicker.Controllers
{
    public class MealsController : Controller
    {
        private MealPickerEntities db = new MealPickerEntities();
        private MealsRepository repo = new MealsRepository();

        // GET: Meals
        public ActionResult Index()
        {
            return View(db.Meals.ToList().OrderBy(m => m.Name));
        }

        // GET: Meals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new MealDetailsVM();

            Meal meal = db.Meals.Find(id);

            if (meal == null)
            {
                return HttpNotFound();
            }

            vm.meal = meal;
            vm.tags = db.Tags.ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditTag(int? mealId, int? tagId, bool isChecked)
        {
            if (isChecked)
            {
                repo.AddTag(mealId, tagId);
            }
            else {
                repo.RemoveTag(mealId, tagId);
            }

            return RedirectToAction("Details", new { Id = mealId });
        }

        public ActionResult AddTag(int? mealId, int? tagId)
        {
            repo.AddTag(mealId, tagId);
            return RedirectToAction("Details", new { Id = mealId });
        }

        // GET: Meals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Meals.Add(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meal);
        }

        // GET: Meals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meal);
        }

        // GET: Meals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meal meal = db.Meals.Find(id);
            db.Meals.Remove(meal);
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
