namespace DDS.Web.Controllers
{
    using System.Web.Mvc;

    public class StudentController : BaseController
    {
        // GET: Student
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
