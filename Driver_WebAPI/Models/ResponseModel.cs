namespace Driver_WebAPI.Models;

/// <summary>
/// Contains properties of response in case exception happens 
/// </summary>
public class ResponseModel
{
    public int responseCode { get; set; }
    public string? responseMessage { get; set; }
}
