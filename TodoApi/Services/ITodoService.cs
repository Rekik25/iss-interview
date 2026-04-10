using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    IEnumerable<Todo> GetAll();
    Todo? GetById(int id);
    void Create(CreateTodoDto dto);
    bool Update(int id, UpdateTodoDto dto);
    bool Delete(int id);
}
