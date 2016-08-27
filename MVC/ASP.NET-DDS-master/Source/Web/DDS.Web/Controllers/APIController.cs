namespace DDS.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using DDS.Services.Data.Interfaces;

    public class APIController : BaseController
    {
        private readonly ITagsService tags;
        private readonly IStudentsService students;
        private readonly ITeachersService teachers;
        private readonly IDiplomasService diplomas;

        public APIController(
            ITagsService tags,
            IStudentsService students,
            ITeachersService teachers,
            IDiplomasService diplomas)
        {
            this.tags = tags;
            this.students = students;
            this.teachers = teachers;
            this.diplomas = diplomas;
        }

        public JsonResult GetAllItems(string type)
        {
            switch (type)
            {
                case "s":
                    return this.Json(new { Results = this.students.GetAll().ToList() }, JsonRequestBehavior.AllowGet);
                case "t":
                    return this.Json(new { Results = this.teachers.GetAll().ToList() }, JsonRequestBehavior.AllowGet);
                case "d":
                    return this.Json(new { Results = this.diplomas.GetAll().ToList() }, JsonRequestBehavior.AllowGet);
                default:
                    return this.Json(new { Results = this.tags.GetAll().ToList() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAllStudentsWithDiplomas(string type)
        {
            var students = this.students.GetAll()
                .Include(s => s.SelectedDiploma)
                .Where(s => s.SelectedDiploma == null)
                .ToList();
            return this.Json(new { Results = students }, JsonRequestBehavior.AllowGet);
        }
    }
}