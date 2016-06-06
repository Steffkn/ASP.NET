namespace DDS.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;

    public class DiplomasService : BaseService<Diploma>, IDiplomasService
    {
        public DiplomasService(IDbRepository<Diploma> diplomas)
            : base(diplomas)
        {
        }

        public async Task<Diploma> GetFullObjectById(int id)
        {
            var diploma = this.Items.All().Where(d => d.Id == id);
            diploma.Include(e => e.Tags)
                .Include(e => e.Teacher)
                .Include(e => e.Teacher.User);
            return await diploma.FirstOrDefaultAsync();
        }

        public IQueryable<Diploma> GetByTeacherId(int id)
        {
            var diplomas = this.Items.All().Where(d => d.TeacherID == id);
            return diplomas;
        }

        public IQueryable<Diploma> GetRandomDiplomas(int count)
        {
            return this.Items.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }

        public override void Edit(Diploma entity)
        {
            this.Items.GetById(entity.Id).Title = entity.Title;
            this.Items.GetById(entity.Id).Description = entity.Description;
            this.Items.GetById(entity.Id).IsApprovedByHead = entity.IsApprovedByHead;
            this.Items.GetById(entity.Id).IsApprovedByLeader = entity.IsApprovedByLeader;
            this.Items.GetById(entity.Id).ContentCSV = entity.ContentCSV;
            this.Items.GetById(entity.Id).ExperimentalPart = entity.ExperimentalPart;
            this.Items.GetById(entity.Id).IsSelectedByStudent = entity.IsSelectedByStudent;
            foreach (var tag in entity.Tags)
            {
                if (!this.Items.GetById(entity.Id).Tags.Contains(tag))
                {
                    this.Items.GetById(entity.Id).Tags.Add(tag);
                }
            }

            base.Edit(entity);
        }
    }
}
