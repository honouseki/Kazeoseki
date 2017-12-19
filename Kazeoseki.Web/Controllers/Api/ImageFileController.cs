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

        [Route("{id:int}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<ImageFile> resp = new ItemResponse<ImageFile>();
                resp.Item = imageFileService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

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

        [Route("{id:int}"), HttpDelete, AllowAnonymous]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ImageFile model = imageFileService.SelectById(id);
                imageFileService.Delete(id, model.SystemFileName);
                SuccessResponse resp = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
