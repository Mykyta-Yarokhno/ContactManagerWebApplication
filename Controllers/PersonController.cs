using ContactManagerWebApplication.Models;
using ContactManagerWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System;

namespace ContactManagerWebApplication.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            var persons = _personService.GetPersons();

            return View(persons);
        }

        [HttpPost]
        public async Task<IActionResult> UploadNewCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var persons = new List<PersonEntity>();

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var cultureInfo = new CultureInfo("en-US");

                // Skip CSV header 
                streamReader.ReadLine();

                string? line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length == 5)
                    {
                        try
                        {
                            var person = new PersonEntity
                            {
                                Name = values[0],
                                DateOfBirth = DateTime.Parse(values[1]),
                                Married = bool.Parse(values[2]),
                                Phone = values[3],
                                Salary = decimal.Parse(values[4], cultureInfo)
                            };

                            persons.Add(person);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                }
            }

            if (persons.Count > 0)
                await _personService.RegisterPerson(persons);
                    
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonInfo([FromBody] PersonEntity person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _personService.UpdatePersonInfo(person);

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePersonInfo(int id)
        {
            var person =  _personService.FindPerson(id);

            if (person == null)
                return NotFound();

            await _personService.DeletePersonInfo(person);

            return Ok();
        }
    }
}
