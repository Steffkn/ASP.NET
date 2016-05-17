namespace DDS.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using Common.Models;
    using DDS.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Teacher> Teachers { get; set; }

        public IDbSet<Diploma> Diplomas { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                        .HasKey(s => s.Id);

            // Map one-to-zero or one relationship
            modelBuilder.Entity<Student>()
                .HasRequired(s => s.User)
                .WithOptional(u => u.Student);

            modelBuilder.Entity<Student>()
                        .HasKey(s => s.Id);

            // Map one-to-zero or one relationship
            modelBuilder.Entity<Teacher>()
                .HasRequired(t => t.User)
                .WithOptional(u => u.Teacher);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
