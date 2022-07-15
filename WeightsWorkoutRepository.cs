using System.Data.SQLite;

namespace ExerciseTracker;

internal class WeightsWorkoutRepository : IWorkoutRepository, IDisposable
{
    private static string FolderPath = Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData);

    private static string FileName = "workouts.sqlite";

    private static string ConnectionString = $"Data Source={FolderPath}/{FileName};Version=3;";

    public WeightsWorkoutRepository()
    {
        if(!File.Exists($@"{FolderPath}/{FileName}"))
            SQLiteConnection.CreateFile(FileName);

        try
        {
            using SQLiteConnection con = new(ConnectionString);
            con.Open();
            var cmd = new SQLiteCommand(@"SELECT name FROM sqlite_master WHERE type='table' AND name='Weights'", con);
            if (cmd.ExecuteScalar() == null)
                Execute(@"CREATE TABLE Weights(
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

    protected static void Execute(string query)
    {
        try
        {
            using SQLiteConnection con = new(ConnectionString);
            con.Open();
            using var cmd = new SQLiteCommand(query, con);
            cmd.ExecuteNonQuery();
        }

        catch
        {
            Console.WriteLine("An unknown error occured.");
            throw;
        }
    }

    public List<Workout> GetWorkouts()
    {
        using SQLiteConnection con = new(ConnectionString);
        con.Open();
        using var cmd = new SQLiteCommand(Helpers.SqlRead, con);
        var reader = cmd.ExecuteReader();

        var workouts = new List<Workout>();
        while (reader.Read())
        {
            workouts.Add(new Workout {
                Id = reader.GetInt32(0),
                Start = DateTime.Parse((string)reader.GetValue(1)),
                End = DateTime.Parse((string)reader.GetValue(2)), 
                Duration = TimeSpan.FromSeconds((long)reader.GetValue(3)),
                Comments = reader.GetString(4)
            });
        }

        return workouts;
    }

    public Workout GetWorkoutByRelativeId(int id) => GetWorkouts()[id - 1];

    public int GetNumberOfWorkouts()
    {
        using SQLiteConnection con = new(ConnectionString);
        con.Open();
        using var cmd = new SQLiteCommand("SELECT COUNT(*) from Weights;", con);
        var reader = cmd.ExecuteReader();
        reader.Read();

        return reader.GetInt32(0);
    }

    public void InsertWorkout(Workout log)
    {
        var timeStart = log.Start?.ToString("yyyy-MM-dd HH:mm:ss");
        var timeEnd = log.End?.ToString("yyyy-MM-dd HH:mm:ss");
        var duration = log.Duration?.TotalSeconds;

        Execute(string.Format(
            Helpers.SqlInsert, timeStart, timeEnd, duration, log.Comments));
    }

    public void UpdateWorkout(Workout log)
    {
        var timeStart = log.Start?.ToString("yyyy-MM-dd HH:mm:ss");
        var timeEnd = log.End?.ToString("yyyy-MM-dd HH:mm:ss");
        var duration = log.Duration?.TotalSeconds;

        Execute(string.Format(Helpers.SqlUpdate,
            $"Start = '{timeStart}', End = '{timeEnd}', Duration = '{duration}', Comments = '{log.Comments}'", log.Id));
    }

    public void DeleteWorkout(int id) => Execute(string.Format(Helpers.SqlDelete, id));

    public void Dispose() => GC.SuppressFinalize(this);
}
