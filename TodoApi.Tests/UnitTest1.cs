using Xunit;
using Moq;
using TodoApi.Services;
using TodoApi.Repositories;
using TodoApi.DTOs;
using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Tests;

public class UnitTest1
{
    private readonly Mock<ITodoRepository> _repoMock;
    private readonly TodoService _service;

    public UnitTest1()
    {
        _repoMock = new Mock<ITodoRepository>();
        _service = new TodoService(_repoMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsTodos_WhenDataExists()
    {
        _repoMock.Setup(r => r.GetAll())
            .Returns(new List<Todo>
            {
                new Todo { Id = 1, Title = "Test" }
            });

        var result = _service.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public void GetAll_ReturnsEmpty_WhenNoData()
    {
        _repoMock.Setup(r => r.GetAll())
            .Returns(new List<Todo>());

        var result = _service.GetAll();

        Assert.Empty(result);
    }

    [Fact]
    public void Create_CallsRepositoryCreate()
    {
        var dto = new CreateTodoDto
        {
            Title = "New Todo",
            Description = "Desc"
        };

        _service.Create(dto);

        _repoMock.Verify(r => r.Create(It.IsAny<Todo>()), Times.Once);
    }

    [Fact]
    public void Update_ReturnsTrue_WhenTodoExists()
    {
        _repoMock.Setup(r => r.Update(It.IsAny<Todo>()))
            .Returns(true);

        var dto = new UpdateTodoDto
        {
            Title = "Updated",
            Description = "Updated",
            IsCompleted = true
        };

        var result = _service.Update(1, dto);

        Assert.True(result);
    }

    [Fact]
    public void Update_ReturnsFalse_WhenTodoDoesNotExist()
    {
        _repoMock.Setup(r => r.Update(It.IsAny<Todo>()))
            .Returns(false);

        var dto = new UpdateTodoDto
        {
            Title = "Updated",
            Description = "Updated",
            IsCompleted = true
        };

        var result = _service.Update(99, dto);

        Assert.False(result);
    }

    [Fact]
    public void Delete_ReturnsTrue_WhenTodoExists()
    {
        _repoMock.Setup(r => r.Delete(1))
            .Returns(true);

        var result = _service.Delete(1);

        Assert.True(result);
    }

    [Fact]
    public void Delete_ReturnsFalse_WhenTodoDoesNotExist()
    {
        _repoMock.Setup(r => r.Delete(99))
            .Returns(false);

        var result = _service.Delete(99);

        Assert.False(result);
    }
}
