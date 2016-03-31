namespace DDS.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;

    using Services.Data;
    using ViewModels.Diplomas;

    public class HomeController : BaseController
    {
        private readonly IDiplomasService diplomas;
        private readonly ITagsService tags;

        public HomeController(
            IDiplomasService diplomas,
            ITagsService tags)
        {
            this.diplomas = diplomas;
            this.tags = tags;
        }

        public ActionResult Index()
        {
            //var diplomas = this.diplomas.GetRandomDiplomas(1).To<DiplomaViewModel>().ToList();
            //var tags = this.tags.GetAll().To<TagViewModel>().ToList();
            //    //this.Cache.Get(
            //    //    "tags",
            //    //    () => this.tags.GetAll().To<TagViewModel>().ToList(),
            //    //    30 * 60);
            //var viewModel = new IndexViewModel
            //{
            //    Diplomas = diplomas,
            //    Tags = tags
            //};

            //return this.View(viewModel);
            return this.View();
        }
    }
}
