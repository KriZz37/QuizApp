using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.Entities;

namespace QuizApp.Data
{
    /// <summary>
    /// Database context, DbSets represent database tables,
    /// they can extract information from the database e.g. with LINQ.
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// Configures created entities.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Folder tree
            modelBuilder.Entity<Folder>()
                .HasMany(x => x.Subfolders)
                .WithOne(x => x.Parent);

            // Ignore helpers
            modelBuilder.Entity<Folder>()
                .Ignore(x => x.SubfoldersWithQuizzes);

            modelBuilder.Entity<Answer>()
                .Ignore(x => x.IsCorrect);

            modelBuilder.Entity<Folder>()
                .Ignore(x => x.IsExpanded);
        }
    }
}
