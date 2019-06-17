using RongProject1.Entities;
using System.ComponentModel.DataAnnotations;

namespace RongProject1.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
