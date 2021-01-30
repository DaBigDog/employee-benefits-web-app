using System.Collections.Generic;

using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;

namespace EmployeeBenefits.Service
{

    /// <summary>
    /// Dependent service interface.
    /// </summary>
    public interface IDependentService
    {
        IEnumerable<Dependent> GetAll();
    }




    /// <summary>
    /// Dependent service.
    /// </summary>
    public class DependentService : IDependentService
    {
        private IDependentRepository repo;

        public DependentService(IDependentRepository DependentRepository)
        {
            repo = DependentRepository;
        }

        /// <summary>
        /// Get all Dependent entities
        /// </summary>
        /// <returns>IEnumerable<Dependent></returns>
        public IEnumerable<Dependent> GetAll()
        {
            return repo.GetAll();
        }

    }
}
