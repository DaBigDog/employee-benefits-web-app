using EmployeeBenefits.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Repository
{
    public interface IBenefitRepository : IRepositoryBase<Benefit>
    {

    }


    public class BenefitRepository : RepositoryBase<Benefit>, IBenefitRepository
    {

        public BenefitRepository(EmployeeBenefitsContext dbContext) : base(dbContext)
        {

        }


    }
}
