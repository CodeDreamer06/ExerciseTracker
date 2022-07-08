namespace ExerciseTracker;

internal interface IWorkoutRepository : IDisposable
{
    List<Workout> GetWorkouts();
    Workout GetWorkoutByRelativeId(int id);
    void InsertWorkout(Workout log);
    void UpdateWorkout(Workout log);
    void DeleteWorkout(int id);
}
