
using DataAccess.Services.SiteSelection.Interfaces;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : BaseApiController

    {
        private readonly ISiteSelector siteSelector;

        public SiteController(ISiteSelector siteSelector)
        {
            this.siteSelector = siteSelector;
        }
       
        /// <param name="model"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult ChangeSite([FromBody] SiteDto model)
        {
            if (ModelState.IsValid)
            {
                siteSelector.SelectedSite = model.SiteName;
                return Ok(siteSelector.SelectedSite);
            }
            else
            {
                return BadRequest("No Sitename was given");
            }
        }
    }
}
