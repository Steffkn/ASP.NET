namespace DDS.Services.Data
{
    using System;
    using System.Linq;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class DiplomasService : IDiplomasService
    {
        private readonly IDbRepository<Diploma> diplomas;

        public DiplomasService(IDbRepository<Diploma> diplomas)
        {
            this.diplomas = diplomas;
        }

        public Diploma GetById(int id)
        {
            var diploma = this.diplomas.GetById(id);
            return diploma;
        }

        public IQueryable<Diploma> GetRandomDiplomas(int count)
        {
            return this.diplomas.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }

        public IQueryable<Diploma> GetAll()
        {
            return this.diplomas.All().OrderBy(x => x.Title);
        }

        public IQueryable<Diploma> GetAllByCreatedDate()
        {
            return this.diplomas.All().OrderBy(x => x.CreatedOn);
        }

        public void Delete(Diploma entity)
        {
            this.diplomas.Delete(entity);
            this.diplomas.Save();
        }

        public void Create(Diploma entity)
        {
            this.diplomas.Add(entity);
            this.diplomas.Save();
        }

        public void Edit(Diploma entity)
        {
            this.diplomas.GetById(entity.Id).Title = entity.Title;
            this.diplomas.GetById(entity.Id).Description = entity.Description;
            this.diplomas.Save();
        }
    }
}
