using System.Globalization;
using ConsoleTableExt;

namespace ExerciseTracker;

internal static class Helpers
{
    public static string MainMessage = @"
# Exercise Tracker
  A simple exercise logger for you to measure your progress!
* exit or 0: stop the program
* show: display existing logs
* add: create an exercise log
* update [id]: change an existing log
* remove [optional: id]: delete the last recent log
Use help to display this message again!
";

    public static string NoLogsMessage = "There are no logs to display. Type 'help' to learn how to log a new workout.";

    public static string DateTimeFormat = @"The recommended forms for dateTime are 'MM/DD/YYYY HH/MM/SS' and 'WeekDay, day monthName year HH:MM:SS'.
Examples: 08/18/2022 07:22:16 or Sat, 18 Aug 2022 07:22:16";

    private static readonly Dictionary<HeaderCharMapPositions, char> HeaderCharacterMap = new()
    {
        { HeaderCharMapPositions.TopLeft, '╒' },
        { HeaderCharMapPositions.TopCenter, '╤' },
        { HeaderCharMapPositions.TopRight, '╕' },
        { HeaderCharMapPositions.BottomLeft, '╞' },
        { HeaderCharMapPositions.BottomCenter, '╪' },
        { HeaderCharMapPositions.BottomRight, '╡' },
        { HeaderCharMapPositions.BorderTop, '═' },
        { HeaderCharMapPositions.BorderRight, '│' },
        { HeaderCharMapPositions.BorderBottom, '═' },
        { HeaderCharMapPositions.BorderLeft, '│' },
        { HeaderCharMapPositions.Divider, '│' },
    };

    public static void DisplayTable<T>(List<T> records, string emptyMessage) where T : class
    {
        if (records.Count == 0)
        {
            Console.WriteLine(emptyMessage);
            return;
        }

        ConsoleTableBuilder.From(records)
            .WithCharMapDefinition(CharMapDefinition.FramePipDefinition, HeaderCharacterMap)
            .ExportAndWriteLine();
    }

    public static string CorrectSpelling(string command)
    {
        var definitions = new List<string> { "exit", "help", "show", "add", "update", "remove" };
        string correctDefinition = "";
        int maxPercentage = 0;

        try
        {
            foreach (var definition in definitions)
            {
                int matchPercentage = FuzzySharp.Fuzz.Ratio(command.Split()[0], definition);

                if (matchPercentage > maxPercentage)
                {
                    maxPercentage = matchPercentage;
                    correctDefinition = definition;
                }
            }

            return maxPercentage > 40 ? $"Did you mean {correctDefinition}?" : "";
        }

        catch
        {
            return "";
        }
    }
}

public static class ExtensionMethods
{
    public static int GetNumber(this string str, string input)
    {
        _ = int.TryParse(
            str.Replace(input, ""),
            NumberStyles.Any,
            NumberFormatInfo.InvariantInfo,
            out int number);

        return number;
    }
}
