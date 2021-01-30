using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeBenefits.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyService service;
        private readonly IOptions<Models.CompanySettings> companySettings;

        public CompanyController(ICompanyService companyService, IOptions<Models.CompanySettings> options)
        {
            service = companyService;
            companySettings = options;
        }




        // GET: api/Company
        [HttpGet]
        public IEnumerable<Company> Get()
        {
            return service.GetAll();
        }

        // GET api/Company/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Company
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Company/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Company/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
