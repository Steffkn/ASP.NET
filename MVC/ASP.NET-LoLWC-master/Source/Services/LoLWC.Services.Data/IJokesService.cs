namespace LoLWC.Services.Data
{
    using System.Linq;

    using LoLWC.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}
