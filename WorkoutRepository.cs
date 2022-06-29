using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker;

internal class WorkoutRepository : IWorkoutRepository, IDisposable
{
    private WorkoutContext context;

    public WorkoutRepository(WorkoutContext context)
    {
        this.context = context;
        context.Database.EnsureCreated();
    }

    public List<Workout> GetWorkouts() => context.Workouts!.ToList();

    public Workout GetWorkoutById(int id) => context.Workouts!.Find(id)!;

    public void InsertWorkout(Workout log) => context.Workouts!.Add(log);

    public void UpdateWorkout(Workout log) => 
        context.Entry(log).State = EntityState.Modified;

    public void DeleteWorkout(int id)
    {
        var log = context.Workouts!.Find(id);
        context.Workouts!.Remove(log!);
    }

    public void Save() => context.SaveChanges();

    private bool disposed = false;

    protected virtual void Dispose(bool isDisposing)
    {
        if (!disposed && isDisposing) context.Dispose();
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
