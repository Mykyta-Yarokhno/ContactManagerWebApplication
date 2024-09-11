using ContactManagerWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System;

namespace ContactManagerWebApplication.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<PersonEntity> GetPersons()
        {
            return _context.Persons;
        }

        public async Task RegisterPerson(IEnumerable<PersonEntity> persons)
        {
            _context.Persons.AddRange(persons);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterPerson(PersonEntity person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonInfo(PersonEntity person)
        {
            var personInDb = await _context.Persons.FindAsync(person.Id);
            if (personInDb != null)
            {
                personInDb.Name = person.Name;
                personInDb.DateOfBirth = person.DateOfBirth;
                personInDb.Married = person.Married;
                personInDb.Phone = person.Phone;
                personInDb.Salary = person.Salary;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePersonInfo(PersonEntity person)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }

        public PersonEntity? FindPerson(int id)
        {
            return _context.Persons.Find(id);
        }
    }
}
