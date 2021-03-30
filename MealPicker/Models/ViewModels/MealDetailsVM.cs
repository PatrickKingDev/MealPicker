using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPicker.Models.ViewModels
{
    public class MealDetailsVM
    {
        public Meal meal { get; set; }
        public List<Tag> tags { get; set; }
    }
}