using Kazeoseki.Models.ViewModels;
using Kazeoseki.Services.Services;
using KazeosekiApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kazeoseki.Web.Controllers.Api
{
    [RoutePrefix("api/linkurldata")]
    public class LinkUrlDataController : ApiController
    {
        LinkUrlDataService linkUrlDataService = new LinkUrlDataService();

        [Route, HttpGet, AllowAnonymous]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemResponse<LinkUrlData> resp = new ItemResponse<LinkUrlData>();
                resp.Item = linkUrlDataService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
