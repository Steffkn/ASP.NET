namespace DDS.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PagedList;
    using Services.Data.Interfaces;
    using ViewModels.ManageDiplomas;
    using ViewModels.Shared;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class ManageDiplomasController : BaseController
    {
        private readonly ITeachersService teachers;
        private readonly IDiplomasService diplomas;
        private readonly IStudentsService students;
        private readonly ITagsService tags;
        private ApplicationUserManager userManager;

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

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
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

            var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId()).FirstOrDefault();
            var diplomas = this.diplomas.GetByTeacherId(teacher.Id)
                                            .To<CommonDiplomaViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomas = diplomas.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            if (diplomas.LongCount() <= 0)
            {
                this.TempData["NotFound"] = true;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int Id)
        {
            var diploma = this.diplomas.GetObjectById(Id);
            diploma.IsApprovedByLeader = true;

            this.diplomas.Save();

            return this.RedirectToAction("Details", "ManageDiplomas", new { @id = Id });
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDiplomaViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId()).FirstOrDefault();

                // create diploma
                var diploma = new Diploma()
                {
                    Teacher = teacher,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ExperimentalPart = viewModel.ExperimentalPart,
                    ContentCSV = string.Join(",", viewModel.ContentCSV),
                };

                var listOfTags = new List<Tag>();
                foreach (var viewModelTag in viewModel.TagsNames)
                {
                    var tagId = 0;
                    Tag tag;
                    if (int.TryParse(viewModelTag, out tagId))
                    {
                        tag = this.tags.GetObjectById(tagId);
                    }
                    else
                    {
                        tag = this.tags.EnsureCategory(viewModelTag);
                    }

                    listOfTags.Add(tag);
                }

                diploma.Tags = listOfTags;

                // add diploma to teacher
                this.teachers.AddDiploma(teacher.Id, diploma);
                this.TempData["Message"] = "Дипломата е създадена!";

                return this.RedirectToAction("Index", "ManageDiplomas");
            }
            else
            {
                this.TempData["Message"] = "Дипломата е не пълна!";
            }

            return this.View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                this.TempData["Message"] = "Дипломата не е намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetObjectById(intId);

            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            var viewModel = new EditDiplomaViewModel()
            {
                Id = diploma.Id,
                Title = diploma.Title,
                Description = diploma.Description,
                ExperimentalPart = diploma.ExperimentalPart,
                ContentCSV = diploma.ContentCSV
                                    .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                                    .ToList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditDiplomaViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var dDiploma = this.diplomas.GetObjectById(viewModel.Id);
                dDiploma.Title = viewModel.Title;
                dDiploma.Description = viewModel.Description;
                dDiploma.ExperimentalPart = viewModel.ExperimentalPart;
                dDiploma.ContentCSV = string.Join(",", viewModel.ContentCSV);

                this.diplomas.Save();

                this.TempData["Message"] = "Дипломата е променена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            return this.View(viewModel);
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

        public ActionResult Selected(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            var result = new StudentDiplomaViewModel();
            var student = new SimpleStudentViewModel();

            int intId = id ?? 0;
            var diploma = this.diplomas.GetObjectById(intId);
            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            var teacher = this.teachers.GetById(diploma.TeacherID).Include(t => t.User);

            // var diplomaModel = new DisplayDiplomaViewModel()
            // {
            //     Id = diploma.Id,
            //     Title = diploma.Title,
            //     Description = diploma.Description,
            //     ExperimentalPart = diploma.ExperimentalPart,
            //     ContentCSV = diploma.ContentCSV
            //                         .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
            //                         .ToList(),
            //     CreatedOn = diploma.CreatedOn.ToString(),
            //     ModifiedOn = diploma.ModifiedOn.ToString(),
            //     DeletedOn = diploma.DeletedOn.ToString(),
            //     IsDeleted = diploma.IsDeleted,
            //     ApprovedByLeader = diploma.IsApprovedByLeader,
            //     ApprovedByHead = diploma.IsApprovedByHead,
            //     IsSelectedByStudent = diploma.IsSelectedByStudent,
            //     TeacherID = diploma.TeacherID,
            //     TeacherName = string.Format("{0} {1} {2}", teacher.User.ScienceDegree, teacher.User.FirstName, teacher.User.LastName).Trim()
            // };
            var diplomaModel = this.diplomas.GetAll()
                 .Where(d => d.Id == intId)
                 .Include(d => d.Teacher)
                 .Include(d => d.Tags)
                 .To<DisplayDiplomaViewModel>();

            var studentDetails = this.students.GetAll()
                 .Where(s => s.SelectedDiploma.Id == diploma.Id)
                 .Include(s => s.User)
                 .To<SimpleStudentViewModel>()
                 .FirstOrDefault();

            result.Diploma = diplomaModel.FirstOrDefault();

            result.Diploma.ContentCSV = diploma.ContentCSV
                                    .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                                    .ToList();

            result.Diploma.Tags = diploma.Tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Disabled = true,
                Selected = true,
                Value = t.Id.ToString()
            });

            result.Student = studentDetails;
            return this.View(result);
        }

        // GET: Deleted
        public ActionResult Deleted(string sortOrder, string currentFilter, string searchString, int? page)
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

            var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId()).FirstOrDefault();
            var diplomas = this.diplomas.GetDeleted()
                                        .Where(x => x.Teacher.Id == teacher.Id)
                                        .To<CommonDiplomaViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomas = diplomas.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            if (diplomas.LongCount() <= 0)
            {
                this.TempData["Message"] = "Няма намерени дипломи!";
                this.TempData["NotFound"] = true;
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

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
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

            this.diplomas.Delete(diploma);
            this.TempData["Message"] = "Дипломата е изтрита!";
            return this.RedirectToAction("Index", "ManageDiplomas");
        }

        // Get: HardDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HardDelete(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Deleted");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetDeleted().FirstOrDefault(d => d.Id == intId);

            if (diploma == null)
            {
                this.TempData["NotFound"] = true;
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Deleted");
            }

            this.diplomas.HardDelete(diploma);
            return this.RedirectToAction("Deleted");
        }

        public ActionResult HardDeleteAll()
        {
            var teacherID = this.teachers.GetByUserId(this.User.Identity.GetUserId()).FirstOrDefault().Id;
            var diplomas = this.diplomas.GetDeleted().Where(d => d.TeacherID == teacherID).ToList();

            foreach (var diploma in diplomas)
            {
                this.diplomas.HardDelete(diploma);
            }

            return this.RedirectToAction("Index");
        }

        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetDeleted().FirstOrDefault(d => d.Id == id);

            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            this.diplomas.UnDelete(diploma);
            this.TempData["Message"] = "Дипломата е върната!";
            return this.RedirectToAction("Index", "ManageDiplomas");
        }
    }
}
