using Kazeoseki.Models.Domain;
using Kazeoseki.Services.Services;
using KazeosekiApp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Kazeoseki.Web.Controllers.Api
{
    [RoutePrefix("api/gallery")]
    public class GalleryController : ApiController
    {
        private ImageFileService imageFileService = new ImageFileService();
        //private GalleryService 

        [Route, HttpPost, AllowAnonymous]
        public HttpResponseMessage InsertFile(EncodedImage encodedImage)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                byte[] newBytes = Convert.FromBase64String(encodedImage.EncodedImageFile);
                ImageFile model = new ImageFile();
                model.ImageFileName = "galleryImg";
                model.ImageUrl = null;
                model.ByteArray = newBytes;
                model.ImageFileType = 1;
                model.Location = "gallery";
                model.ModifiedBy = "1";
                model.Extension = encodedImage.FileExtension;

                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = imageFileService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
