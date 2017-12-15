using Kazeoseki.Models.Domain;
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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserService userService = new UserService();

        [Route, HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                ItemsResponse<User> resp = new ItemsResponse<User>();
                resp.Items = userService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{username}"), HttpGet, AllowAnonymous]
        public HttpResponseMessage SelectByUsername(string username)
        {
            try
            {
                ItemResponse<LoginUser> resp = new ItemResponse<LoginUser>();
                resp.Item = userService.SelectByUsername(username);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage Insert(LoginUser model)
        {
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = userService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
