using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPicker.Models.ViewModels
{
    public class MealsVM
    {
        public List<Tag> Tags { get; set; }
        public string chosenMeal { get; set; }
    }
}