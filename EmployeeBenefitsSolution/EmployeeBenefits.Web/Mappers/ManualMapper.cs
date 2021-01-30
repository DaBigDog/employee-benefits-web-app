using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Web.Mappers
{
    public static class ManualMapper
    {

        public static List<Employee> ConvertToModel(this IEnumerable<EmployeeViewModel> employeeViewModels, int companyId)
        {
            List<Employee> list = new List<Employee>();
            if (employeeViewModels == null || !employeeViewModels.Any())
            {
                return list;
            }

            for (int i = 0; i < employeeViewModels.Count(); i++)
            {
                EmployeeViewModel vm = employeeViewModels.ToList()[i];
                Employee newEmployee = new Employee()
                {
                    Id = vm.id,
                    CompanyId = companyId,
                    FirstName = vm.firstName,
                    MiddleName = vm.middleName,
                    LastName = vm.lastName
                };

                if (vm.dependents != null && vm.dependents.Any())
                {
                    newEmployee.Dependents = new Collection<Dependent>(vm.dependents.Select(vmDep => new Dependent()
                    {
                        Id = vmDep.id,
                        EmployeeId = vmDep.employeeId,
                        FirstName = vmDep.firstName,
                        MiddleName = vmDep.middleName,
                        LastName = vmDep.lastName
                    }).ToList());
                }

                list.Add(newEmployee);
            }

            return list;
        }


    }
}
