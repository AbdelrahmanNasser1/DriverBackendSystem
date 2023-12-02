namespace Driver_WebAPI.Models;

/// <summary>
/// Our primary model contains properties of Driver
/// </summary>
public class Driver
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}