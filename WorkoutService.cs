namespace ExerciseTracker;

internal class WorkoutService
{
    WorkoutController controller = new WorkoutController();

    internal void Add()
    {
        var log = new Workout();

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
            if (newValue == null) return;
            property.SetValue(log, newValue);
        }

        controller.Create(log);
    }

    internal void Show()
    {
        var logs = controller.Read().ConvertAll(log => log.GetDeepClone());
        
        for (int i = 0; i < logs.Count; i++) logs[i].Id = i + 1;

        Helpers.DisplayTable(logs, Helpers.NoLogsMessage);
    }

    internal void Update(int relativeId)
    {
        var workouts = controller.Read();

        if (relativeId == 0)
        {
            Console.WriteLine("Please enter a valid id from the table.");
            return;
        }

        if (workouts.Count <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for no longer exists.");
            return;
        }

        var log = controller.ReadUsingId(workouts[relativeId - 1].Id);

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
            if (newValue == null) return;
            property.SetValue(log, newValue);

            controller.Update(ReplaceEmptyFields(log));
        }
    }

    internal void Remove(int relativeId)
    {
        var workouts = controller.Read();

        if (workouts.Count <= relativeId - 1)
        {
            Console.WriteLine("The workout you are looking for no longer exists.");
            return;
        }

        int absoluteId = relativeId == 0 ? workouts.Last().Id : workouts[relativeId - 1].Id;
        
        controller.Delete(absoluteId);
    }

    internal Workout ReplaceEmptyFields(Workout log)
    {
        var workout = controller.ReadUsingId(log.Id);

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
            Console.WriteLine("The value you entered is invalid for " + property.Name);

            if (property.Name is "Start" or "End")
                Console.WriteLine(Helpers.DateTimeFormat);

            return null;
        }
    }
}
