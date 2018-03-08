namespace ElementsWeb.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Services.Data.Interfaces;
    using ElementsWeb.Web.Infrastructure.Mapping;
    using ElementsWeb.Web.ViewModels.Home;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    public class HomeController : BaseController
    {
        private readonly IUserService users;
        private readonly ICharacterService characters;

        private ApplicationUserManager userManager;

        public HomeController(
            IUserService users,
            ICharacterService characters)
        {
            this.users = users;
            this.characters = characters;
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
            return this.View();
        }

        [HttpGet]
        public ActionResult CreateCharacter()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCharacter(CharacterViewModel viewModel)
        {
            viewModel.Username = this.User.Identity.Name;

            if (this.ModelState.IsValid)
            {
                var newCharacter = new Character()
                {
                    Name = viewModel.Name,
                };

                this.users.CreateCharacter(this.User.Identity.GetUserId(), newCharacter);

                return this.RedirectToAction("Characters", "Home");
            }

            return this.View(viewModel);
        }

        /// <summary>
        /// Done
        /// </summary>
        /// <returns>Returns all teachers with their tags</returns>
        public ActionResult Characters()
        {
            var characters = this.characters.GetAllByUserName(this.User.Identity.Name).To<CharacterViewModel>();

            return this.View(characters.ToList());
        }
    }
}
