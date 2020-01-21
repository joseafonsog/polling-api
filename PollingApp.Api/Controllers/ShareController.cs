using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PollingApp.Admin;
using PollingApp.Api.Dtos;

namespace PollingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly IShareAdmin _shareAdmin;

        public ShareController(IShareAdmin shareAdmin)
        {
            _shareAdmin = shareAdmin;
        }

        [HttpGet]
        public ActionResult Get(
            [FromQuery(Name = "destination_email")] string destinationEmail,
            [FromQuery(Name = "content_url")]string contentUrl)
        {
            JsonResult jsonResult;

            try
            {
                _shareAdmin.SendEmail(destinationEmail, contentUrl);
                jsonResult = new JsonResult(new StatusResponseDto
                {
                    Status = "OK"
                });
                jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                jsonResult = new JsonResult(new StatusResponseDto
                {
                    Status = "Bad Request. Either destination_email not valid or empty content_url"
                });
                jsonResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest;
            }

            return jsonResult;
        }
    }
}
