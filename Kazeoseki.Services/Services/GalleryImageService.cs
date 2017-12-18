using Kazeoseki.Models.Domain;
using System;
using System.Collections.Generic;
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
    }
}
