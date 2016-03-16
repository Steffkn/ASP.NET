namespace Diploma.Web.Controllers
{
    using System.Web.Mvc;
    using static Diploma.Web.AutofacConfig;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}