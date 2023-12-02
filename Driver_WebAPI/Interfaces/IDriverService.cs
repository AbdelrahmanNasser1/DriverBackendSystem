using Driver_WebAPI.DTOs;
using Driver_WebAPI.Models;

namespace Driver_WebAPI.Interfaces;

/// <summary>
/// contains all defination for driver methods
/// </summary>
public interface IDriverService
{
    Driver GetDriverById(string id);
    string GetAlphabetizedName(string id);
    IEnumerable<Driver> GetDrivers();
    IEnumerable<Driver> GetSortedDrivers();
    Driver AddDriver(DriverDto driverModel);
    Driver UpdateDriver(DriverDto driverModel, string id);
    void DeleteDriverById(string id);
    IEnumerable<Driver> AddRandomDrivers(int count = 10);
}
