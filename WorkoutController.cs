namespace ExerciseTracker;

public enum RepositoryType { Cardio, Weights, Flexibility };

partial class WorkoutController
{
    internal void Add()
    {
        SetRepositoryFromInput(keyword: "add");

        var log = GetWorkoutFromUser();
        if (log is NullWorkout) return;

        WorkoutRepository!.Create(log);
        Console.WriteLine("Successfully logged your workout!");
    }

    internal void Show()
    {
        SetRepositoryFromInput();

        var logs = WorkoutRepository!.Read().ConvertAll(log => log.GetDeepClone());

        for (int i = 0; i < logs.Count; i++) logs[i].Id = i + 1;

        Helpers.DisplayTable(logs, Helpers.NoLogsMessage);
    }

    internal void Update(int relativeId)
    {
        if (relativeId is 0)
        {
            Console.WriteLine("Please enter a valid id from the table.");
            return;
        }

        SetRepositoryFromInput(keyword: "update");

        if (WorkoutRepository!.GetCount() <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for no longer exists.");
            return;
        }

        Console.WriteLine("Leave the field empty if you don't want to change the value.");

        var log = GetWorkoutFromUser(ignoreEmptyFields: true);
        if (log is NullWorkout) return;

        log.Id = relativeId;

        WorkoutRepository!.Update(ReplaceEmptyFields(log));
        Console.WriteLine("Successfully updated your workout!");
    }

    internal void Remove(int relativeId)
    {
        SetRepositoryFromInput(keyword: "remove");

        var workouts = WorkoutRepository!.Read();

        if (workouts.Count <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for doesn't exist.");
            return;
        }

        int absoluteId = workouts[relativeId - 1].Id;

        WorkoutRepository!.Delete(absoluteId);
        Console.WriteLine("Successfully removed your workout!");
    }
}
