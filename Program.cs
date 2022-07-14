namespace ExerciseTracker;

class Program
{
    static void Main()
    {
        Console.WriteLine(Helpers.MainMessage);

        while (true)
        {
            var rawCommand = Console.ReadLine()!;
            var command = rawCommand.ToLower().Trim();

            if (command is "exit" or "0") break;

            else if (command is "help") Console.WriteLine(Helpers.MainMessage);

            else if (string.IsNullOrWhiteSpace(command)) continue;

            else if (command.StartsWith("add")) 
                WorkoutService.Add(command);
            
            else if (command.StartsWith("show")) 
                WorkoutService.Show(command);

            else if (command.StartsWith("update"))
                WorkoutService.Update(rawCommand.GetNumber("update"));

            else if (command.StartsWith("remove"))
                WorkoutService.Remove(rawCommand.GetNumber("remove"));

            else
            {
                string suggestion = Helpers.CorrectSpelling(command);
                Console.WriteLine($"Not a command. Use 'help' if required. {suggestion}");
            }
        }
    }
}