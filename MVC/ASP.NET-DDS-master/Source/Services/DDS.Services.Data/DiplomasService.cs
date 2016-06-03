namespace DDS.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;
    using System.Threading.Tasks;

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

        public async Task<Diploma> GetByIdFullObject(int id)
        {
            var diploma = this.diplomas.All().Where(d => d.Id == id);
            diploma.Include(e => e.Tags)
                .Include(e => e.Teacher)
                .Include(e => e.Teacher.User);
            return await diploma.FirstOrDefaultAsync();
        }

        public IQueryable<Diploma> GetByTeacherId(int id)
        {
            var diplomas = this.diplomas.All().Where(d => d.TeacherID == id);
            return diplomas;
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
            this.diplomas.GetById(entity.Id).ApprovedByHead = entity.ApprovedByHead;
            this.diplomas.GetById(entity.Id).ApprovedByLeader = entity.ApprovedByLeader;
            this.diplomas.GetById(entity.Id).ContentCSV = entity.ContentCSV;
            this.diplomas.GetById(entity.Id).ExperimentalPart = entity.ExperimentalPart;
            this.diplomas.GetById(entity.Id).ModifiedOn = DateTime.Now;
            this.diplomas.Save();
        }
    }
}
