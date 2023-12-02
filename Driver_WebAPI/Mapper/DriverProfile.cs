using AutoMapper;
using Driver_WebAPI.DTOs;
using Driver_WebAPI.Models;

namespace Driver_WebAPI.Mapper;

public class DriverProfile: Profile
{
    public DriverProfile()
    {
        CreateMap<DriverDto, Driver>();
    }
}
