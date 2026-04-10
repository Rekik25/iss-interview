using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Todo> GetAll() => _repository.GetAll();

    public Todo? GetById(int id) => _repository.GetById(id);

    public void Create(CreateTodoDto dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description ?? "",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _repository.Create(todo);
    }

    public bool Update(int id, UpdateTodoDto dto)
    {
        var todo = new Todo
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description ?? "",
            IsCompleted = dto.IsCompleted
        };

        return _repository.Update(todo);
    }

    public bool Delete(int id) => _repository.Delete(id);
}
