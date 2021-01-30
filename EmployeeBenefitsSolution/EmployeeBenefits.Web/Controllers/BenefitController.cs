using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using EmployeeBenefits.Database.Models;
using EmployeeBenefits.Service;

namespace EmployeeBenefits.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitService service;
        private readonly Models.CompanySettings companySettings;
        public BenefitController(IBenefitService benefitService, IOptions<Models.CompanySettings> options)
        {
            service = benefitService;
            companySettings = options.Value;
        }


        // GET: api/Benefit
        [HttpGet]
        public IEnumerable<Benefit> Get()
        {
            return service.GetAllByCompanyId(companySettings.CompanyId);
        }

        [HttpGet, Route("DeductionCost")]
        public DeductionCost GetBenefitDeductionCost()
        {
            return service.GetBenefitDeductionCosts(companySettings.CompanyId);
        }

        [HttpGet, Route("Latest")]
        public Benefit GetLatestBenefit()
        {
            return service.GetLatestByCompanyId(companySettings.CompanyId);
        }

        // GET api/Benefit/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Benefit
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Benefit/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Benefit/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
