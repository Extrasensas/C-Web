using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.NewFolder;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitmController : ControllerBase
    {
        static IList<Car> cars = new List<Car>
        {
            new Car() {  Make = "Opel", Model = "Ascona" },
            new Car() { Make = "Ferrari", Model = "F10" },
            new Car() {  Make = "BMW", Model = "S" },
        };

        private readonly MydbContext _db;
        public KitmController(MydbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(cars);
        }
        [HttpGet("{Id}")]

        public IActionResult Get(Guid id)
        {
            foreach (var car in cars)
            {
                if (car.Id == id)
                {
                    return Ok(car);
                }
            }
            return new
                StatusCodeResult((int)HttpStatusCode.NotFound);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Cardto car)
        {

            if (!string.IsNullOrEmpty(car.Make) && !string.IsNullOrEmpty(car.Model))
            {
                bool exists = false;
                foreach (var item in cars)
                {
                    if (item.Model == car.Model && item.Make == car.Make)
                    {
                        exists = true;
                    }
                }
                if (exists)
                {
                    return new StatusCodeResult((int)StatusCodes.Status409Conflict);
                }

                Car newCar = new Car()
                {

                    Model = car.Model,
                    Make = car.Make
                };
                cars.Add(newCar);
                return Ok(newCar);
            }
            return BadRequest();
        }
   

        [HttpPut("id")]

        public IActionResult Put(Guid id, [FromBody] Cardto car)
        {
            if (!cars.Any(c => c.Id == id))
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(car.Make) && !string.IsNullOrEmpty(car.Model))
            {
                var editCar = cars.First(c => c.Id == id);
                editCar.Make = car.Make;
                editCar.Model = car.Model;
                return Accepted(editCar);
            }
            return BadRequest();


        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!cars.Any(c => c.Id == id))
            {
                return NotFound();

            }
            else
            {
                var carToDelete = cars.First(c => c.Id == id);
                cars.Remove(carToDelete);
                return new StatusCodeResult((int)StatusCodes.Status204NoContent);
            }
        }
    }
}




