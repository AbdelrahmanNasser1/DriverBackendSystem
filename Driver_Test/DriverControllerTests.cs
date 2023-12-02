using Driver_WebAPI.DTOs;
using FluentValidation.Results;

namespace Driver_Test;

public class DriverControllerTests
{
    [Fact]
    public void Get_ReturnsListOfDrivers()
    {
        // Arrange
        var mockDriverService = new Mock<IDriverService>();
        mockDriverService.Setup(service => service.GetDrivers())
                        .Returns(new List<Driver>
                        {
                            new Driver { Id = "1", FirstName = "Driver First Name 1" ,LastName= "Driver Last Name 1" , Email="driverone@example.com" , PhoneNumber = "212334231" },
                            new Driver { Id = "2", FirstName = "Driver First Name 2" ,LastName= "Driver Last Name 2" , Email="drivertwo@example.com" , PhoneNumber = "233433532" },
                        });

        var controller = new DriversController(mockDriverService.Object, null);

        // Act
        var result = controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var drivers = Assert.IsAssignableFrom<IEnumerable<Driver>>(okResult.Value);
        Assert.Equal(2, drivers.Count());
    }

    [Fact]
    public void GetSortedDrivers_ReturnsSortedDrivers()
    {
        // Arrange
        var mockDriverService = new Mock<IDriverService>();
        mockDriverService.Setup(service => service.GetSortedDrivers())
                        .Returns(new List<Driver>
                        {
                            new Driver { Id = "1", FirstName = "john" ,LastName= "michael" , Email="driverone@example.com" , PhoneNumber = "212334231" },
                            new Driver { Id = "2", FirstName = "elise" ,LastName= "tamer" , Email="drivertwo@example.com" , PhoneNumber = "233433532" },
                        }.OrderBy(d=>d.FirstName).ThenBy(d=>d.LastName));

        var controller = new DriversController(mockDriverService.Object, null);

        // Act
        var result = controller.GetSortedDrivers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var drivers = Assert.IsAssignableFrom<IEnumerable<Driver>>(okResult.Value);
        Assert.Equal("elise", drivers.First().FirstName);
        Assert.Equal("john", drivers.Last().FirstName);
    }

    [Fact]
    public void GetAlphabetizedName_ReturnsAlphabetizedName()
    {
        // Arrange
        var mockDriverService = new Mock<IDriverService>();
        var driverId = "1";
        var driverName = "Driver A";
        mockDriverService.Setup(service => service.GetAlphabetizedName(driverId))
                        .Returns(driverName);

        var controller = new DriversController(mockDriverService.Object, null);

        // Act
        var result = controller.GetAlphabetizedName(driverId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var alphabetizedName = Assert.IsType<string>(okResult.Value);
        Assert.Equal(driverName, alphabetizedName);
    }

    [Fact]
    public void GetById_ReturnsDriverById()
    {
        // Arrange
        var mockDriverService = new Mock<IDriverService>();
        var driverId = "1";        
        var driver= new Driver { Id = "1", FirstName = "Driver First Name 1", LastName = "Driver Last Name 1", Email = "driverone@example.com", PhoneNumber = "212334231" };
        mockDriverService.Setup(service => service.GetDriverById(driverId))
                        .Returns(driver);

        var controller = new DriversController(mockDriverService.Object, null);

        // Act
        var result = controller.Get(driverId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var driverResult = Assert.IsType<Driver>(okResult.Value);
        Assert.Equal(driverId, driverResult.Id);
    }

    [Fact]
    public void Delete_ReturnsOkResult()
    {
        // Arrange
        var mockDriverService = new Mock<IDriverService>();
        var driverId = "1";

        var controller = new DriversController(mockDriverService.Object, null);

        // Act
        var result = controller.Delete(driverId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal($"Successfully delete the driver with id: {driverId}", okResult.Value);
    }
}

