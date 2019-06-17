using Microsoft.AspNetCore.Mvc;
using RongProject1.Services;
using System.Threading.Tasks;

namespace RongProject1.ViewComponents
{
    public class GreetingViewComponent : ViewComponent
    {
        private IGreeter _greeter;

        public GreetingViewComponent(IGreeter greeter)
        {
            _greeter = greeter;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            var model = _greeter.GetGreeting();

            //return to Default.cshtml
            return Task.FromResult<IViewComponentResult>(View("Default", model));
        }
    }
}
