namespace ElementsWeb.Services.Data
{
    using System.Linq;
    using ElementsWeb.Data.Common;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Services.Data.Interfaces;

    public class CharacterService : BaseService<Character>, ICharacterService
    {
        public CharacterService(IDbRepository<Character> characters)
            : base(characters)
        {
        }

        public IQueryable<Character> GetByName(string name)
        {
            return this.Items.All().Where(s => s.Name == name);
        }

        public IQueryable<Character> GetAllByUserName(string userName)
        {
            return this.Items.All().Where(ch => ch.User.UserName == userName);
        }
    }
}
