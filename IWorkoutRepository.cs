namespace ExerciseTracker;

internal interface IWorkoutRepository : IDisposable
{
    List<Workout> GetWorkouts();
    Workout GetWorkoutByRelativeId(int id);
    int GetNumberOfWorkouts();
    void InsertWorkout(Workout log);
    void UpdateWorkout(Workout log);
    void DeleteWorkout(int id);
}
