using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EmployeeBenefits.Database.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Dependents = new HashSet<Dependent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Dependent> Dependents { get; set; }
    }
}
