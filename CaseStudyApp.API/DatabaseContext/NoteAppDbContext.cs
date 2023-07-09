using CaseStudyApp.API.Entities;
using CaseStudyApp.API.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace CaseStudyApp.API.DatabaseContext
{
    public class NoteAppDbContext : DbContext
    {
        public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).IsRequired(false).OnDelete(DeleteBehavior.NoAction);
        }

        public override int SaveChanges()
        {

            foreach (EntityEntry<IAudit> entry in ChangeTracker.Entries<IAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        break;
                }
            }

            return base.SaveChanges();
        }


    }
}
