using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker;

internal static class ExerciseController
{
    internal class ExerciseContext : DbContext
    {
        public DbSet<Exercise>? Exercises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            options.UseSqlite($"Data Source = {path}");
        }
    }
}
