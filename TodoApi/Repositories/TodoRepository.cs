using Microsoft.Data.Sqlite;
using TodoApi.Models;
using Microsoft.AspNetCore.Hosting;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(IWebHostEnvironment env)
    {
        var dbPath = Path.Combine(env.ContentRootPath, "todos.db");
        _connectionString = $"Data Source={dbPath}";
    }

    public IEnumerable<Todo> GetAll()
    {
        var todos = new List<Todo>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Todos";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(ReadTodo(reader));
        }

        return todos;
    }

    public Todo? GetById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Todos WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        using var reader = command.ExecuteReader();
        return reader.Read() ? ReadTodo(reader) : null;
    }

    public void Create(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Todos (Title, Description, IsCompleted, CreatedAt)
            VALUES ($title, $description, $isCompleted, $createdAt)
        """;

        command.Parameters.AddWithValue("$title", todo.Title);
        command.Parameters.AddWithValue("$description", todo.Description);
        command.Parameters.AddWithValue("$isCompleted", todo.IsCompleted ? 1 : 0);
        command.Parameters.AddWithValue("$createdAt", todo.CreatedAt.ToString("o"));

        command.ExecuteNonQuery();
    }

    public bool Update(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE Todos
            SET Title = $title,
                Description = $description,
                IsCompleted = $isCompleted
            WHERE Id = $id
        """;

        command.Parameters.AddWithValue("$id", todo.Id);
        command.Parameters.AddWithValue("$title", todo.Title);
        command.Parameters.AddWithValue("$description", todo.Description);
        command.Parameters.AddWithValue("$isCompleted", todo.IsCompleted ? 1 : 0);

        return command.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Todos WHERE Id = $id";
        command.Parameters.AddWithValue("$id", id);

        return command.ExecuteNonQuery() > 0;
    }

    private static Todo ReadTodo(SqliteDataReader reader)
    {
        return new Todo
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
            IsCompleted = reader.GetInt32(3) == 1,
            CreatedAt = DateTime.Parse(reader.GetString(4))
        };
    }
}
