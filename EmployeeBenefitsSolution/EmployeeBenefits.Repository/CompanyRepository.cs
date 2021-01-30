using EmployeeBenefits.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Repository
{


    public interface ICompanyRepository : IRepositoryBase<Company>
    {

    }


    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {

        public CompanyRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
