namespace ExerciseTracker;

internal class WorkoutController
{
    public IWorkoutRepository? _workoutRepository;

    public void SetRepository(bool isCardioWorkout)
    {
        _workoutRepository = isCardioWorkout ?
            new CardioWorkoutRepository()
            : new WeightsWorkoutRepository();
    }

    public void Create(Workout log)
    {
        _workoutRepository!.InsertWorkout(log);
        Console.WriteLine("Successfully logged your workout!");
    }

    public List<Workout> Read() => _workoutRepository!.GetWorkouts();

    public Workout ReadUsingId(int id) => _workoutRepository!.GetWorkoutByRelativeId(id);

    public void Update(Workout log)
    {
        _workoutRepository!.UpdateWorkout(log);
        Console.WriteLine("Successfully updated your workout!");
    }

    public void Delete(int id)
    {
        _workoutRepository!.DeleteWorkout(id);
        Console.WriteLine("Successfully removed your workout!");
    }

    protected void Dispose() => _workoutRepository!.Dispose();
}
