using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationClient.Models
{
    public class Employee:PageModel
    {
        public int EmployeeID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
       

        public DateTime HireDate { get; set; }
        public double Salary { get; set; }
        public string? Department { get; set; }
    }
}
