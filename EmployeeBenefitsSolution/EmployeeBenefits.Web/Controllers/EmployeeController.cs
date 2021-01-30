using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Service;
using EmployeeBenefits.Web.Mappers;
using EmployeeBenefits.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EmployeeBenefits.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;
        private readonly Models.CompanySettings companySettings;


        public EmployeeController(IEmployeeService employeeService, IOptions<Models.CompanySettings> options)
        {
            service = employeeService;
            companySettings = options.Value;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return service.GetEmployeesAndDependentsForCompany(companySettings.CompanyId).ToList();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpPost, Route("List")]
        public void PostEmployeeList(EmployeeViewModel[] viewModel)
        {
            // no content to send back to ui

            // model state error are already handled in the start up
            if (ModelState.IsValid)
            {
                IEnumerable<Employee> employeeList = viewModel.ConvertToModel(companySettings.CompanyId);

                service.SaveEmployeeList(employeeList);
            }


        }
    }
}
