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
    public class LinkService : BaseService
    {
        public int Insert(Link model)
        {
            int id = 0;
            this.DataProvider.ExecuteNonQuery(
                "Links_Insert",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.SqlDbType = System.Data.SqlDbType.Int;
                    paramId.Direction = System.Data.ParameterDirection.Output;
                    paramCol.Add(paramId);

                    paramCol.AddWithValue("@Title", model.Title);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@Url", model.Url);
                    paramCol.AddWithValue("@ImageId", model.ImageId);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                },
                returnParameters: delegate(SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }

        public List<Link> SelectAll()
        {
            List<Link> result = new List<Link>();
            this.DataProvider.ExecuteCmd(
                "Links_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    Link model = LinkMapper(reader);
                    result.Add(model);
                }
            );
            return result;
        }

        public Link SelectById(int id)
        {
            Link model = new Link();
            this.DataProvider.ExecuteCmd(
                "Links_SelectById",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    model = LinkMapper(reader);
                }
            );
            return model;
        }

        public void Update(Link model)
        {
            this.DataProvider.ExecuteNonQuery(
                "Links_Update",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@@Title", model.Title);
                    paramCol.AddWithValue("@Description", model.Description);
                    paramCol.AddWithValue("@Url", model.Url);
                    paramCol.AddWithValue("@ImageId", model.ImageId);
                    paramCol.AddWithValue("@ModifiedBy", model.ModifiedBy);
                }
            );
        }

        public void Delete(int id)
        {
            this.DataProvider.ExecuteNonQuery(
                "Links_Delete",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }

        private static Link LinkMapper(IDataReader reader)
        {
            Link model = new Link();
            int index = 0;
            model.Id = reader.GetSafeInt32(index++);
            model.Title = reader.GetSafeString(index++);
            model.Description = reader.GetSafeString(index++);
            model.Url = reader.GetSafeString(index++);
            model.ImageId = reader.GetSafeInt32(index++);
            model.CreatedDate = reader.GetSafeDateTime(index++);
            model.ModifiedDate = reader.GetSafeDateTime(index++);
            model.ModifiedBy = reader.GetSafeString(index++);
            return model;
        }
    }
}
