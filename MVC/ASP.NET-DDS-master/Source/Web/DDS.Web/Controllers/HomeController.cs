namespace DDS.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Interfaces;
    using ViewModels.Home;
    using ViewModels.ManageDiplomas;
    using ViewModels.Shared;
    using PagedList;
    public class HomeController : BaseController
    {
        private readonly IDiplomasService diplomas;
        private readonly ITeachersService teachers;
        private readonly ITagsService tags;

        private IQueryable<TagViewModel> allOptionsList;

        private ApplicationUserManager userManager;

        public HomeController(
            IDiplomasService diplomas,
            ITeachersService teachers,
            ITagsService tags)
        {
            this.diplomas = diplomas;
            this.teachers = teachers;
            this.tags = tags;
            this.allOptionsList = this.GetSelect2Options();
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

        public ActionResult Index()
        {
            var randomDiplomas = this.diplomas.GetRandomDiplomas(9)
                                                .To<CommonDiplomaViewModel>();
            return this.View(randomDiplomas.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetById(intId);
            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "Home");
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
                TeacherName = string.Format("{0} {1}", user.User.FirstName, user.User.LastName),
                Tags = diploma.Tags.Select(t =>
                                    new SelectListItem
                                    {
                                        Text = t.Name,
                                        Value = t.Id.ToString(),
                                    })
            };

            return this.View(result);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            int intId = id ?? 0;
            var diploma = await this.diplomas.GetFullObjectById(intId);

            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "Home");
            }

            var count = diploma.Tags.Count();
            var teacherUser = diploma.Teacher.User;
            var viewModel = new DisplayDiplomaViewModel()
            {
                Id = diploma.Id,
                Title = diploma.Title,
                Description = diploma.Description,
                ExperimentalPart = diploma.ExperimentalPart,
                ContentCSV = diploma.ContentCSV
                                    .Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                                    .ToList(),
                CreatedOn = diploma.CreatedOn.ToString(),
                TeacherID = diploma.Teacher.Id,
                Tags = diploma.Tags.Select(t =>
                                    new SelectListItem
                                    {
                                        Text = t.Name,
                                        Value = t.Name,
                                    })
                //TeacherName = diploma.Teacher.User.FirstName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public ActionResult Edit(DisplayDiplomaViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                var diploma = new Diploma()
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    ExperimentalPart = viewModel.ExperimentalPart,
                    ContentCSV = string.Join(",", viewModel.ContentCSV),
                    IsSelectedByStudent = true,
                };
                diploma.Tags = new List<Tag>();
                foreach (var tag in viewModel.TagsNames)
                {
                    diploma.Tags.Add(this.tags.EnsureCategory(tag));
                }

                this.diplomas.Edit(diploma);
            }

            return this.View(viewModel);
        }

        public JsonResult GetAllTags(string searchTerm, int pageSize, int pageNumber)
        {
            var resultList = new List<TagViewModel>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                resultList = this.allOptionsList
                    .Where(n => n.text.ToLower()
                    .Contains(searchTerm.ToLower()))
                    .ToList();
            }
            else
            {
                resultList = this.allOptionsList.ToList();
            }

            var results = resultList.OrderBy(t => t.text).Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

            return this.Json(new { Results = results, Total = resultList.Count }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Select()
        //{
        //    var test = new TestViewModel();
        //    test.Id = 10;
        //    test.Title = "title of the century";

        //    var list = new List<Tag>();
        //    list.Add(new Tag { Id = 1, Name = "test one" });
        //    list.Add(new Tag { Id = 2, Name = "test two" });
        //    list.Add(new Tag { Id = 3, Name = "test tree" });
        //    list.Add(new Tag { Id = 4, Name = "test tree" });

        //    test.Tags = list;

        //    ViewBag.Message = "";
        //    return this.View(test);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Select(TestViewModel viewModel)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        ViewBag.ModelState = "Model is valid TestViewModel";
        //    }
        //    else
        //    {
        //        ViewBag.ModelState = "Model is <strong>NOT</strong> valid TestViewModel";
        //    }

        //    var tags = "";
        //    foreach (var item in viewModel.SelectedTags)
        //    {
        //        tags = tags + " " + this.tags.GetById(item).Name;
        //    }

        //    ViewBag.Message = tags;

        //    var list = new List<Tag>();
        //    list.Add(new Tag { Id = 1, Name = "test one" });
        //    list.Add(new Tag { Id = 2, Name = "test two" });
        //    list.Add(new Tag { Id = 3, Name = "test tree" });
        //    list.Add(new Tag { Id = 4, Name = "test tree" });

        //    var result = new List<Tag>();
        //    for (int i = 0; i < viewModel.SelectedTags.Length; i++)
        //    {
        //        result.Add(list.Find(x => x.Id == viewModel.SelectedTags[i]));
        //    }

        //    viewModel.Tags = result;

        //    return this.View(viewModel);
        //}

        public IQueryable<TagViewModel> GetSelect2Options()
        {
            var tags = this.tags.GetAll();
            var optionList = new List<TagViewModel>();
            foreach (var tag in tags)
            {
                optionList.Add(new TagViewModel
                {
                    id = tag.Id.ToString(),
                    text = tag.Name
                });
            }

            return optionList.AsQueryable();
        }

        public ActionResult Diplomas(string sortOrder, string currentFilter, string searchString, int? page)
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

            var diplomas = this.diplomas.GetAll().To<CommonDiplomaViewModel>();

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
