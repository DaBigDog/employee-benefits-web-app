using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefits.Web.ViewModels
{

    /// <summary>
    /// Employee view model for client data in the Employee controller
    /// </summary>
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
