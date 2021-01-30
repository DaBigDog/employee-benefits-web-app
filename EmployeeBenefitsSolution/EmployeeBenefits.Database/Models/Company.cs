using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EmployeeBenefits.Database.Models
{
    public partial class Company
    {
        public Company()
        {
            Benefits = new HashSet<Benefit>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Benefit> Benefits { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
