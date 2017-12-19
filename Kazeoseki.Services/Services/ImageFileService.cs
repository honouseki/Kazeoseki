using Amazon.S3;
using Amazon.S3.Model;
using Kazeoseki.Data;
using Kazeoseki.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<ImageFile> SelectByImageType(int typeId)
        {
            List<ImageFile> result = new List<ImageFile>();
            this.DataProvider.ExecuteCmd(
                "ImageFiles_SelectByImageType",
                inputParamMapper: delegate(SqlParameterCollection paramCol) 
                {
                    paramCol.AddWithValue("@ImageFileType", typeId);
                },
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    ImageFile model = new ImageFile();
                    int index = 0;

                    model.FileId = reader.GetSafeInt32(index++);
                    model.ImageFileName = reader.GetSafeString(index++);
                    model.SystemFileName = reader.GetSafeString(index++);
                    model.ImageFileType = reader.GetSafeInt32(index++);
                    model.Location = reader.GetSafeString(index++);
                    model.CreatedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedBy = reader.GetSafeString(index++);

                    result.Add(model);
                }
            );
            return result;
        }

        public ImageFile SelectById(int id)
        {
            ImageFile model = new ImageFile();
            this.DataProvider.ExecuteCmd(
                "ImageFiles_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;
                    model.FileId = reader.GetSafeInt32(index++);
                    model.ImageFileName = reader.GetSafeString(index++);
                    model.SystemFileName = reader.GetSafeString(index++);
                    model.ImageFileType = reader.GetSafeInt32(index++);
                    model.Location = reader.GetSafeString(index++);
                    model.CreatedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedBy = reader.GetSafeString(index++);
                }
            );
            return model;
        }

        public void Delete(int id, string systemFileName)
        {
            this.DataProvider.ExecuteNonQuery(
                "ImageFiles_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
            // Deletes from AmazonC3
            DeleteFromAmazon(systemFileName);
        }

        private void SaveToAmazon(string systemFileName, byte[] bytes)
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USWest1))
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

        private void DeleteFromAmazon(string systemFileName)
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USWest1))
            {
                DeleteObjectRequest obj = new DeleteObjectRequest
                {
                    BucketName = "kazeoseki",
                    Key = string.Format("images/{0}", systemFileName)
                };
                try
                {
                    client.DeleteObject(obj);
                }
                catch (AmazonS3Exception ex)
                {
                    Console.WriteLine(ex.Message, ex.InnerException);
                }
            }
        }
    }
}
