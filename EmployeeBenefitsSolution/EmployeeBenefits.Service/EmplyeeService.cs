using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeBenefits.Service
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        IQueryable<Employee> GetAllForCompany(int companyId);

        IQueryable<Employee> GetEmployeesAndDependentsForCompany(int companyId);

        void SaveEmployeeList(IEnumerable<Employee> employees);
    }

    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository repository;
        private IDependentRepository dependentRepository;

        public EmployeeService(IEmployeeRepository employeeRepo, IDependentRepository dependentRepo)
        {
            repository = employeeRepo;
            dependentRepository = dependentRepo;
        }


        public IEnumerable<Employee> GetAll()
        {
            return repository.GetAll();
        }

        public IQueryable<Employee> GetAllForCompany(int companyId)
        {
            return repository.Where(e => e.CompanyId == companyId);
        }

        public IQueryable<Employee> GetEmployeesAndDependentsForCompany(int companyId)
        {
            return repository.GetRelatedTablesExpression(e => e.CompanyId == companyId, "Dependents");
        }


        public void SaveEmployeeList(IEnumerable<Employee> employees)
        {
            /************************************************************************************* 
             * 
             *  This is not the best method to delete records deleted by the user but it works  
             * 
             *  We would be better to wrap the entire section in a transaction so everything is
             *  rolled back if one part fails.
             * 
             ************************************************************************************/

            /************  Delete   **************************/
            // first we need to delete Dependents that were deleted by user
            List<int> existingDepIds = employees.Where(emp => emp.Id != 0)
                .SelectMany(x => x.Dependents.Where(dep => dep.Id != 0).Select(d => d.Id)).ToList();

            dependentRepository.RemoveRange(dep => !existingDepIds.Contains(dep.Id));

            dependentRepository.Save();

            // next, we delete employees deleted by user
            List<int> existingEmployeeIds = employees.Where(emp => emp.Id != 0).Select(e => e.Id).ToList();

            repository.RemoveRange(emp => !existingEmployeeIds.Contains(emp.Id));

            repository.Save();


            /********** Add new Employees and Dependents *************/
            // find all new employees and their dependents
            List<Employee> newEmployees = employees.Where(e => e.Id == 0).ToList();
            newEmployees.ForEach(emp =>
            {
                repository.Add(emp);
            });
            // now commit the new employees and their dependents
            repository.Save();

            // find all new dependents added to existing employees and insert
            List<Employee> existingEmployeesWithNewDeps = employees.Where(e => e.Id != 0 && e.Dependents.Any(d => d.Id == 0)).ToList();
            existingEmployeesWithNewDeps.ForEach(exEmp =>
            {
                exEmp.Dependents.Where(d => d.Id == 0).ToList().ForEach(d =>
                {
                    // ensure we associate the dependent with the correct employee
                    if (d.EmployeeId != exEmp.Id) { d.EmployeeId = exEmp.Id; }
                    dependentRepository.Add(d);
                });
            });
            // commit new dependents added to existing employees
            dependentRepository.Save();
        }


    }
}