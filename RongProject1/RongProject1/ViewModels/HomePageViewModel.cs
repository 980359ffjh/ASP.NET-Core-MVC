using RongProject1.Entities;
using System.Collections.Generic;

namespace RongProject1.ViewModels
{
    public class HomePageViewModel
    {
        public string CurrentMessage { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
