namespace ExerciseTracker;

internal class CardioWorkoutRepository : IWorkoutRepository, IDisposable
{
    private readonly CardioWorkoutContext context;

    public CardioWorkoutRepository()
    {
        context = new CardioWorkoutContext();
        context.Database.EnsureCreated();
    }

    public List<Workout> Read() => context.CardioWorkouts!.ToList();

    public Workout ReadUsingRelativeId(int id) => 
        context.CardioWorkouts!.ToList()[id - 1];

    public int GetCount() => context.CardioWorkouts!.Count();

    public void Create(Workout log)
    {
        context.CardioWorkouts!.Add(log);
        context.SaveChanges();
    }

    public void Update(Workout log)
    {
        var workout = context.CardioWorkouts?.SingleOrDefault(l => l.Id == log.Id);
        if (workout is null) return;

        context.Entry(workout).CurrentValues.SetValues(log);
        context.SaveChanges();
    }

    public void Delete(int id)
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
