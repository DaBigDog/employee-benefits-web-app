using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Service
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
    }





    public class CompanyService : ICompanyService
    {
        private ICompanyRepository repo;

        public CompanyService(ICompanyRepository companyRepository)
        {
            repo = companyRepository;
        }


        public IEnumerable<Company> GetAll()
        {
            return repo.GetAll();
        }

    }
}
