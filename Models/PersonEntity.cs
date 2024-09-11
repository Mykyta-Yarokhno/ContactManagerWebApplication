using System.ComponentModel.DataAnnotations;

namespace ContactManagerWebApplication.Models
{
    public class PersonEntity
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public bool Married { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
    }
}
