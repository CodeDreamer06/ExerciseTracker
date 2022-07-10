namespace ExerciseTracker;

internal class WorkoutService
{
    private static WorkoutController _controller = new();

    internal static void Add(string command)
    {
        var log = new Workout();
        var workoutType = command.RemoveKeyword("add");

        if (workoutType is not "cardio" and not "weights")
        {
            Console.WriteLine("Please select either a cardio or a weights workout.");
            return;
        }

        foreach (var property in log.GetType().GetProperties())
        {
            if (property.Name == "Id") continue;

            if (property.Name == "Duration")
            {
                if (log.IsTimeInvalid())
                {
                    Console.WriteLine("You cannot start a workout after it ends.");
                    return;
                }

                log.SetDuration();
                continue;
            }

            Console.Write(property.Name + ": ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput) && property.Name != "Comments")
            {
                Console.WriteLine($"You cannot leave {property.Name} empty");
                return;
            }

            var newValue = SetProperty(property, userInput);
            if (newValue is null) return;
            property.SetValue(log, newValue);
        }

        _controller.SetRepository(workoutType is "cardio");
        _controller.Create(log);
    }

    internal static void Show(string command)
    {
        var workoutType = command.RemoveKeyword("show", Helpers.ShowFormatError);

        if (workoutType is not "cardio" and not "weights")
        {
            Console.WriteLine("Please select either a cardio or a weights workout.");
            return;
        }

        _controller.SetRepository(workoutType is "cardio");

        var logs = _controller.Read().ConvertAll(log => log.GetDeepClone());
        
        for (int i = 0; i < logs.Count; i++) logs[i].Id = i + 1;

        Helpers.DisplayTable(logs, Helpers.NoLogsMessage);
    }

    internal static void Update(int relativeId)
    {
        SetRepositoryFromInput("update");

        if (relativeId == 0)
        {
            Console.WriteLine("Please enter a valid id from the table.");
            return;
        }

        if (_controller.GetNumberOfWorkouts() <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for no longer exists.");
            return;
        }

        var log = _controller!.ReadUsingId(relativeId);

        Console.WriteLine("Leave the field empty if you don't want to change the value.");
        
        foreach (var property in log.GetType().GetProperties())
        {
            if (property.Name == "Id") continue;

            if (property.Name == "Duration")
            {
                if (log.IsTimeInvalid())
                {
                    Console.WriteLine("You cannot start a workout after it ends.");
                    return;
                }

                log.SetDuration();
                continue;
            }

            Console.Write(property.Name + ": ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput)) continue;

            var newValue = SetProperty(property, userInput);
            if (newValue is null) return;
            property.SetValue(log, newValue);

            log.Id = relativeId;

            _controller!.Update(ReplaceEmptyFields(log));
        }
    }

    internal static void Remove(int relativeId)
    {
        SetRepositoryFromInput("remove");
        var workouts = _controller!.Read();

        if (workouts.Count <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for no longer exists.");
            return;
        }

        int absoluteId = workouts[relativeId - 1].Id;

        _controller!.Delete(absoluteId);
    }

    private static void SetRepositoryFromInput(string keyword)
    {
        string? workoutType;

        Console.WriteLine($"Do you want to {keyword} a cardio or a weights workout?");

        while (true)
        {
            workoutType = Console.ReadLine()?.Trim();
            bool isWorkoutTypeValid = workoutType is "cardio" or "weights";

            if (isWorkoutTypeValid) break;

            else Console.WriteLine("Please select either a cardio or a weights workout.");

        }

        _controller.SetRepository(workoutType is "cardio");
    }

    internal static Workout ReplaceEmptyFields(Workout log)
    {
        var workout = _controller!.ReadUsingId(log.Id);
        int id = workout.Id;

        try
        {
            foreach (var property in from property in log.GetType().GetProperties()
                                     where property.GetValue(log) is null
                                     select property)
            {
                property.SetValue(log, property.GetValue(workout));
            }
        }

        catch
        {
            Console.WriteLine("Failed to update log");
            return new Workout();
        }

        log.Id = id;
        return log;
    }

    private static object? SetProperty(System.Reflection.PropertyInfo property,
                                       string? userInput)
    {
        try
        {
            var propertyType = Nullable.GetUnderlyingType(property.PropertyType)!;
            return (property.PropertyType == typeof(string)) ?
                userInput : Convert.ChangeType(userInput, propertyType);
        }

        catch (FormatException)
        {
            Console.WriteLine($"The value you entered is invalid for {property.Name}.");

            if (property.Name is "Start" or "End")
                Console.WriteLine(Helpers.DateTimeFormat);

            return null;
        }
    }
}
