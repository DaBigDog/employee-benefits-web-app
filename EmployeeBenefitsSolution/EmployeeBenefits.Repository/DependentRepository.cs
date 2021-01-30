using EmployeeBenefits.Database.Models;

namespace EmployeeBenefits.Repository
{
    public interface IDependentRepository : IRepositoryBase<Dependent>
    {

    }

    /// <summary>
    /// Allows access and changes to Dependent table rows.
    /// </summary>
    public class DependentRepository : RepositoryBase<Dependent>, IDependentRepository
    {

        public DependentRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
