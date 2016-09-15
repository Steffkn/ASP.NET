namespace DDS.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using PagedList;
    using Services.Data.Interfaces;
    using ViewModels.ManageDiplomas;
    using ViewModels.Shared;

    public class HomeController : BaseController
    {
        private readonly IDiplomasService diplomas;
        private readonly ITeachersService teachers;
        private readonly IStudentsService students;
        private readonly IMessagesService messages;
        private readonly ITagsService tags;

        private ApplicationUserManager userManager;

        public HomeController(
            IDiplomasService diplomas,
            ITeachersService teachers,
            IStudentsService students,
            IMessagesService messages,
            ITagsService tags)
        {
            this.diplomas = diplomas;
            this.teachers = teachers;
            this.students = students;
            this.messages = messages;
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

        /// <summary>
        /// Returns the index page (start up page)
        /// </summary>
        /// <returns>Index page</returns>
        public ActionResult Index()
        {
            var tags =
                 this.Cache.Get(
                     "tags",
                     () => this.tags.GetAll().To<TagViewModel>().ToList(),
                     30 * 60);
            return this.View();
        }

        public ActionResult DownloadFile(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetObjectById(intId);
            if (diploma == null)
            {
                this.TempData["Message"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Index", "Home");
            }

            var result = new StudentDiplomaViewModel();

            var studentDetails = this.students.GetAll()
                                              .Where(s => s.SelectedDiploma.Id == diploma.Id)
                                              .Include(s => s.User)
                                              .To<SimpleStudentViewModel>()
                                              .FirstOrDefault();
            result.Student = studentDetails;

            if (result.Student != null)
            {
                if (string.IsNullOrEmpty(result.Student.Address) || string.IsNullOrEmpty(result.Student.Email)
                    || string.IsNullOrEmpty(result.Student.FirstName) || string.IsNullOrEmpty(result.Student.MiddleName)
                    || string.IsNullOrEmpty(result.Student.LastName) || string.IsNullOrEmpty(result.Student.PhoneNumber)
                    || result.Student.FNumber == 0)
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            var diplomaModel = this.diplomas.GetAll()
                                             .Where(d => d.Id == intId)
                                             .Include(d => d.Teacher)
                                             .Include(d => d.Tags)
                                             .To<DisplayDiplomaViewModel>();

            result.Diploma = diplomaModel.FirstOrDefault();
            result.Diploma.ContentCSV = diploma.ContentCSV.Split(new char[] { GlobalConstants.ContentSeparator }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            result.Diploma.TeacherID = diploma.TeacherID;
            result.Diploma.Tags = diploma.Tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Disabled = true,
                Selected = true,
                Value = t.Id.ToString()
            });

            var teacher = this.teachers.GetById(diploma.TeacherID).Include(t => t.User).FirstOrDefault();
            result.Diploma.TeacherName = string.Format("{0} {1}. {2}", teacher.User.ScienceDegree, teacher.User.FirstName[0], teacher.User.LastName).Trim();

            if (result.Diploma != null)
            {
                if (result.Diploma.ContentCSV.Count == 0 || string.IsNullOrEmpty(result.Diploma.ExperimentalPart)
                    || result.Diploma.Tags.Count() == 0 || string.IsNullOrEmpty(result.Diploma.TeacherName)
                    || string.IsNullOrEmpty(result.Diploma.Title) || string.IsNullOrEmpty(result.Diploma.ModifiedOn.ToString()))
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            Dictionary<string, string> proparties = new Dictionary<string, string>();

            proparties.Add("[USER_NAME]", string.Format("{0} {1} {2} {3}", result.Student.ScienceDegree, result.Student.FirstName, result.Student.MiddleName, result.Student.LastName));
            proparties.Add("[USER_FNUMBER]", result.Student.FNumber.ToString());
            proparties.Add("[USER_ADDRESS]", result.Student.Address);
            proparties.Add("[USER_TEL]", result.Student.PhoneNumber);
            proparties.Add("[USER_EMAIL]", result.Student.Email);
            proparties.Add("[DOC_TITLE]", result.Diploma.Title.ToString());
            proparties.Add("[DOC_BEGINDATE]", result.Diploma.ModifiedOn.Value.ToString("dd/MM/yyyy"));
            proparties.Add("[DOC_ENDDATA]", result.Diploma.ModifiedOn.Value.AddYears(1).ToString("dd/MM/yyyy"));
            proparties.Add("[DOC_EXP]", result.Diploma.ExperimentalPart);
            proparties.Add("[TEACHER]", result.Diploma.TeacherName);
            proparties.Add("[CONSULTANT]", string.Empty);
            proparties.Add("[DEP_HEAD]", "доц. д-р М. Лазарова");
            proparties.Add("[DEAN]", "проф. д-р Д. Гоцева");

            int i = 1;
            foreach (var item in result.Diploma.ContentCSV)
            {
                string placeHolder = string.Format("[DOC_{0}]", i);
                proparties.Add(placeHolder, string.Format("{0}. {1}", i++, item));
            }

            while (i <= 8)
            {
                string placeHolder = string.Format("[DOC_{0}]", i++);
                proparties.Add(placeHolder, string.Empty);
            }

            StringBuilder tags = new StringBuilder();

            foreach (var tag in result.Diploma.Tags)
            {
                tags.Append(tag.Text + ", ");
            }

            string tagsString = tags.Remove(tags.Length - 2, 2).ToString();

            proparties.Add("[DOC_TECH]", tagsString.ToString());

            var filePath = DocXGenerator.Generate(proparties, this.Server.MapPath("~/Content/DocxFiles/Templates/DiplomaTemplate.docx"));
            var fileName = string.Format("{0}.docx", proparties["[USER_FNUMBER]"]);
            return this.File(filePath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        /// <summary>
        /// Returns details for a diploma by id of the diploma
        /// </summary>
        /// <param name="id">ID of the diploma</param>
        /// <returns>Returns details for a diploma</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Diplomas", "Home");
            }

            var userID = this.User.Identity.GetUserId();
            var currentStudent = this.students.GetByUserId(userID).Include(s => s.SelectedDiploma).FirstOrDefault();

            if (currentStudent != null)
            {
                if (currentStudent.SelectedDiploma != null)
                {
                    this.TempData["DiplomaIsSelected"] = "Вече сте избрали диплома";

                    if (currentStudent.SelectedDiploma.Id == id)
                    {
                        this.TempData["IsOwnDiploma"] = true;
                    }
                }
            }

            int intId = id ?? 0;
            var diploma = this.diplomas.GetObjectById(intId);

            if (diploma == null)
            {
                this.TempData["DiplomasNotFound"] = "Дипломата не бе намерена!";
                return this.RedirectToAction("Diplomas", "Home");
            }

            var result = this.diplomas.GetById(intId)
                                        .Include(d => d.Tags)
                                        .Include(d => d.Teacher)
                                        .To<DisplayDiplomaViewModel>().FirstOrDefault();

            result.ContentCSV = diploma.ContentCSV.Split(new char[] { GlobalConstants.ContentSeparator }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            result.Tags = diploma.Tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Disabled = true,
                Selected = true,
                Value = t.Id.ToString()
            });

            var teacher = this.teachers.GetById(diploma.TeacherID).Include(t => t.User).FirstOrDefault();
            result.TeacherName = string.Format("{0} {1} {2}", teacher.User.ScienceDegree, teacher.User.FirstName, teacher.User.LastName).Trim();

            var userId = this.User.Identity.GetUserId();
            var messages = this.messages.GetAll().Where(mes => mes.SelectedDiploma.Id == diploma.Id
                                                            && (mes.ResieverUserId == userId
                                                            || mes.SenderUserId == userId))
                                                            .To<MessageViewModel>().ToList();
            foreach (var message in messages)
            {
                message.SenderUser = this.UserManager.FindById(message.SenderUserId);
                message.ResieverUser = this.UserManager.FindById(message.ResieverUserId);
            }

            this.ViewBag.Messages = messages;
            this.ViewBag.CurrentUserId = userID;

            return this.View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.StudentRoleName)]
        public ActionResult Select(int id)
        {
            var selectedDiploma = this.diplomas.GetById(id).Include(d => d.Teacher).FirstOrDefault();
            var student = this.students.GetByUserId(this.User.Identity.GetUserId()).Include(s => s.User).FirstOrDefault();

            if (student != null)
            {
                if (student.Address == null || student.FNumber == 0 || student.User.PhoneNumber == null)
                {
                    this.TempData["UpdateProfile"] = "Моля попълнете личната си информация преди да изберете дипломна работа!";
                    return this.RedirectToAction("Details", new { id = id });
                }
            }

            if (selectedDiploma.Teacher != null)
            {
                selectedDiploma.Teacher.Students.Count();
                selectedDiploma.Teacher.Students.Add(student);
            }

            if (selectedDiploma != null && student != null)
            {
                selectedDiploma.IsSelectedByStudent = true;
                student.SelectedDiploma = selectedDiploma;
                this.students.Save();
            }

            this.TempData["DiplomaIsSelectedSuccesfully"] = string.Format("Дипломата \'{0}\' е избрана успешно!", selectedDiploma.Title);

            return this.RedirectToAction("Diplomas");
        }

        /// <summary>
        /// DONE
        /// </summary>
        /// <param name="sortOrder">Order to be used for sorting</param>
        /// <param name="currentFilter">Filter</param>
        /// <param name="searchString">Searching string</param>
        /// <param name="page">Page that we are at</param>
        /// <returns>Sorted diplomas</returns>
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

            var diplomas = this.diplomas.GetAll().Where(d => !d.IsSelectedByStudent).To<CommonDiplomaViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomas = diplomas.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            if (diplomas.LongCount() <= 0)
            {
                this.TempData["DiplomasNotFound"] = "Не са намерени дипломи!";
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

            int pageSize = GlobalConstants.PageSize;
            int pageNumber = page ?? 1;
            return this.View(diplomas.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <returns>Returns all teachers with their tags</returns>
        public ActionResult Teachers()
        {
            var teachers = this.teachers.GetAll().Include(t => t.Tags).To<TeacherViewModel>();

            return this.View(teachers);
        }

        /// <summary>
        /// DONE
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <returns>Returns a teacher from a given ID and hes/hers diplomas</returns>
        public ActionResult TeacherDetails(int? id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Teachers", "Home");
            }

            int intId = id ?? 0;

            var teacher = this.teachers.GetById(intId).To<TeacherViewModel>().FirstOrDefault();

            if (teacher == null)
            {
                this.TempData["Message"] = "Учителят не бе намерена!";
                return this.RedirectToAction("Teachers", "Home");
            }

            TeacherDiplomasViewModel teacherDiplomaVM = new TeacherDiplomasViewModel();
            teacherDiplomaVM.TeacherDetails = teacher;
            teacherDiplomaVM.Diplomas = this.diplomas.GetAll().Where(d => d.TeacherID == teacher.Id && !d.IsSelectedByStudent).ToList();

            return this.View(teacherDiplomaVM);
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <param name="id">Id of the tag</param>
        /// <param name="teacherId">Id of the teacher</param>
        /// <param name="sortOrder">Order to be used for sorting</param>
        /// <param name="currentFilter">Filter</param>
        /// <param name="searchString">Searching string</param>
        /// <param name="page">Page that we are at</param>
        /// <returns>Returns diplomas by tag</returns>
        public ActionResult Tag(int? id, int? teacherId, string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (id == null)
            {
                return this.RedirectToAction("Diplomas");
            }

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

            int intId = id ?? 0;
            var tag = this.tags.GetObjectById(intId);

            var diplomas = this.diplomas.GetAll()
                .Where(d => !d.IsSelectedByStudent && (d.TeacherID == teacherId || teacherId == null))
                .To<CommonDiplomaViewModel>()
                .ToList()
                .Where(d => d.Tags.Contains(tag));

            if (!string.IsNullOrEmpty(searchString))
            {
                diplomas = diplomas.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            if (diplomas.LongCount() <= 0)
            {
                this.TempData["DiplomasNotFound"] = "Не са намерени дипломи!";
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

            int pageSize = GlobalConstants.PageSize;
            int pageNumber = page ?? 1;
            return this.View(diplomas.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult GetAllTags(string searchTerm, int pageSize, int pageNumber)
        {
            var resultList = new List<TagViewModel>();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                resultList = this.tags.GetAll().To<TagViewModel>()
                    .Where(n => n.text.ToLower()
                    .Contains(searchTerm.ToLower()))
                    .ToList();
            }
            else
            {
                resultList = this.tags.GetAll().To<TagViewModel>().ToList();
            }

            var results = resultList.OrderBy(t => t.text).Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

            return this.Json(new { Results = results, Total = resultList.Count }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(FormCollection formCollection, int diplomaId, int teacherId)
        {
            string message = formCollection["message"];
            if (this.ModelState.IsValid)
            {
                var senderId = this.User.Identity.GetUserId();
                var resieverId = this.teachers.GetById(teacherId)
                                                .Include(s => s.User)
                                                .Select(s => s.User.Id)
                                                .FirstOrDefault();

                Diploma selectedDiploma = this.diplomas.GetById(diplomaId).FirstOrDefault();

                var messageEntity = new Message()
                {
                    SenderUserId = senderId,
                    ResieverUserId = resieverId,
                    MessageSend = message,
                    SelectedDiploma = selectedDiploma,
                    IsRead = false,
                };

                this.messages.Create(messageEntity);
            }

            return this.RedirectToAction("Details", new { Id = diplomaId });
        }
    }
}
