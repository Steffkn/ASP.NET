namespace ElementsWeb.Services.Data.Interfaces
{
    using System.Linq;
    using ElementsWeb.Data.Models;

    public interface ICharacterService : IBaseService<Character>
    {
        IQueryable<Character> GetByName(string name);

        IQueryable<Character> GetAllByUserName(string userName);
    }
}
