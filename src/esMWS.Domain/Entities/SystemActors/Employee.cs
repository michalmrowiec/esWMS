using System.ComponentModel.DataAnnotations;

namespace esMWS.Domain.Entities.SystemActors
{
    public class Employee
    {
        [Required]
        [MaxLength(60)]
        public string EmployeeId { get; set; } = null!;
        [Required]
        [MaxLength(3)]
        public string RoleId { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(255)]
        public string? PasswordHash { get; set; }
        [MaxLength(255)]
        public string EmailAddress { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string Country { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string Region { get; set; }
        [MaxLength(25)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
