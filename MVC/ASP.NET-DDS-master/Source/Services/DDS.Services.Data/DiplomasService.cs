namespace DDS.Services.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;

    public class DiplomasService : BaseService<Diploma>, IDiplomasService
    {
        public DiplomasService(IDbRepository<Diploma> diplomas)
            : base(diplomas)
        {
        }

        public IQueryable<Diploma> GetByTeacherId(int id)
        {
            return this.Items.All().Where(d => d.TeacherID == id);
        }

        public IQueryable<Diploma> GetRandomDiplomas(int count)
        {
            return this.Items.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }
    }
}
