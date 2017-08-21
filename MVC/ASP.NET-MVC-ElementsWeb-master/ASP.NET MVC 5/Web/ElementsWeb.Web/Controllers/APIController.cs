namespace ElementsWeb.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Services.Data.Interfaces;
    using ElementsWeb.Web.Infrastructure.Mapping;
    using ElementsWeb.Web.ViewModels.Home;
    using Microsoft.AspNet.Identity.Owin;
    using ElementsWeb.Web.ViewModels.API;

    public class APIController : BaseController
    {
        private readonly IUserService users;
        private readonly ICharacterService characters;

        private ApplicationSignInManager signInManager;

        public APIController(
            IUserService users,
            ICharacterService characters,
            ApplicationSignInManager signInManager)
        {
            this.users = users;
            this.characters = characters;
            this.SignInManager = signInManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        // GET: API
        public JsonResult Index()
        {
            return this.Json(new { Results = "Error" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCharacters(string username)
        {
            var characters = this.characters.GetAllByUserName(username).To<CharacterViewModel>().ToList();
            return this.Json(new { Characters = characters }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CreateCharacter(string foo)
        {
            //var characterJson = System.Web.Helpers.Json.Decode(json);
            //string userName = (string)characterJson.UserName;

            var data = foo.Split(new char[] { '_' });
            string userName = data[0];
            string characterName = data[1];
            var userko = this.users.GetAll().Where(u => u.UserName == userName).FirstOrDefault();

            var newCharacter = new Character()
            {
                Name = characterName,
            };

            this.users.CreateCharacter(userko.Id, newCharacter);
            var result = this.characters.GetByName(characterName).To<CharacterViewModel>().ToList();

            return this.Json(new { Results = result }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> LogIn(string input)
        {
            var data = input.Split(new char[] { '_' });
            string userName = data[0];
            string passowrd = data[1];

            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await this.SignInManager.PasswordSignInAsync(
                userName,
                passowrd,
                isPersistent: false,
                shouldLockout: false);

            var characters = this.characters.GetAllByUserName(userName).To<SimpleCharacterViewModel>().ToArray();

            var userId = this.users.GetAll().Where(u => u.UserName == userName).FirstOrDefault().Id;

            switch (result)
            {
                case SignInStatus.Success:
                    return this.Json(new { Results = true, Characters = characters, UserId = userId }, JsonRequestBehavior.AllowGet);
                case SignInStatus.LockedOut:
                    return this.Json(new { Results = false }, JsonRequestBehavior.AllowGet);
                case SignInStatus.RequiresVerification:
                    return this.Json(new { Results = false }, JsonRequestBehavior.AllowGet);
                case SignInStatus.Failure:
                default:
                    return this.Json(new { Results = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult IsCharacterNameFree(string input, string userId)
        {
            var wantedCharacter = this.characters.GetByName(input).FirstOrDefault();

            if (wantedCharacter != null)
            {
                return this.Json(new { Results = false, Error = "Name is already taken" }, JsonRequestBehavior.AllowGet);
            }

            if (userId != string.Empty || userId != null)
            {

                var newCharacter = new Character()
                {
                    Name = input,
                    MaxHealth = 100,
                    MaxResource = 100,
                    RandomNumber = 666
                };

                this.users.CreateCharacter(userId, newCharacter);
                var result = this.characters.GetByName(input).To<SimpleCharacterViewModel>().ToList();

                return this.Json(new { Results = true, Character = result }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { Results = false, Error = "Invalid credentials" }, JsonRequestBehavior.AllowGet);
        }
    }
}