namespace ExerciseTracker;

class Program
{
    static void Main(string[] args)
    {
        var command = "";
        var service = new ExerciseService();
        const string help = @"
# Exercise Tracker
  A simple exercise logger for you to measure your progress!
* exit or 0: stop the program
* show: display existing logs
* add: create an exercise log
* update [id]: change an existing log
* remove [optional: id]: delete the last recent log
Use help to display this message again!
";

        while (true)
        {
            command = Console.ReadLine()!.ToLower();

            if (command is "exit" or "0") break;

            else if (command is "help") Console.WriteLine(help);

            else if (string.IsNullOrWhiteSpace(command)) continue;

            else if (command.StartsWith("add")) service.Add();

            else if (command.StartsWith("show")) service.Show();

            else if (command.StartsWith("update")) service.Update(0);

            else if (command.StartsWith("remove")) service.Remove(0);

            else Console.WriteLine("Not a command. Use 'help' if required. ");
        }
    }
}