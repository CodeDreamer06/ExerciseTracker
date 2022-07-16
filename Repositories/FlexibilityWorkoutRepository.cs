using System.Data.SQLite;
using Dapper;

namespace ExerciseTracker;

internal class FlexibilityWorkoutRepository : IWorkoutRepository, IDisposable
{
    public FlexibilityWorkoutRepository()
    {
        SqliteDbContext.CreateDb();
        SqliteDbContext.CreateTable(delegate (string query) { 
            Execute(query);
        }, "Flexibility");
    }

    protected static void Execute(string query, object? parameters = default)
    {
        try
        {
            using SQLiteConnection con = new(SqliteDbContext.ConnectionString);
            con.Open();
            con.Execute(query, parameters);
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
        var workouts = con.Query<WorkoutToDbDTO>("SELECT Id, datetime(start) AS Start, datetime(end) AS End, Duration AS DurationInSeconds, Comments FROM Flexibility;").ToList();
        return workouts.ConvertAll(log => log.ConvertToWorkout());
    }

    public Workout ReadUsingRelativeId(int id) => Read()[id - 1];

    public int GetCount()
    {
        using SQLiteConnection con = new(SqliteDbContext.ConnectionString);
        con.Open();
        return con.ExecuteScalar<int>("SELECT COUNT(*) from Flexibility;");
    }

    public void Create(Workout log) => 
        Execute("INSERT INTO Flexibility(start, end, duration, comments) values(@Start, @End, @DurationInSeconds, @Comments)", new WorkoutToDbDTO(log));

    public void Update(Workout log) => 
        Execute($"UPDATE Flexibility SET Start = @Start, End = @End, Duration = @DurationInSeconds, Comments = @Comments where id = {log.Id}", new WorkoutToDbDTO(log));

    public void Delete(int id) => 
        Execute("DELETE FROM Flexibility WHERE id = @id", new { id });


    public void Dispose() => GC.SuppressFinalize(this);
}
