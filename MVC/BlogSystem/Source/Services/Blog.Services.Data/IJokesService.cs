namespace Blog.Services.Data
{
    using System.Linq;

    using Blog.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}
