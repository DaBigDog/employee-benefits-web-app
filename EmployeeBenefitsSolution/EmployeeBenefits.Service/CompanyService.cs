using System.Collections.Generic;

using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;

namespace EmployeeBenefits.Service
{
    /// <summary>
    /// Comapny service interfacec
    /// </summary>
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
    }




    /// <summary>
    /// Company service.
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository repo;

        public CompanyService(ICompanyRepository companyRepository)
        {
            repo = companyRepository;
        }

        /// <summary>
        /// Get all company entities
        /// </summary>
        /// <returns>IEnumerable<Company></returns>
        public IEnumerable<Company> GetAll()
        {
            return repo.GetAll();
        }

    }
}
