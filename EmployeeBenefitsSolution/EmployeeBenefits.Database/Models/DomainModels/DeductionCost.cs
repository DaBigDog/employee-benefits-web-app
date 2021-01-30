using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBenefits.Database.Models
{
    public class DeductionCost
    {
        public decimal EmployeeYearlyCost { get; set; }
        public decimal EmployeeYearlyCostWithDiscount { get; set; }

        public decimal DependentYearlyCost { get; set; }
        public decimal DependentYearlyCostWithDiscount { get; set; }

        public decimal EmployeeBiWeeklyCost { get; set; }
        public decimal EmployeeBiweeklyCostWithDiscount { get; set; }

        public decimal DependentBiweeklyCost { get; set; }
        public decimal DependentBiweeklyCostWithDiscount { get; set; }

        public string DeductionMatch { get; set; }
    }
}
