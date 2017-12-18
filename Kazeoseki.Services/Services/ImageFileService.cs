using Amazon.S3;
using Amazon.S3.Model;
using Kazeoseki.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Services.Services
{
    public class ImageFileService : BaseService
    {
        static IAmazonS3 client;

        public int Insert(ImageFile model)
        {
            int id = 0;
            string systemFileName = string.Empty;
            model.ModifiedBy = "0";

            if (model.ByteArray != null)
            {
                systemFileName = string.Format("{0}_{1}{2}",
                    model.ImageFileName,
                    Guid.NewGuid().ToString(),
                    model.Extension);
                SaveToAmazon(systemFileName, model.ByteArray);
            }
            else if (model.ImageUrl != null)
            {
                string dlUrl = model.ImageUrl.Split('?')[0];
                string ext = Path.GetExtension(dlUrl);

                WebClient webClient = new WebClient();
                byte[] imgBytes = webClient.DownloadData(model.ImageUrl);
                webClient.Dispose();

                systemFileName = string.Format("{0}_{1}{2}",
                    model.ImageFileName,
                    Guid.NewGuid().ToString(),
                    ext);
                SaveToAmazon(systemFileName, imgBytes);
            }

            model.SystemFileName = systemFileName;
            model.Location = "AmazonS3";

            this.DataProvider.ExecuteNonQuery(
                    "ImageFiles_Insert",
                    inputParamMapper: delegate (SqlParameterCollection paramCol)
                    {
                        SqlParameter paramId = new SqlParameter();
                        paramId.ParameterName = "@Id";
                        paramId.SqlDbType = System.Data.SqlDbType.Int;
                        paramId.Direction = System.Data.ParameterDirection.Output;
                        paramCol.Add(paramId);

                        paramCol.AddWithValue("@ImageFileName", model.ImageFileName);
                        paramCol.AddWithValue("@SystemFileName", model.SystemFileName);
                        paramCol.AddWithValue("@ImageFileType", model.ImageFileType);
                        paramCol.AddWithValue("@Location", model.Location);
                        paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    },
                    returnParameters: delegate (SqlParameterCollection paramCol)
                    {
                        id = (int)paramCol["@Id"].Value;
                    }
            );
            return id;
        }

        //public void DeleteAmazonFile ; reference FileUploadService.cs from LPGallery

        private void SaveToAmazon(string systemFileName, byte[] bytes)
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USWest2))
            {
                var request = new PutObjectRequest
                {
                    BucketName = "kazeoseki",
                    CannedACL = S3CannedACL.PublicRead,
                    Key = string.Format("images/{0}", systemFileName)
                };
                using (var ms = new MemoryStream(bytes))
                {
                    request.InputStream = ms;
                    client.PutObject(request);
                }
            }
        }

    }
}
