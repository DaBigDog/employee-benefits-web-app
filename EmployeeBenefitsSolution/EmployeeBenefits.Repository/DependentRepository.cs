using EmployeeBenefits.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Repository
{
    public interface IDependentRepository : IRepositoryBase<Dependent>
    {

    }


    public class DependentRepository : RepositoryBase<Dependent>, IDependentRepository
    {

        public DependentRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
