using Driver_WebAPI.DTOs;
using Driver_WebAPI.Interfaces;
using Driver_WebAPI.Models;
using Driver_WebAPI.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace DriverSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly IValidator<DriverDto> _validator;

        public DriversController(IDriverService driverService,
               IValidator<DriverDto> validator
            )
        {
            _driverService = driverService;
            _validator = validator;
        }

        // GET: api/<DriversController>
        [HttpGet]
        public IActionResult Get()
        {
            var drivers = _driverService.GetDrivers();
            return Ok(drivers);
        }

        // GET: api/<DriversController>/sort-drivers
        [HttpGet("sort-drivers")]
        public IActionResult GetSortedDrivers()
        {
            var drivers = _driverService.GetSortedDrivers();
            return Ok(drivers);
        }

        [HttpGet("alphabetizedname/{id}")]
        public IActionResult GetAlphabetizedName(string id)
        {
            var alphabetizedName = _driverService.GetAlphabetizedName(id);
            if (string.IsNullOrEmpty( alphabetizedName))
            {
                return NotFound();
            }

           
            return Ok(alphabetizedName);
        }

        // GET api/<DriversController>/alphabetizedName/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var driver = _driverService.GetDriverById(id);
            if (driver == null)
            {
                return NotFound("Driver not founded.");
            }
            return Ok(driver);
        }

        // POST api/<DriversController>/create-random-drivers
        [HttpPost("create-random-drivers/{count}")]
        public ActionResult Post(int count = 10)
        {
            var drivers = _driverService.AddRandomDrivers(count);

            return Ok(drivers);
        }

        // POST api/<DriversController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DriverDto model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                ValidationErrors.GetInputValidationErrors(result);
            }

            var driver = _driverService.AddDriver(model);

            if (driver == null)
            {
                return NotFound("Driver is not added successfully.");
            }
            return Ok(driver);
        }

        // PUT api/<DriversController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] DriverDto model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                ValidationErrors.GetInputValidationErrors(result);
            }
            var driver = _driverService.UpdateDriver(model, id);
            return Ok(driver);
        }

        // DELETE api/<DriversController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _driverService.DeleteDriverById(id);
            return Ok($"Successfully delete the driver with id: {id}");
        }
    }
}