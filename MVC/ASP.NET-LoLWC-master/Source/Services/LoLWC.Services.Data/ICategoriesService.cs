namespace LoLWC.Services.Data
{
    using System.Linq;

    using LoLWC.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}
