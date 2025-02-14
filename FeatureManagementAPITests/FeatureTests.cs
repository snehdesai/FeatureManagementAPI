using Microsoft.AspNetCore.Mvc;
using FeatureManagementAPI.Controllers;
using FeatureManagementAPI.Models;
using FeatureManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class FeaturesControllerTests
{
    private AppDbContext _context;
    private FeaturesController _controller;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "FeatureManagementAPI")  
            .Options;

        _context = new AppDbContext(options);

        _context.Features.AddRange(new List<Feature>
    {
        new Feature { Id = 1, Title = "Feature1", Description = "Description of Feature 1", EstimatedComplexity = "M", Status = "Active" },
        new Feature { Id = 2, Title = "Feature2", Description = "Description of Feature 2", EstimatedComplexity = "S", Status = "New" }
    });
        _context.SaveChanges();

        _controller = new FeaturesController(_context);
    }
    [Test]
    public async Task GetFeature_ValidId_ReturnsFeature()
    {
        // Act
        var result = await _controller.GetFeature(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        var feature = okResult.Value as Feature;
        Assert.AreEqual(1, feature.Id);
    }

    [Test]
    public async Task PutFeature_ValidFeature_ReturnsOk()
    {
        // Arrange
        var featureId = 1;
        var updatedFeature = new Feature
        {
            Id = featureId,
            Title = "Updated Feature",
            Description = "Updated description",
            EstimatedComplexity = "L",
            Status = "Active"
        };

        // Act
        var result = await _controller.PutFeature(1, updatedFeature);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var feature = okResult.Value as Feature;
        Assert.AreEqual("Updated Feature", feature.Title);
    }

    [Test]
    public async Task PutFeature_FeatureNotFound_ReturnsNotFound()
    {
        // Arrange
        var featureId = 1;
        var updatedFeature = new Feature
        {
            Id = featureId, 
            Title = "Updated Feature",
            Description = "Updated description",
            EstimatedComplexity = "L",
            Status = "Active"
        };

        // Act
        var result = await _controller.PutFeature(featureId, updatedFeature);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Feature not found", notFoundResult.Value?.ToString());
    }
    [Test]
    public async Task DeleteFeature_ValidId_ReturnsNoContent()
    {
        // Act
        var result = await _controller.DeleteFeature(1);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
    [Test]
    public async Task DeleteFeature_InvalidId_ReturnsNotFound()
    {
        // Act
        var result = await _controller.DeleteFeature(99); // Invalid ID

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}
