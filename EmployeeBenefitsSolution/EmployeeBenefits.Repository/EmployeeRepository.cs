using EmployeeBenefits.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Repository
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {

    }


    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
