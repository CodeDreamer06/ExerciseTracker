namespace ExerciseTracker;

public class Workout
{
    public int Id { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Comments { get; set; }

    public Workout SetDuration()
    {
        if (Start > End)
        {
            Console.WriteLine("You cannot start a workout after it ends.");
            return new NullWorkout();
        }

        if (Start != null && End != null)
            Duration = End - Start;

        return this;
    }

    public Workout GetDeepClone() => new() { 
        Id = Id, Start = Start, End = End, Duration = Duration, Comments = Comments 
    };
}

public class NullWorkout : Workout { 
    new public int Id = -1; 
}

public class WorkoutToDbDTO
{
    public int Id { get; set; }

    public string? Start { get; set; }

    public string? End { get; set; }

    public double? DurationInSeconds { get; set; }

    public string? Comments { get; set; }

    private WorkoutToDbDTO() { }

    public WorkoutToDbDTO(Workout log)
    {
        Start = log.Start?.ToString("yyyy-MM-dd HH:mm:ss");
        End = log.End?.ToString("yyyy-MM-dd HH:mm:ss");
        DurationInSeconds = log.Duration?.TotalSeconds;
        Comments = log.Comments;
    }

    public Workout ConvertToWorkout() => new()
    {
        Id = Id,
        Start = DateTime.Parse(Start!),
        End = DateTime.Parse(End!),
        Duration = TimeSpan.FromSeconds(DurationInSeconds!.Value),
        Comments = Comments
    };

    public void Deconstruct(out string? start, out string? end, out double? durationInSeconds, out string? comments)
    {
        start = Start;
        end = End;
        durationInSeconds = DurationInSeconds;
        comments = Comments;
    }
}