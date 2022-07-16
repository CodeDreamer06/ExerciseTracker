using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker;

internal static class SqliteDbContext
{
    public static readonly string FolderPath = Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData);

    public static readonly string FileName = "workouts.sqlite";

    public static readonly string ConnectionString = $"Data Source={FolderPath}\\{FileName};Version=3;";

    public static void CreateDb()
    {
        if (!File.Exists($@"{FolderPath}\{FileName}"))
            SQLiteConnection.CreateFile(FileName);
    }

    public static void CreateTable(Action<string> execute, string tableName)
    {
        try
        {
            using SQLiteConnection con = new(ConnectionString);
            con.Open();
            var cmd = new SQLiteCommand(@$"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'", con);

            if (cmd.ExecuteScalar() is null)
                execute(@$"CREATE TABLE {tableName}(
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Start DATETIME NULL,
                    End DATETIME NULL,
                    Duration INTEGER NULL,
                    Comments TEXT NULL);");
        }
        catch
        {
            Console.WriteLine("Unable to check if table exists.");
        }
    }
}

internal class CardioWorkoutContext : DbContext
{
    public DbSet<Workout>? CardioWorkouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder) + "\\workouts.sqlite";
        options.UseSqlite($"Data Source = {path}");
    }
}