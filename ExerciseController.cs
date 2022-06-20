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

    internal static void Create(Exercise log)
    {
        using var db = new ExerciseContext();
        db.Database.EnsureCreated();
        db.Add(log);
        db.SaveChanges();
    }

    internal static List<Exercise> Read()
    {
        using var db = new ExerciseContext();
        db.Database.EnsureCreated();
        return db.Exercises!.ToList();
    }

    internal static void Update(Exercise log)
    {
        using var db = new ExerciseContext();
        db.Database.EnsureCreated();
        var exercise = db.Exercises!.Where(e => e.Id == log.Id).First();
        exercise = log;
        db.SaveChanges();
    }

    internal static void Delete(int id)
    {
        using var db = new ExerciseContext();
        db.Database.EnsureCreated();
        var exercise = db.Exercises!.Where(e => e.Id == id).First();
        db.Remove(exercise);
        db.SaveChanges();
    }
}
