namespace ElementsWeb.Services.Data.Interfaces
{
    using System.Linq;
    using ElementsWeb.Data.Models;

    public interface IUserService
    {
        IQueryable<ApplicationUser> GetAll();

        ApplicationUser GetById(string userId);

        IQueryable<Character> GetAllCharactersOfUser(string userId);

        IQueryable<Character> GetCharacterOfUserByName(string userId, string name);

        IQueryable<Character> GetCharacterOfUserById(string userId, int id);

        void CreateCharacter(string userId, Character entity);

        void EditCharacter(Character entity);

        void DeleteCharacter(Character entity);
    }
}
