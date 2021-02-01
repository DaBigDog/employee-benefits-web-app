using System;
using System.Collections.Generic;
using System.Linq;

using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Repository;

namespace EmployeeBenefits.Service
{
    public interface IBenefitService
    {
        IEnumerable<Benefit> GetAll();
        IQueryable<Benefit> GetAllByCompanyId(int companyId);

        Benefit GetLatestByCompanyId(int companyId);

        DeductionCost GetBenefitDeductionCosts(int companyId);
    }




    /// <summary>
    /// Service for the Benefit class.
    /// </summary>
    public class BenefitService : IBenefitService
    {
        private IBenefitRepository repo;

        public BenefitService(IBenefitRepository BenefitRepository)
        {
            repo = BenefitRepository;
        }

        /// <summary>
        /// Gets all Benefit entities.
        /// </summary>
        /// <returns>IEnumerable<Benefit></returns>
        public IEnumerable<Benefit> GetAll()
        {
            return repo.GetAll();
        }

        /// <summary>
        /// Get all Benefit entities with matching Company Id
        /// </summary>
        /// <param name="companyId">int - company id</param>
        /// <returns>IQueryable<Benefit></returns>
        public IQueryable<Benefit> GetAllByCompanyId(int companyId)
        {
            return repo.Where(b => b.CompanyId == companyId);
        }

        /// <summary>
        /// Gets latest Benefit entity for a Company.
        /// </summary>
        /// <param name="companyId">int company id</param>
        /// <returns>Benefit</returns>
        public Benefit GetLatestByCompanyId(int companyId)
        {
            return repo.Where(b => b.CompanyId == companyId).OrderByDescending(r => r.Id).FirstOrDefault();
        }


        /// <summary>
        /// Returns yearly and bi-weekly benefit costs for a Company
        /// </summary>
        /// <param name="companyId">int - company id</param>
        /// <returns>DeductionCost</returns>
        public DeductionCost GetBenefitDeductionCosts(int companyId)
        {
            return GetBenefitsDeductionCost(companyId);
        }

        /********************************************
         * 
         *  We need to do some calculations to ensure the total yearly benefit deduction
         *  equals the total cost of the deduction. For example... there are
         *  52 weeks in a year or 26 biweekly paychecks. $1000 cannot be split evenly between 26
         *  pay periods so we may need to adjust last pay period. 
         * 
         *  Undercharge Example:
         *  1000/26 = 38.4615
         *  38.46 * 26 = 999.96 <-- ooops. We undercharged and lost $$$. 
         *  So we need to add the extra $0.04 to the last pay period.
         *  
         *  
         *  Overcharge Example (10% discount applied):
         *  1000 - ((10/100) * 1000)
         *  900/26 = 34.6153
         *  34.62 * 26 = 900.12 <-- ooops we overcharged
         *  If we overcharge then we'll subtract it ($0.12) from the last pay period.
         *  
         * ******************************************/

        /// <summary>
        /// Returns the yearlt cost and avarage pay period deductions for each cost type
        /// </summary>
        /// <param name="companyId">int - company id</param>
        /// <returns>DeductionCost - avg. pay period deduction for each type</returns>
        private DeductionCost GetBenefitsDeductionCost(int companyId)
        {
            DeductionCost model = new DeductionCost();
            List<decimal> deductionSchedule = new List<decimal>();

            Benefit benefit = repo.Where(c => c.CompanyId == companyId).OrderByDescending(o => o.Id).FirstOrDefault();

            if (benefit == null)
            {
                // The Company must have a it's benefit(s) first set up.
                // We must first notify the user
                // and redirect the user to the Benefits entry page... which we need to complete.

                throw new Exception("Benefit for the Company does not yet exist.");

                // Please execute the EmployeeBenefits.Database -> Scripts -> InitializeDB.sql script
                // to create the dabase and populate the Company and Beneit tables
            }

            model.DeductionMatch = benefit.DiscountMatch;

            // calculate the yearly costs
            model.EmployeeYearlyCost = TotalBenefitCost(benefit.EmpYearlyBenefitCost, null);
            model.EmployeeYearlyCostWithDiscount = TotalBenefitCost(benefit.EmpYearlyBenefitCost, benefit.PercentDiscount);

            model.DependentYearlyCost = TotalBenefitCost(benefit.DepYearlyBenefitCost, null);
            model.DependentYearlyCostWithDiscount  = TotalBenefitCost(benefit.DepYearlyBenefitCost, benefit.PercentDiscount);

            // it's assumed this will always be biweekly but we can change later
            model.EmployeeBiWeeklyCost = CalculateAverageBenefitDeduction(benefit.EmpYearlyBenefitCost, null);
            model.EmployeeBiweeklyCostWithDiscount = CalculateAverageBenefitDeduction(benefit.EmpYearlyBenefitCost, benefit.PercentDiscount);

            model.DependentBiweeklyCost = CalculateAverageBenefitDeduction(benefit.DepYearlyBenefitCost, null);
            model.DependentBiweeklyCostWithDiscount = CalculateAverageBenefitDeduction(benefit.DepYearlyBenefitCost, benefit.PercentDiscount);


            return model;
        }

        /// <summary>
        /// Calculates the average pay period deduction
        /// </summary>
        /// <param name="yearlyCost">decimal - yearly cost of benefit</param>
        /// <param name="discount">int? - discount if one applies</param>
        /// <returns></returns>
        private decimal CalculateAverageBenefitDeduction(decimal yearlyCost, int? discount)
        {
            decimal averageDeduction = 0.00M;
            yearlyCost = TotalBenefitCost(yearlyCost, discount);

            averageDeduction = RoundDown(GetBiWeeklyDeductionSchedule(yearlyCost).Average(), 2);


            return averageDeduction;
        }


        /// <summary>
        /// Calculates the Yearly benefit cost with deduction (if there is one)
        /// </summary>
        /// <param name="yearlyCost">yearly Company cost - decimal</param>
        /// <param name="discount">Company percent discount - int</param>
        /// <returns>decimal</returns>
        private decimal TotalBenefitCost(decimal yearlyCost, int? discount)
        {
            if (discount.HasValue)
            {
                decimal percentDiscount = (discount.Value / 100m);
                decimal reduction = (percentDiscount * yearlyCost);
                yearlyCost = yearlyCost - reduction;
            }

            return yearlyCost;
        }

        /// <summary>
        /// Calculates the benefit deduction for each bi-weekly pay period
        /// </summary>
        /// <param name="yearlyCost">decial - yearly benefit cost</param>
        /// <returns>List - bi-weekly deduction for each pay period in a year</returns>
        private List<decimal> GetBiWeeklyDeductionSchedule(decimal yearlyCost)
        {
            List<decimal> list = new List<decimal>();
            decimal biweeklyDeduction = RoundDown((yearlyCost / 26m), 2);

            decimal total = 0.0m;
            for (int i = 0; i <= 25; i++)
            {
                if (i == 25)
                {
                    biweeklyDeduction = (yearlyCost - total);
                }

                total += biweeklyDeduction;
                list.Add(biweeklyDeduction);
            }

            return list;
        }

        /// <summary>
        /// Rounds down a decimal value.
        /// </summary>
        /// <param name="value">decimal - value to round down</param>
        /// <param name="decimalPlaces">int - number of decimal places in return value</param>
        /// <returns>decimal value rounded down with specified decimal places</returns>
        private decimal RoundDown(decimal value, int decimalPlaces)
        {
            decimal factor = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(value * factor) / factor;
        }

    }
}
