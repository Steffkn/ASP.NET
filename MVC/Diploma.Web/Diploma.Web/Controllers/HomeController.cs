namespace Diploma.Web.Controllers
{
    using System.Web.Mvc;
    using static Diploma.Web.AutofacConfig;

    public class HomeController : Controller
    {
        private IService service;

        public HomeController(IService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            this.service.Work();
            return View();
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