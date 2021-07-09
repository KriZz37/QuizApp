using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSIiZ_WPF.Entities;

namespace WSIiZ_WPF.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Folder tree
            modelBuilder.Entity<Folder>()
                .HasMany(x => x.Subfolders)
                .WithOne(x => x.Parent);

            // Ignore helpers
            modelBuilder.Entity<Folder>()
                .Ignore(x => x.SubfoldersWithExams);

            modelBuilder.Entity<Answer>()
                .Ignore(x => x.IsCorrect);

            modelBuilder.Entity<Folder>()
                .Ignore(x => x.IsExpanded);
        }
    }
}
