namespace Blog.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data;
    using ViewModels.Post;

    public class PostController : BaseController
    {
        private readonly IPostService posts;

        public PostController(
            IPostService posts)
        {
            this.posts = posts;
        }

        public ActionResult ById(int id)
        {
            var post = this.posts.GetById(id);

            if (post == null)
            {
                this.RedirectToAction("Index", "Home");
            }

            var viewModel = this.Mapper.Map<PostViewModel>(post);
            viewModel.ImageURL = @"..\.." + viewModel.ImageURL.Substring(viewModel.ImageURL.IndexOf(@"\Content\"));
            return this.View(viewModel);
        }
    }
}