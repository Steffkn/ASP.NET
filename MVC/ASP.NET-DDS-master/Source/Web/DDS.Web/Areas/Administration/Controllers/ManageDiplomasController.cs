namespace DDS.Web.Areas.Administration.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using PagedList;
    using Services.Data.Interfaces;
    using Web.Controllers;
    using Web.ViewModels.Shared;
    using Web.ViewModels.ManageDiplomas;
    public class ManageDiplomasController : BaseController
    {
        private readonly ITeachersService teachers;
        private readonly IDiplomasService diplomas;
        private readonly IStudentsService students;
        private readonly ITagsService tags;

        public ManageDiplomasController(
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

        // GET: ManageDiplomas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            this.ViewBag.CurrentSort = sortOrder;
            this.ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            this.ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            this.ViewBag.CurrentFilter = searchString;

            //var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId()).FirstOrDefault();
            var diplomas = this.diplomas.GetAll()
                                            .To<CommonDiplomaViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                if (searchString.ToLower() == "удобрени")
                {
                    diplomas = diplomas.Where(d => d.IsApprovedByLeader || d.IsApprovedByHead);
                }
                else if (searchString.ToLower() == "избрани")
                {
                    diplomas = diplomas.Where(d => d.IsSelectedByStudent);
                }
                else if (searchString.ToLower() == "свободни")
                {
                    diplomas = diplomas.Where(d => !d.IsSelectedByStudent);
                }
                else
                {
                    diplomas = diplomas.Where(s => s.Title.Contains(searchString) || s.Description.Contains(searchString));
                }
            }

            if (diplomas.LongCount() <= 0)
            {
                this.TempData["Message"] = "Не са намерени дипломи!";
            }
            else
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        diplomas = diplomas.OrderByDescending(s => s.Title);
                        break;

                    case "Date":
                        diplomas = diplomas.OrderBy(s => s.CreatedOn);
                        break;

                    case "date_desc":
                        diplomas = diplomas.OrderByDescending(s => s.CreatedOn);
                        break;

                    default:
                        // Title ascending
                        diplomas = diplomas.OrderBy(s => s.Title);
                        break;
                }
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;
            return this.View(diplomas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetObjectById(intId);
            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            var result = new StudentDiplomaViewModel();

            var studentDetails = this.students.GetAll()
                                              .Where(s => s.SelectedDiploma.Id == diploma.Id)
                                              .Include(s => s.User)
                                              .To<SimpleStudentViewModel>()
                                              .FirstOrDefault();
            result.Student = studentDetails;

            var diplomaModel = this.diplomas.GetAll()
                                             .Where(d => d.Id == intId)
                                             .Include(d => d.Teacher)
                                             .Include(d => d.Tags)
                                             .To<DisplayDiplomaViewModel>();

            result.Diploma = diplomaModel.FirstOrDefault();
            result.Diploma.ContentCSV = diploma.ContentCSV.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            result.Diploma.TeacherID = diploma.TeacherID;
            result.Diploma.Tags = diploma.Tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Disabled = true,
                Selected = true,
                Value = t.Id.ToString()
            });

            var teacher = this.teachers.GetById(diploma.TeacherID).Include(t => t.User).FirstOrDefault();
            result.Diploma.TeacherName = string.Format("{0} {1} {2}", teacher.User.ScienceDegree, teacher.User.FirstName, teacher.User.LastName).Trim();

            return this.View(result);
        }
    }
}
