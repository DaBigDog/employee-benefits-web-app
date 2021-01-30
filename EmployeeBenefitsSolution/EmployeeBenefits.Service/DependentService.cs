using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Service
{
    public interface IDependentService
    {
        IEnumerable<Dependent> GetAll();
    }





    public class DependentService : IDependentService
    {
        private IDependentRepository repo;

        public DependentService(IDependentRepository DependentRepository)
        {
            repo = DependentRepository;
        }


        public IEnumerable<Dependent> GetAll()
        {
            return repo.GetAll();
        }

    }
}
