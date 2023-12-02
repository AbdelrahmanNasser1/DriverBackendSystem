namespace Driver_Test;

public class DriverRepositorytests: IDisposable
{
    private const string ConnectionString = "Data Source=Driver.db";
    private readonly string _scriptPath = "C:\\Users\\Abdo\\source\\repos\\DriverBackendSystem\\Driver_WebAPI\\Scripts";
    private readonly DriverDbContext _context;

    public DriverRepositorytests()
    {
        _context = new DriverDbContext(ConnectionString,_scriptPath);    
    }
    [Fact]
    public void Add_Driver_ShouldIncreaseCount()
    {
        // Arrange
        var repository = new DriverRepository<Driver>(_context);
        var addedDriver = new Driver { Id = Guid.NewGuid().ToString(), FirstName = "Test First Name", LastName = "Test First Name", Email = "TestEmail@example.com", PhoneNumber = "132327613" };
        repository.Add(addedDriver);

        // Act
        var retrievedDriver = repository.GetById(addedDriver.Id!);

        // Assert
        Assert.NotNull(retrievedDriver);
        Assert.Equal(addedDriver.Id, retrievedDriver.Id);
        Assert.Equal(addedDriver.FirstName, retrievedDriver.FirstName);
        Assert.Equal(addedDriver.LastName, retrievedDriver.LastName);
        Assert.Equal(addedDriver.Email, retrievedDriver.Email);
        Assert.Equal(addedDriver.PhoneNumber, retrievedDriver.PhoneNumber);
    }
    [Fact]
    public void GetById_ExistingDriver_ShouldReturnDriver()
    {
        // Arrange
        var repository = new DriverRepository<Driver>(_context);
        var addedDriver = new Driver { Id = Guid.NewGuid().ToString(), FirstName = "Test First Name", LastName = "Test First Name", Email = "TestEmail@example.com", PhoneNumber = "132327613" };
        repository.Add(addedDriver);

        // Act
        var retrievedDriver = repository.GetById(addedDriver.Id!);

        // Assert
        Assert.NotNull(retrievedDriver);
        Assert.Equal(addedDriver.Id, retrievedDriver.Id);
        Assert.Equal(addedDriver.FirstName, retrievedDriver.FirstName);
        Assert.Equal(addedDriver.LastName, retrievedDriver.LastName);
        Assert.Equal(addedDriver.Email, retrievedDriver.Email);
        Assert.Equal(addedDriver.PhoneNumber, retrievedDriver.PhoneNumber);
    }

    [Fact]
    public void Update_ExistingDriver_ShouldUpdateProduct()
    {
        // Arrange
        var repository = new DriverRepository<Driver>(_context);
        var addedDriver = new Driver { Id = Guid.NewGuid().ToString(), FirstName = "Test First Name", LastName = "Test First Name", Email = "TestEmail@example.com", PhoneNumber = "132327613" };
        repository.Add(addedDriver);

        // Act
        addedDriver.FirstName = "Updated First Name";
        addedDriver.LastName = "Updated Last Name";
        addedDriver.Email = "UpdatedTestEmail@gmail.com";
        addedDriver.PhoneNumber = "132387663";
        repository.Update(addedDriver);
        
        // Assert
        var updatedDriver = repository.GetById(addedDriver.Id);
        Assert.NotNull(updatedDriver);
        Assert.Equal("Updated First Name", updatedDriver.FirstName);
        Assert.Equal("Updated Last Name", updatedDriver.LastName);
        Assert.Equal("UpdatedTestEmail@gmail.com",updatedDriver.Email);
        Assert.Equal("132387663", updatedDriver.PhoneNumber);
    }

    [Fact]
    public void Delete_ExistingDriver_ShouldDecreaseCount()
    {
        // Arrange
        var repository = new DriverRepository<Driver>(_context);
        var addedDriver = new Driver { Id = Guid.NewGuid().ToString(), FirstName = "Test First Name", LastName = "Test First Name", Email = "TestEmail@example.com", PhoneNumber = "132327613" };
        repository.Add(addedDriver);
        var initialCount = repository.GetAll().Count();

        // Act
        repository.Delete(addedDriver.Id);

        // Assert
        var updatedCount = repository.GetAll().Count();
        Assert.Equal(initialCount - 1, updatedCount);
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}