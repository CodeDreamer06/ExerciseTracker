namespace ExerciseTracker;

class Program
{
    static void Main()
    {
        var service = new WorkoutController();
        Console.WriteLine(Helpers.MainMessage);

        while (true)
        {
            var rawCommand = Console.ReadLine()!;
            var command = rawCommand.ToLower().Trim();

            if (command is "exit" or "0") break;

            else if (command is "help") 
                Console.WriteLine(Helpers.MainMessage);

            else if (string.IsNullOrWhiteSpace(command)) continue;

            else if (command is "add") 
                service.Add();
            
            else if (command is "show") 
                service.Show();

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