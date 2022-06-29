using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker;

public class Workout
{
    public int Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Comments { get; set; }
    public bool IsTimeInvalid() => Start > End;
    public void SetDuration()
    {
        if (Start != null && End != null) 
            Duration = End - Start;
    }

    public Workout GetDeepClone() => new() { 
        Id = Id, Start = Start, End = End, Duration = Duration, Comments = Comments 
    };
}

internal class WorkoutContext : DbContext
{
    public DbSet<Workout>? Workouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder) + "/exerciseTracker.sqlite";
        options.UseSqlite($"Data Source = {path}");
    }
}