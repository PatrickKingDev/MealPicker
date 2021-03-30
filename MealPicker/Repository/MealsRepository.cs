using MealPicker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPicker.Repository
{
    public class MealsRepository
    {
        public MealsRepository() { }

        public string GetRandomMeal(IEnumerable<string> tags) {
            using (var dbContext = new MealPickerEntities())
            {
                var rand = new Random();

                var meals = dbContext.Meals.Where(m => !tags.Except(m.Tags.Select(t => t.Name)).Any()).ToList();

                if (meals.Count() == 0) {
                    return string.Empty;
                }

                return meals.ElementAt(rand.Next(0, meals.Count() - 1)).Name;
            }
        }

        public List<Tag> GetTags() {
            using (var dbContext = new MealPickerEntities()) {
                return dbContext.Tags.ToList();
            }
        }

        public void AddTag(int? MealID, int? TagID) {
            using (var dbContext = new MealPickerEntities())
            {
                var tag = dbContext.Tags.Find(TagID);
                var meal = dbContext.Meals.Find(MealID);

                meal.Tags.Add(tag);
                dbContext.SaveChanges();
            }
        }

        public void RemoveTag(int? MealID, int? TagID) {
            using (var dbContext = new MealPickerEntities())
            {
                var tag = dbContext.Tags.Find(TagID);
                var meal = dbContext.Meals.Find(MealID);

                meal.Tags.Remove(tag);
                dbContext.SaveChanges();
            }
        }
    }
}