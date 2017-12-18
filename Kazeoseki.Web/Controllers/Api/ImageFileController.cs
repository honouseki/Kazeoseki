using Kazeoseki.Models.Domain;
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
    [RoutePrefix("api/imagefile")]
    public class ImageFileController : ApiController
    {
        private ImageFileService imageFileService = new ImageFileService();

        [Route("type/{typeId:int}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectByImageType(int typeId)
        {
            try
            {
                ItemsResponse<ImageFile> resp = new ItemsResponse<ImageFile>();
                resp.Items = imageFileService.SelectByImageType(typeId);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
