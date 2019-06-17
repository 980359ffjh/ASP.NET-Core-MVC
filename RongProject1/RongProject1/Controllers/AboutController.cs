using Microsoft.AspNetCore.Mvc;

namespace RongProject1.Controllers
{
    [Route("detail/[controller]/[action]")]
    public class AboutController
    {
        public string Index()
        {
            return "Rong";
        }
        
        public string Phone()
        {
            return "0988594241";
        }
        
        public string Address()
        {
            return "Taiwan";
        }
    }
}
