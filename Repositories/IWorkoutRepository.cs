namespace ExerciseTracker;

internal interface IWorkoutRepository : IDisposable
{
    int GetCount();
    void Create(Workout log);
    List<Workout> Read();
    Workout ReadUsingRelativeId(int id);
    void Update(Workout log);
    void Delete(int id);
}
