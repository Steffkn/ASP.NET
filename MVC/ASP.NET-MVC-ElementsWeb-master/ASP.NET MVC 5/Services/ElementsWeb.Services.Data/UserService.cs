namespace ElementsWeb.Services.Data
{
    using System;
    using System.Linq;
    using ElementsWeb.Data;
    using ElementsWeb.Data.Common;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Services.Data.Interfaces;

    public class UserService : IUserService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IDbRepository<Character> characters;

        public UserService()
        {
            this.characters = new DbRepository<Character>(this.db);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return this.db.Users.OrderBy(u => u.UserName);
        }

        public ApplicationUser GetById(string userId)
        {
            return this.db.Users.Find(userId);
        }

        public IQueryable<Character> GetAllCharactersOfUser(string userId)
        {
            return this.db.Characters.Where(ch => ch.User.Id == userId).OrderBy(ch => ch.Name);
        }

        public IQueryable<Character> GetCharacterOfUserById(string userId, int id)
        {
            var characters = this.db.Characters.Where(ch => ch.User.Id == userId);
            return characters.Where(ch => ch.Id == id);
        }

        public IQueryable<Character> GetCharacterOfUserByName(string userId, string name)
        {
            var characters = this.db.Characters.Where(ch => ch.User.Id == userId);
            return characters.Where(ch => ch.Name == name);
        }

        public void CreateCharacter(string userId, Character entity)
        {
            var user = this.GetById(userId);
            entity.User = user;
            this.db.Characters.Add(entity);
            this.db.SaveChanges();
        }

        public void DeleteCharacter(Character entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.db.SaveChanges();
        }

        public void EditCharacter(Character entity)
        {
            this.db.Characters.FirstOrDefault(s => s.Id == entity.Id).ModifiedOn = DateTime.UtcNow;
            this.db.SaveChanges();
        }
    }
}
