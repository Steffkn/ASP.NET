using System.Security.Claims;

namespace ElementsWeb.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Services.Data.Interfaces;
    using ElementsWeb.Web.Infrastructure.Mapping;
    using ElementsWeb.Web.ViewModels.API;
    using ElementsWeb.Web.ViewModels.Home;
    using Microsoft.AspNet.Identity.Owin;

    public class ApiController : BaseController
    {
        private readonly IUserService users;
        private readonly ICharacterService characters;

        private ApplicationSignInManager signInManager;

        public ApiController(
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
            get { return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }

            private set { this.signInManager = value; }
        }

        // GET: API
        public JsonResult Index()
        {
            return this.Json(new { Results = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCharacters(string username)
        {
            var characters = this.characters.GetAllByUserName(username).To<CharacterViewModel>().ToList();
            return this.Json(new { Characters = characters }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CreateCharacter(string foo)
        {
            // var characterJson = System.Web.Helpers.Json.Decode(json);
            // string userName = (string)characterJson.UserName;
            var data = foo.Split(new char[] { '_' });
            string userName = data[0];
            string characterName = data[1];
            var userko = this.users.GetAll().FirstOrDefault(u => u.UserName == userName);
            var newCharacter = new Character()
            {
                Name = characterName,
                Level = 1,
                CurentExperiense = 0,
                CreatedOn = DateTime.UtcNow,
            };

            this.users.CreateCharacter(userko.Id, newCharacter);
            var result = this.characters.GetByName(characterName).To<CharacterViewModel>().ToList();

            return this.Json(new { Results = result }, JsonRequestBehavior.AllowGet);
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> LogIn(string input)
        {
            var data = input.Split(new char[] { '_' });
            string userName = data[0];
            string passowrd = data[1];

            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String ecn = System.Environment.MachineName;
            // JsonSerializer js = new JsonSerializer();
            // JsonReader j = new JsonTextReader(TextReader.Null);
            // object o = js.Deserialize(j);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await this.SignInManager.PasswordSignInAsync(
                userName,
                passowrd,
                isPersistent: false,
                shouldLockout: false);

            var user = this.users.GetAll().FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                result = SignInStatus.RequiresVerification;
            }

            switch (result)
            {
                case SignInStatus.Success:
                    var characters = this.characters.GetAllByUserName(userName).To<SimpleCharacterViewModel>().ToArray();
                    return this.Json(
                        new
                        {
                            Results = true,
                            Characters = characters,
                            UserId = user.Id,
                            Claims = computer_name[0].ToString()
                        }, behavior: JsonRequestBehavior.AllowGet);
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
                };

                this.users.CreateCharacter(userId, newCharacter);
                var result = this.characters.GetByName(input).To<SimpleCharacterViewModel>().ToList();

                return this.Json(new { Results = true, Character = result }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new { Results = false, Error = "Invalid credentials" }, JsonRequestBehavior.AllowGet);
        }
    }
}
