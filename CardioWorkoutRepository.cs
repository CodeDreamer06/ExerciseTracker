namespace ExerciseTracker;

internal class CardioWorkoutRepository : IWorkoutRepository, IDisposable
{
    private readonly CardioWorkoutContext context;

    public CardioWorkoutRepository()
    {
        context = new CardioWorkoutContext();
        context.Database.EnsureCreated();
    }

    public List<Workout> GetWorkouts() => context.CardioWorkouts!.ToList();

    public Workout GetWorkoutByRelativeId(int id) => 
        context.CardioWorkouts!.ToList()[id - 1];

    public int GetNumberOfWorkouts() => context.CardioWorkouts!.Count();

    public void InsertWorkout(Workout log)
    {
        context.CardioWorkouts!.Add(log);
        context.SaveChanges();
    }

    public void UpdateWorkout(Workout log)
    {
        var workout = context.CardioWorkouts?.SingleOrDefault(l => l.Id == log.Id);
        if (workout is null) return;

        workout = log;
        context.SaveChanges();
    }

    public void DeleteWorkout(int id)
    {
        var log = context.CardioWorkouts!.Find(id);
        context.CardioWorkouts!.Remove(log!);
        context.SaveChanges();
    }

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
