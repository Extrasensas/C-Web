using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitmController : ControllerBase
    {
        static IList<Car> cars = new List<Car>
        {
            new Car() { Id = 1, Make = "Opel", Model = "Ascona" },
            new Car() { Id = 2, Make = "Ferrari", Model = "F10" },
            new Car() { Id = 3, Make = "BMW", Model = "S" },
        };

        public IActionResult Get()
        {
            return Ok(cars);
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            return Ok(cars.First(c => c.Id == id));
        }
        [HttpPost]
        public IActionResult Post(Car car)
        {
            cars.Add(car);
            return Ok();

        }      
    }
}
