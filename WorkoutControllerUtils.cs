namespace ExerciseTracker;

partial class WorkoutController
{
    public IWorkoutRepository? WorkoutRepository;

    private static IWorkoutRepository GetRepository(RepositoryType repositoryType) => repositoryType switch
    {
        RepositoryType.Cardio => new CardioWorkoutRepository(),
        RepositoryType.Weights => new WeightsWorkoutRepository(),
        RepositoryType.Flexibility => new FlexibilityWorkoutRepository(),
        _ => throw new NotSupportedException()
    };

    private void SetRepositoryFromInput(string keyword = "show")
    {
        if (keyword == "show")
            Console.WriteLine($"Do you want to show cardio, weights or flexibility workouts?");
        else
            Console.WriteLine($"Do you want to {keyword} a cardio, weights or a flexibility workout?");

        while (true)
        {
            string? workoutType = Console.ReadLine()?.Trim();
            bool isWorkoutTypeValid = workoutType is "cardio" or "weights" or "flexibility";

            if (isWorkoutTypeValid)
            {
                var repositoryType = (RepositoryType) Enum.Parse(typeof(RepositoryType), workoutType!, true);
                WorkoutRepository = GetRepository(repositoryType);
                break;
            }

            else Console.WriteLine("Please select either a cardio, weights or a flexibility workout.");
        }
    }

    private Workout ReplaceEmptyFields(Workout log)
    {
        var workout = WorkoutRepository!.ReadUsingRelativeId(log.Id);
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
        log.SetDuration();
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

    private static Workout GetWorkoutFromUser(bool ignoreEmptyFields = false)
    {
        var log = new Workout();

        foreach (var property in log.GetType().GetProperties())
        {
            if (property.Name is "Id" or "Duration") continue;

            Console.Write(property.Name + ": ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                if (ignoreEmptyFields) continue;

                if (property.Name != "Comments")
                {
                    Console.WriteLine($"You cannot leave {property.Name} empty");
                    return new NullWorkout();
                }
            }

            var newValue = SetProperty(property, userInput);
            if (newValue is null) return new NullWorkout();
            property.SetValue(log, newValue);
        }

        return log.SetDuration();
    }
}
