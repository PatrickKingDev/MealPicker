using MealPicker.Models.ViewModels;
using MealPicker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MealPicker.Controllers
{
    public class HomeController : Controller
    {
        private MealsRepository repo = new MealsRepository();

        public ActionResult Index()
        {
            var vm = new MealsVM();
            vm.Tags = repo.GetTags();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string selected = Request.Form["chkMeal"].ToString();
            string[] selectedList = selected.Split(',');

            var vm = new MealsVM();
            vm.Tags = repo.GetTags();
            vm.chosenMeal = repo.GetRandomMeal(selectedList);

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}