using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todo = _service.GetById(id);
        return todo == null
            ? NotFound(new { message = "Todo not found" })
            : Ok(todo);
    }

    [HttpPost]
    public IActionResult Create(CreateTodoDto dto)
    {
        _service.Create(dto);
        return Ok(new { message = "Todo created successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateTodoDto dto)
    {
        return _service.Update(id, dto)
            ? Ok(new { message = "Todo updated successfully" })
            : NotFound(new { message = "Todo not found" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _service.Delete(id)
            ? Ok(new { message = "Todo deleted successfully" })
            : NotFound(new { message = "Todo not found" });
    }
}
