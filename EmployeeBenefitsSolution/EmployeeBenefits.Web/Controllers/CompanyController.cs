using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Service;



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
