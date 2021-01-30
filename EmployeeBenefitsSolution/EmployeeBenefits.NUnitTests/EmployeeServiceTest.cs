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

    
    public class EmployeeServiceTest
    {
        private Mock<IEmployeeRepository> mocEmployeeRepo;
        private List<Employee> employees;

        [SetUp]
        public void Setup()
        {
            mocEmployeeRepo = new Mock<IEmployeeRepository>();
            employees = new List<Employee>();

            employees.Add(new Employee() { Id = 1, CompanyId = 1, FirstName = "TestFirst1", MiddleName = "TestMiddle1", LastName = "TestLast1" });
            employees.Add(new Employee() { Id = 2, CompanyId = 1, FirstName = "TestFirst2", MiddleName = "", LastName = "TestLast2" });
            employees.Add(new Employee() { Id = 3, CompanyId = 1, FirstName = "TestFirst3", MiddleName = "TestMiddle3", LastName = "TestLast3" });
        }


        [Test]
        public void GetAllEmployees()
        {
            mocEmployeeRepo.Setup(e => e.GetAll()).Returns(employees.AsQueryable);

            EmployeeService employeeService = new EmployeeService(mocEmployeeRepo.Object, null);
            IEnumerable<Employee> employeeServiceList = employeeService.GetAll();


            Assert.IsTrue(employeeServiceList.Count() == 3);
            Assert.IsTrue(employeeServiceList.All(e => !string.IsNullOrEmpty(e.FirstName)));
            Assert.IsFalse(employeeServiceList.Any(e => e.CompanyId != 1));
        }

    }
}
