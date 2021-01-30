
using EmployeeBenefits.Database.Models;

namespace EmployeeBenefits.Repository
{
    public interface IBenefitRepository : IRepositoryBase<Benefit>
    {

    }

    /// <summary>
    /// Allows access and changes to Benefit table rows.
    /// </summary>
    public class BenefitRepository : RepositoryBase<Benefit>, IBenefitRepository
    {

        public BenefitRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
