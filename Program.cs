namespace ExerciseTracker;

class Program
{
    static void Main(string[] args)
    {
        var service = new WorkoutService();
        Console.WriteLine(Helpers.MainMessage);

        while (true)
        {
            var rawCommand = Console.ReadLine()!;
            var command = rawCommand.ToLower().Trim();

            if (command is "exit" or "0") break;

            else if (command is "help") Console.WriteLine(Helpers.MainMessage);

            else if (string.IsNullOrWhiteSpace(command)) continue;

            else if (command.StartsWith("add")) service.Add();
            
            else if (command.StartsWith("show")) service.Show();

            else if (command.StartsWith("update")) 
                service.Update(rawCommand.GetNumber("update"));

            else if (command.StartsWith("remove")) 
                service.Remove(rawCommand.GetNumber("remove"));

            else
            {
                string suggestion = Helpers.CorrectSpelling(command);
                Console.WriteLine($"Not a command. Use 'help' if required. {suggestion}");
            }
        }
    }
}