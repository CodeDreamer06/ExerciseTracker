namespace ExerciseTracker;

internal class WorkoutController
{
    private IWorkoutRepository _workoutRepository;

    public WorkoutController()
    {
        _workoutRepository = new WorkoutRepository(new WorkoutContext());
    }

    public void Create(Workout log)
    {
        _workoutRepository.InsertWorkout(log);
        _workoutRepository.Save();
        Console.WriteLine("Successfully logged your workout!");
    }

    public List<Workout> Read() => _workoutRepository.GetWorkouts();

    public Workout ReadUsingId(int id) => _workoutRepository.GetWorkoutById(id);

    public void Update(Workout log)
    {
        _workoutRepository.UpdateWorkout(log);
        _workoutRepository.Save();
        Console.WriteLine("Successfully updated your workout!");
    }

    public void Delete(int id)
    {
        _workoutRepository.DeleteWorkout(id);
        _workoutRepository.Save();
        Console.WriteLine("Successfully removed your workout!");
    }

    protected void Dispose() => _workoutRepository.Dispose();
}
