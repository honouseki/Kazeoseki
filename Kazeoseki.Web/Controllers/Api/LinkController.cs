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
    [RoutePrefix("api/link")]
    public class LinkController : ApiController
    {
        LinkService linkService = new LinkService();

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage Insert(Link model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                model.ModifiedBy = "1";
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = linkService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route, HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                ItemsResponse<Link> resp = new ItemsResponse<Link>();
                resp.Items = linkService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<Link> resp = new ItemResponse<Link>();
                resp.Item = linkService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut, AllowAnonymous]
        public HttpResponseMessage Update(int id, Link model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                model.Id = id;
                model.ModifiedBy = "1";
                linkService.Update(model);
                SuccessResponse resp = new SuccessResponse();
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
                linkService.Delete(id);
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
