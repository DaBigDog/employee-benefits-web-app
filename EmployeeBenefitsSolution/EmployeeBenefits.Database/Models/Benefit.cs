using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EmployeeBenefits.Database.Models
{
    public partial class Benefit
    {

        // Need to add these attribute to SQL auto-generated identity columns!
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public decimal EmpYearlyBenefitCost { get; set; }
        public decimal DepYearlyBenefitCost { get; set; }
        public int? PercentDiscount { get; set; }
        public string DiscountMatch { get; set; }

        public virtual Company Company { get; set; }
    }
}
