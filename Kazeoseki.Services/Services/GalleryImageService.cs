using Kazeoseki.Data;
using Kazeoseki.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Services.Services
{
    public class GalleryImageService : BaseService
    {
        public int Insert(GalleryImage model)
        {
            int id = 0;
            this.DataProvider.ExecuteNonQuery(
                "GalleryImages_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = System.Data.SqlDbType.Int;
                    paramId.Direction = System.Data.ParameterDirection.Output;
                    paramCol.Add(paramId);

                    paramCol.AddWithValue("@ImageTitle", model.ImageTitle);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@FileId", model.FileId);
                    paramCol.AddWithValue("@CategoryId", model.CategoryId);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                },
                returnParameters: delegate(SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }

        public List<GalleryImage> SelectAll()
        {
            List<GalleryImage> result = new List<GalleryImage>();
            this.DataProvider.ExecuteCmd(
                "GalleryImages_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    GalleryImage model = new GalleryImage();
                    int index = 0;

                    model.Id = reader.GetSafeInt32(index++);
                    model.ImageTitle = reader.GetSafeString(index++);
                    model.Description = reader.GetSafeString(index++);
                    model.FileId = reader.GetSafeInt32(index++);
                    model.CategoryId = reader.GetSafeInt32(index++);
                    model.CreatedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedBy = reader.GetSafeString(index++);

                    result.Add(model);
                }
            );
            return result;
        }

        public void Update(GalleryImage model)
        {
            this.DataProvider.ExecuteNonQuery(
                "GalleryImages_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@ImageTitle", model.ImageTitle);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@FileId", model.FileId);
                    paramCol.AddWithValue("@CategoryId", model.CategoryId);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                }
            );
        }

        public void Delete(int id)
        {
            this.DataProvider.ExecuteNonQuery(
                "GalleryImages_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }
    }
}
