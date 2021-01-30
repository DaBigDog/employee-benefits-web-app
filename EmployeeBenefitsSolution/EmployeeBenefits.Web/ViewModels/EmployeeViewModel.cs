using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Web.ViewModels
{

    public class EmployeeViewModel : PersonViewModel
    {

        public IEnumerable<DependentViewModel> dependents { get; set; }
    }

    public class DependentViewModel: PersonViewModel
    {
        [Required]
        public int employeeId { get; set; }
    }


    public class PersonViewModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string firstName { get; set; }

        public string middleName { get; set; }

        [Required]
        public string lastName { get; set; }

    }


}
