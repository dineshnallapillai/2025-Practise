using DiagnosticApi.Controllers;
using DiagnosticApi.Model;
using DiagnosticApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DiagnosticApiTests;

public class PatientsControllerTests
{
    private readonly Mock<IPatientService> _mockService;
    private readonly PatientsController _controller;

    public PatientsControllerTests()
    {
        _mockService = new Mock<IPatientService>();
        _controller = new PatientsController(_mockService.Object);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenPatientExists()
    {
        // Arrange
        var expectedPatient = new Patient { Id = 1, Name = "John", Age = 40 };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(expectedPatient);

        // Act
        var result = await _controller.Get(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result); 
        var actualPatient = Assert.IsType<Patient>(okResult.Value); 
        Assert.Equal(expectedPatient.Id, actualPatient.Id); 
        Assert.Equal(expectedPatient.Name, actualPatient.Name); 
        Assert.Equal(expectedPatient.Age, actualPatient.Age); 
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenPatientDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(99))!.ReturnsAsync((Patient?)null);

        // Act
        var result = await _controller.Get(99);

        // Assert
        Assert.IsType<NotFoundResult>(result); 
    }
}