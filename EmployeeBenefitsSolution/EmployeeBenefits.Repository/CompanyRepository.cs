using EmployeeBenefits.Database.Models;

namespace EmployeeBenefits.Repository
{


    public interface ICompanyRepository : IRepositoryBase<Company>
    {

    }

    /// <summary>
    /// Allows access and changes to Company table rows.
    /// </summary>
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {

        public CompanyRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
