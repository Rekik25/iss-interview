using TodoApi.Models;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    IEnumerable<Todo> GetAll();
    Todo? GetById(int id);
    void Create(Todo todo);
    bool Update(Todo todo);
    bool Delete(int id);
}
