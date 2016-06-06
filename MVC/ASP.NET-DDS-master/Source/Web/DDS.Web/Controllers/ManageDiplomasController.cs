﻿namespace DDS.Web.Controllers
{
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
        private ApplicationUserManager userManager;

        public ManageDiplomasController(
            ITeachersService teachers,
            IDiplomasService diplomas)
        {
            this.teachers = teachers;
            this.diplomas = diplomas;
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

            var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId());
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
                var teacherID = this.teachers.GetByUserId(this.User.Identity.GetUserId());

                // create diploma
                var diploma = new Diploma()
                {
                    Teacher = teacherID,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ExperimentalPart = viewModel.ExperimentalPart,
                    ContentCSV = string.Join(",", viewModel.ContentCSV),
                };

                // add diploma to teacher
                this.teachers.AddDiploma(teacherID.Id, diploma);
                this.TempData["Message"] = "Дипломата е създадена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            return this.View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetById(intId);

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
                var diploma = new Diploma()
                {
                    Id = viewModel.Id,
                    Teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId()),
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ExperimentalPart = viewModel.ExperimentalPart,
                    ContentCSV = string.Join(",", viewModel.ContentCSV),
                };

                this.diplomas.Edit(diploma);

                this.TempData["Message"] = "Дипломата бе променена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            return this.View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetById(intId);

            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            this.diplomas.Delete(diploma);
            this.TempData["Message"] = "Дипломата е изтрита!";
            return this.RedirectToAction("Index", "ManageDiplomas");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetById(intId);
            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "ManageDiplomas");
            }

            var user = this.teachers.GetFullObjectById(diploma.TeacherID);

            var result = new DisplayDiplomaViewModel()
            {
                Id = diploma.Id,
                Title = diploma.Title,
                Description = diploma.Description,
                ExperimentalPart = diploma.ExperimentalPart,
                ContentCSV = diploma.ContentCSV
                                    .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                                    .ToList(),
                CreatedOn = diploma.CreatedOn.ToString(),
                ModifiedOn = diploma.ModifiedOn.ToString(),
                DeletedOn = diploma.DeletedOn.ToString(),
                IsDeleted = diploma.IsDeleted,
                ApprovedByLeader = diploma.IsApprovedByLeader,
                ApprovedByHead = diploma.IsApprovedByHead,
                TeacherID = diploma.TeacherID,
                TeacherName = string.Format("{0} {1}", user.User.FirstName, user.User.LastName)
            };

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

            var teacher = this.teachers.GetByUserId(this.User.Identity.GetUserId());
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
    }
}
