using AutoMapper;
using Driver_WebAPI.DTOs;
using Driver_WebAPI.Interfaces;
using Driver_WebAPI.Models;
using System;
using System.Text;
using System.Text.Json;

namespace Driver_WebAPI.Services;

/// <summary>
/// contains all implementation for driver methods
/// </summary>
public class DriverService : IDriverService
{
    private readonly IDriverRepository<Driver> _driverRepo;
    private readonly ILogger<DriverService> _logger;
    private readonly IMapper _mapper;

    public DriverService(IDriverRepository<Driver> driverRepo,
        ILogger<DriverService> logger,
        IMapper mapper
        )
    {
        _driverRepo = driverRepo;
        _logger = logger;
        _mapper = mapper;
    }
    public Driver AddDriver(DriverDto driverModel)
    {
        _logger.LogInformation($"add driver: {JsonSerializer.Serialize(driverModel)}");
        var driver = _mapper.Map<Driver>( driverModel );
        
        //add Guid Id
        driver.Id = Guid.NewGuid().ToString();

        _driverRepo.Add(driver);

        _logger.LogInformation($"driver added successfully: {JsonSerializer.Serialize(driver)}");
        return driver;
    }

    public void DeleteDriverById(string id)
    {
        _logger.LogInformation($"delete driver by id: {id}");
        try
        {
            _driverRepo.Delete(id);
            _logger.LogInformation("driver deleted successfully");
        }
        catch (Exception)
        {
            throw new Exception("Driver is not found");
        }
    }

    public string GetAlphabetizedName(string id)
    {
        _logger.LogInformation($"get driver by id: {id}");

        var driver = new Driver();
        var alphabetizeName = new StringBuilder();

        try
        {
            driver = _driverRepo.GetById(id);
            driver.FirstName = AlphabetizeName(driver.FirstName!);
            driver.LastName = AlphabetizeName(driver.LastName!);

            alphabetizeName.Append(driver.FirstName);
            alphabetizeName.Append(" ");
            alphabetizeName.Append(driver.LastName);
        }
        catch (Exception)
        {
            throw new Exception("Driver is not found");
        }

        _logger.LogInformation($"driver retrieved successfully: {JsonSerializer.Serialize(driver)}");
        return alphabetizeName.ToString();
    }

    private string AlphabetizeName(string name)
    {
        var alphabetizedChars = name.ToLower().OrderBy(c => c);
        return new string(alphabetizedChars.ToArray());
    }

    public Driver GetDriverById(string id)
    {
        _logger.LogInformation($"get driver by id: {id}");
        var driver = new Driver();

        try
        {
            driver = _driverRepo.GetById(id);
            _logger.LogInformation($"driver retrieved successfully: {JsonSerializer.Serialize(driver)}");
        }
        catch (Exception)
        {
            throw new Exception("Driver is not found");
        }
       
        return driver;
    }

    public IEnumerable<Driver> GetDrivers()
    {
        _logger.LogInformation("Get all drivers...");
        var drivers = _driverRepo.GetAll();

        _logger.LogInformation($"retrieve all drivers successfully and their count are : {drivers.Count()}");
        return drivers;
    }

    public IEnumerable<Driver> GetSortedDrivers()
    {
        _logger.LogInformation("Get all drivers sorted...");
        var drivers = _driverRepo.GetAll();

        drivers.OrderBy(d => d.LastName).ThenBy(d => d.FirstName);

        _logger.LogInformation($"retrieve all drivers successfully and their count are : {drivers.Count()}");
        return drivers;
        
    }

    public Driver UpdateDriver(DriverDto driverModel, string id)
    {
        _logger.LogInformation($"update driver with id: {id}");
        var driver = _mapper.Map<Driver>(driverModel);

        driver.Id = id;

        _driverRepo.Update(driver);

        var updatedDriver = GetDriverById(id);
        _logger.LogInformation($"driver updated successfully : {JsonSerializer.Serialize(updatedDriver)}");
        return updatedDriver;
    }

    public IEnumerable<Driver> AddRandomDrivers(int num = 10)
    {
        for (int i= 0; i< num; i++)
        {
            var driver = new Driver
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = GenerateRandomNames(),
                LastName = GenerateRandomNames(),
                Email = $"{GenerateRandomNames().ToLower()}@example.com",
                PhoneNumber = GenerateRandomNumber()
            };

            _driverRepo.Add(driver);
        }

        return _driverRepo.GetAll();
    }
    private string GenerateRandomNames()
    {
        var random = new Random();

        var nameLength = random.Next(5, 10);
        var randomName = new string(Enumerable.Range(0, nameLength)
            .Select(_ => (char)('a' + random.Next(0, 26)))
            .ToArray());

        return randomName;
    }
    private string GenerateRandomNumber()
    {
        var random = new Random();

        // Generate a random 10-digit number
        long number = (long)(random.NextDouble() * 9000000000) + 1000000000;
        return number.ToString();
    }
}
