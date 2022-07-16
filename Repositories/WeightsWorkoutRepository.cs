using System.Data.SQLite;

namespace ExerciseTracker;

internal class WeightsWorkoutRepository : IWorkoutRepository, IDisposable
{
    public WeightsWorkoutRepository()
    {
        SqliteDbContext.CreateDb();
        SqliteDbContext.CreateTable(Execute, "Weights");
    }

    protected static void Execute(string query)
    {
        try
        {
            using SQLiteConnection con = new(SqliteDbContext.ConnectionString);
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

    public List<Workout> Read()
    {
        using SQLiteConnection con = new(SqliteDbContext.ConnectionString);
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

    public Workout ReadUsingRelativeId(int id) => Read()[id - 1];

    public int GetCount()
    {
        using SQLiteConnection con = new(SqliteDbContext.ConnectionString);
        con.Open();
        using var cmd = new SQLiteCommand("SELECT COUNT(*) from Weights;", con);
        var reader = cmd.ExecuteReader();
        reader.Read();

        return reader.GetInt32(0);
    }

    public void Create(Workout log)
    {
        var (start, end, duration, comments) = new WorkoutToDbDTO(log);
        Execute(string.Format(
            Helpers.SqlInsert, start, end, duration, comments));
    }

    public void Update(Workout log)
    {
        var (start, end, duration, comments) = new WorkoutToDbDTO(log);
        Execute(string.Format(Helpers.SqlUpdate,
            $"Start = '{start}', End = '{end}', Duration = '{duration}', Comments = '{comments}'", log.Id));
    }

    public void Delete(int id) => Execute(string.Format(Helpers.SqlDelete, id));

    public void Dispose() => GC.SuppressFinalize(this);
}
