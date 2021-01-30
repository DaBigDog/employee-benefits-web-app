using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;
using EmployeeBenefits.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeBenefits.NUnitTests
{


    public class BenefitServiceTest
    {
        private Mock<IBenefitRepository> mocBenefitRepository;
        private List<Benefit> benefits;

        [SetUp]
        public void Setup()
        {
            mocBenefitRepository = new Mock<IBenefitRepository>();
            benefits = new List<Benefit>();
            benefits.Add(new Benefit() {
                Id = 1,
                CompanyId = 1,
                EmpYearlyBenefitCost = 1000.00m,
                DepYearlyBenefitCost = 500.00m,
                DiscountMatch = "a",
                PercentDiscount = 10
            });

        }


        [Test]
        public void GetBenefitDeductionCosts()
        {
            // must set up the IQueryable Where to return value instead of null reference error...
            mocBenefitRepository.Setup(e => e.Where(e => e.CompanyId == 1)).Returns(benefits.AsQueryable());

            BenefitService benefitService = new BenefitService(mocBenefitRepository.Object);
            DeductionCost deductionCost = benefitService.GetBenefitDeductionCosts(1);


            Assert.IsTrue(deductionCost.EmployeeYearlyCost == 1000.00m);
            Assert.IsTrue(deductionCost.EmployeeYearlyCostWithDiscount == 900.00m);

            Assert.IsTrue(deductionCost.DependentYearlyCost == 500.00m);
            Assert.IsTrue(deductionCost.DependentYearlyCostWithDiscount == 450.00m);

        }

    }
}
