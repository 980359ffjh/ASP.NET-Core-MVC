using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RongProject1.Entities;
using RongProject1.Services;
using RongProject1.ViewModels;

namespace RongProject1.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            //var model = new Restaurant { Id = 1, Name = "Rong" };
            //var result = new ObjectResult(model);
            var model = new HomePageViewModel
            {
                Restaurants = _restaurantData.GetAll(),
                CurrentMessage = _greeter.GetGreeting()
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);

            // 如果 URL 輸入了沒有在 model 裡的資料，就會導向所設定的頁面
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);

            if(model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /*防止 CSRF(Cross-Site Request Forgery) 跨站偽造請求的攻擊*/
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);

            if(ModelState.IsValid)
            {
                restaurant.Cuisine = model.Cuisine;
                restaurant.Name = model.Name;
                _restaurantData.Commit();

                //根據 "???" 會對應到 ???.cshtml ，表示此 return 會回傳到 "???" 的 .cshtml
                //example : return "Details" -> Detail.cshtml
                return RedirectToAction("Details", new { id = restaurant.Id });
            }

            return View(restaurant);
        }

        //this create function is for view page used(to get )
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //and this create function will for get & post( to post )
        //so they will be have input var
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            //使用驗證
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant
                {
                    Cuisine = model.Cuisine,
                    Name = model.Name
                };
                newRestaurant = _restaurantData.Add(newRestaurant);
                _restaurantData.Commit();

                return RedirectToAction("Details", new { id = newRestaurant.Id });
            }

            return View();
        }
    }
}
