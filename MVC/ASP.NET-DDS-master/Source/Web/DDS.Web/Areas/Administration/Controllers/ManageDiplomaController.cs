namespace DDS.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Interfaces;
    using Web.Controllers;
    using Web.ViewModels.Shared;

    public class ManageDiplomaController : BaseController
    {
        private readonly ITeachersService teachers;
        private readonly IDiplomasService diplomas;
        private readonly IStudentsService students;
        private readonly ITagsService tags;

        public ManageDiplomaController(
                  ITeachersService teachers,
                  IDiplomasService diplomas,
                  IStudentsService students,
                  ITagsService tags)
        {
            this.teachers = teachers;
            this.diplomas = diplomas;
            this.students = students;
            this.tags = tags;
        }

        public ActionResult Diplomas()
        {
            var teachers = this.diplomas.GetAll().Include(t => t.Tags).To<TeacherViewModel>();

            return this.View(teachers);
        }
    }
}
