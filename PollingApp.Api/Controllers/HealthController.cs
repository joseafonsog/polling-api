using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PollingApp.Api.Dtos;

namespace PollingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var jsonResult = new JsonResult(new StatusResponseDto
                {
                    Status = "OK"
                });
                jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;

                return jsonResult;
            }
            catch (Exception)
            {
                var jsonResult = new JsonResult(new StatusResponseDto
                {
                    Status = "Bad Request. Either destination_email not valid or empty content_url"
                });
                jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status503ServiceUnavailable;
                return jsonResult;
            }
        }

    }
}
