namespace DDS.Web.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;
    using Services.Data.Interfaces;
    using ViewModels.Tags;

    public class TagsController : BaseController
    {
        private readonly ITagsService tags;
        private readonly IDiplomasService diplomas;

        public TagsController(ITagsService tags, IDiplomasService diplomas)
        {
            this.tags = tags;
            this.diplomas = diplomas;
        }

        public ActionResult Index()
        {
            var diploma = this.diplomas.GetAll().Where(d => d.Id == 6).Include(d => d.Tags).FirstOrDefault();

            var tagove = diploma.Tags.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });

            return this.View(new DiplomaTestViewModel() { Tags = tagove });
        }

        [HttpPost]
        public ActionResult Index(DiplomaTestViewModel diplomaTest)
        {
            var tagList = new List<Tag>();
            if (diplomaTest.TagsArray != null)
            {
                foreach (var item in diplomaTest.TagsArray)
                {
                    int id;
                    if (int.TryParse(item, out id))
                    {
                        tagList.Add(this.tags.GetObjectById(id));
                    }
                    else
                    {
                        tagList.Add(this.tags.EnsureCategory(item));
                    }
                }
            }

            diplomaTest.Tags = tagList.Select(x => 
            new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = true
            }).ToList();
            return this.View(diplomaTest);
        }

        public JsonResult GetAllSelectedTags(string searchTerm, int pageSize, int pageNumber)
        {
            var tagsFromDB = this.tags.GetAll().To<SelectTagViewModel>();
            var resultList = new List<SelectTagViewModel>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                resultList = tagsFromDB
                    .Where(n => n.text.ToLower()
                    .Contains(searchTerm.ToLower()))
                    .ToList();
            }
            else
            {
                resultList = tagsFromDB.ToList();
            }

            var results = resultList.OrderBy(t => t.text).Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

            return this.Json(new { Results = results, Total = resultList.Count }, JsonRequestBehavior.AllowGet);
        }
    }
}
