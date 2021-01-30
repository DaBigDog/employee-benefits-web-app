using EmployeeBenefits.Database.Models;

namespace EmployeeBenefits.Repository
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {

    }

    /// <summary>
    /// Allows access and changes to Employee table rows.
    /// </summary>
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
